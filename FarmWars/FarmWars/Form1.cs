using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;



namespace FarmWars
{
    public partial class FormGame : Form
    {



        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        //Declare Interger Arrays and Bools
        public int[] PathPos = new int[] { 0, 0, 0, 0, 0, 0 };
        public int[] PathLoc = new int[] { 0, 0, 0, 0, 0, 0 };
        public bool[] Drawn = new bool[6];
        public int[] HosHealth = new int[] { 100, 100, 100, 100, 100, 100 };

        //Declare Intergers
        int TileCount;
        public int Traversible = 1;
        int mapwidth = 0;
        int XPos;
        int YPos;
        public int HuPathLoc = 0;
        public int HuPathPos = 0;
        public int HuHealth = 50;
        int SquareSize = 25;
        int XCord = 0;
        int YCord = 0;
        int Turn = 0;
        int tutorial = 0;
        int AX;
        int AY;
        int BX;
        int BY;
        int HuAX = 1;
        int HuAY = 1;
        int HuBX;
        int HuBY;
        public int width = 0;
        public int height = 0;
        int score = 0;


        //Declear Bools
        bool LShift = false;
        public bool RShift = false;
        bool MouseDrag = false;
        bool GoneShopping = false;
        bool InGame = true;
        bool InMenu = false;
        bool MapDrawn = false;

        //Declear Public Bools
        public bool HuDrawn;
        public bool HostileDrawn = false;
        public bool ISwordPurchased = false;
        public bool BSwordPurchased = false;
        public bool IArmourPurchased = false;
        public bool BArmourPurchased = false;
        public bool HighscoreAll = false;
        public bool UnderAttack = false;
        public bool spacebreak = false;
        public bool FormActive = true;
        
        //Declear Strings
        string oldText = string.Empty;
        string username = "";


        //Declear Clases
        Square squareTile = new Square();
        Feild feildTile = new Feild();

        HostileAstar[] HAstar = new HostileAstar[6];
        HumanAstar HuAstar = new HumanAstar();


        Inventory inventory = new Inventory();
        Shop shop = new Shop();

        //Create Tuple list to store information on tiles and thier information
        List<Tuple<int, int, int, int, int>> newTileMap = new List<Tuple<int, int, int, int, int>>();

        //Create Tuple list to store highscores
        List<Tuple<int, string>> highscore = new List<Tuple<int, string>>();

        //Create Bitmap Background
        private Bitmap Background;

        public FormGame()
        {
            InitializeComponent();
            //Declear Double buffering
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, PnlGame, new object[] { true });

            //Send the Panel Width and Height to the Inventory class
            inventory.PnlHeight = PnlGame.Height;
            inventory.PnlWidth = PnlGame.Width;

            //Send the Panel Width and Height to the Shop class
            shop.PnlHeight = PnlGame.Height;
            shop.PnlWidth = PnlGame.Width;

            //Create a each hostile in the hostile class
            for (int i = 0; i < HAstar.Length; i++)
            {
                HAstar[i] = new HostileAstar();
            }

            DrawMenu();
        }

        /// <summary>
        /// If the form is deactivated or deselected
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDeactivate(EventArgs e)
        {
            //Write if the form has been deselected to the console
            Console.WriteLine("Form Deslected");
            //If the map has already been created show resume on the button
            if (MapDrawn == true)
            {
                BtnGame.Text = "Resume";
            }
            //Set if the form is active to false
            FormActive = false;
            
            //Disable all the timers
            TmrDam.Enabled = false;
            TmrHosMovement.Enabled = false;
            TmrHumMovement.Enabled = false;
            TmrTurn.Enabled = false;
            
            //Pause the game
            PauseGame();
        }

        /// <summary>
        /// Draw the game menu
        /// </summary>
        private void DrawMenu()
        {
            //Declear the graphics as g
            Graphics g = PnlMenu.CreateGraphics();
            //Load the highscore from file
            LoadHighscore();
            //Draw the menu and menu background
            inventory.DrawMenu(g);

        }

        /// <summary>
        /// Yhis method draws the map for the game
        /// </summary>
        private void DrawMap()
        {
            //Create the background bitmap for the entire panel
            Background = new Bitmap(PnlGame.Width, PnlGame.Height);
            //Declear the graphics as g but onto the Background
            Graphics g = Graphics.FromImage(Background);
            //For each of the Hostiles, clear the lists in their clases to reset them
            for (int i = 0; i < 6; i++)
            {
                HAstar[i].EmptyList();
            }

            //Declear a variable called line to store the map information for a ASCII map
            string line = " ";

            //Declear the randoms
            Random tile = new Random();
            Random AB = new Random();
            //Calculate the randoms
            AX = AB.Next(1, 20);
            AY = AB.Next(1, 20);
            BX = AB.Next(30, 40);
            BY = AB.Next(20, 25);

            //Draw the grid with the number of columns given
            for (int y = 0; y * SquareSize < PnlGame.Height; y++)
            {

                //For each row until the number of rows is the same as the number of rows entered by the user
                for (int x = 0; PnlGame.Width > x * SquareSize; x++)
                {
                    //Add to the tile count
                    TileCount++;

                    //Randomly determine the tiletype for the tiles
                    int tiletype = tile.Next(1, 6);

                    //Set the variables for each of the Square Tiles in the class
                    squareTile.height = SquareSize;
                    squareTile.width = SquareSize;
                    squareTile.x = x;
                    squareTile.y = y;
                    squareTile.tiletype = tiletype;

                    //Add the tiles and their details to the class
                    newTileMap.Add(new Tuple<int, int, int, int, int>(x, y, tiletype, Traversible, 0));

                    //Draw the squares in the grid
                    squareTile.DrawSqaure(g);

                    //If the Y and X of the tile matches the AY and AX points specified then designate it as A on the ASCII map, A is startpoint
                    if (y == AY && x == AX)
                    {
                        line = line + "A";
                    }
                    else if (x == BX && y == BY) //If the Y and X of the tile matches the BY and BX points specified then desifnate it as B on the ACCII map, B is endpoint
                    {
                        line = line + "B";
                    }// If the X of the square is withen 9 squares of the width of the map and if the Y of the square is greater than 6 then make it so that the its rendere as - on the ASCII map, - means impassable
                    else if (x >= (PnlGame.Width / SquareSize) - 9)
                    {
                        int height = (PnlGame.Height / SquareSize) - 6;
                        if (y >= 6 && y <= height)
                        {
                            line = line + "-";
                        }
                        else // If the y is not greater then 6 render the ASCII map as it being passable.
                        {
                            line = line + " ";
                        }
                    }
                    else //If none of the above conditions are applicable then render is passable
                    {
                        line = line + " ";
                    }
                }//For each hostile send the line to them
                for (int i = 0; i < HAstar.Length; i++)
                {
                    HAstar[i].AddToList(line);
                }
                //Set the mapwidth to the length of the string
                mapwidth = line.Length;
                //Reset the line to nothing
                line = "";
            }
            //Once completed set the map to drawn
            MapDrawn = true;
            //Draw the shop onto the map
            shop.DrawShop(g, 100, 100);
            //Refresh the map by invalidating the panel
            PnlGame.Invalidate();
            //Set the Tmr for the Turn to enabled
            TmrTurn.Enabled = true;
        }

        /// <summary>
        /// When the panel is invalidated this will run to repaint the panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlGame_Paint(object sender, PaintEventArgs e)
        {
            //Set the variable g to e.Graphics
            var g = e.Graphics;
            //If the map has been drawn
            if (MapDrawn == true)
            {
                //Draw the map to the panel
                g.DrawImage(Background, 0, 0);

                //Set the variables in the inventory class for the Pnl Width and Height,
                inventory.PnlHeight = PnlGame.Height;
                inventory.PnlWidth = PnlGame.Width;

                //Draw the inventory to the panel
                inventory.DrawInventory(g);

            }
            //If GoneShopping is true display the shop
            if (GoneShopping == true)
            {
                shop.DrawShopMenu(g);
            }
        }

        /// <summary>
        /// This event sets the details such as the X and Y of the mouse when it moves
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlGame_MouseMove(object sender, MouseEventArgs e)
        {
            //Set the coordiantes to the mouse to a public variable, one that is specific to the map and one that is specific to the entire panel
            XCord = (e.X / 25);
            YCord = (e.Y / 25);
            XPos = e.X;
            YPos = e.Y;
            //If the mouse is being dragged
            if (MouseDrag == true)
            {
                //If RShift has been toggled to true, RShift being BuildMode
                if (RShift == true)
                {
                    //Set the graphics of F to the background
                    Graphics f = Graphics.FromImage(Background);

                    //If the click on the panel was withen the blue square surrounding the Human
                    if (XPos >= -50 + HuAstar.xLoc && YPos >= -50 + HuAstar.yLoc && XPos <= 75 + HuAstar.xLoc && YPos <= 75 + HuAstar.yLoc)
                    {
                        //Set the itemnum to 0
                        int itemnum = 0;
                        //If their is a selected crop avalible in the inventory
                        if (inventory.inventory.Where(z => z.Item1 == feildTile.CropType).Any())
                        {
                            //If the var      
                            var itemtype = inventory.inventory.Find(z => z.Item1 == feildTile.CropType);
                            if (itemtype.Item2 > 0)
                            {
                                if (feildTile.cropfield.Where(z => z.Item3 == XCord * SquareSize && z.Item4 == YCord * SquareSize).Any())
                                {

                                } else
                                {
                                    feildTile.SquareSize = SquareSize;
                                    feildTile.DrawFeild(f, Turn, XCord, YCord);
                                    itemnum = itemtype.Item2 - 1;
                                    inventory.inventory.RemoveAll(z => z.Item1 == feildTile.CropType);
                                    inventory.inventory.Add(new Tuple<string, int>(feildTile.CropType, itemnum));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void PnlGame_Click(object sender, EventArgs e)
        {
            Graphics g = PnlGame.CreateGraphics();
            Graphics f = Graphics.FromImage(Background);
            if (InGame == true)
            {
                if (RShift == true)
                {
                    if (XPos >= -50 + HuAstar.xLoc && YPos >= -50 + HuAstar.yLoc && XPos <= 75 + HuAstar.xLoc && YPos <= 75 + HuAstar.yLoc)
                    {
                        int itemnum = 0;
                        if (inventory.inventory.Where(z => z.Item1 == feildTile.CropType).Any())
                        {
                            var itemtype = inventory.inventory.Find(z => z.Item1 == feildTile.CropType);
                            if (itemtype.Item2 > 0)
                            {
                                feildTile.SquareSize = SquareSize;
                                feildTile.DrawFeild(f, Turn, XCord, YCord);

                                itemnum = itemtype.Item2 - 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == feildTile.CropType);
                                inventory.inventory.Add(new Tuple<string, int>(feildTile.CropType, itemnum));
                            }
                        }
                    }
                }
                if (RShift == false)
                {
                    if (XPos >= -50 + HuAstar.xLoc && YPos >= -50 + HuAstar.yLoc && XPos <= 75 + HuAstar.xLoc && YPos <= 75 + HuAstar.yLoc)
                    {
                        string CropType = feildTile.HarvestField(XPos, YPos, Turn);

                        int tilex = XPos / 25;
                        int tiley = YPos / 25;
                        if (CropType == "TaterSeed")
                        {
                            if (inventory.inventory.Where(z => z.Item1 == "Tater").Any())
                            {
                                int itemnum;
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "Tater");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "Tater");
                                inventory.inventory.Add(new Tuple<string, int>("Tater", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("Tater", 1));
                            }
                            ResetTile(tilex, tiley);

                        }
                        if (CropType == "WheatSeed")
                        {
                            if (inventory.inventory.Where(z => z.Item1 == "Wheat").Any())
                            {
                                int itemnum;
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "Wheat");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "Wheat");
                                inventory.inventory.Add(new Tuple<string, int>("Wheat", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("Wheat", 1));
                            }
                            ResetTile(tilex, tiley);

                        }
                        if (CropType == "CornSeed")
                        {
                            if (inventory.inventory.Where(z => z.Item1 == "Corn").Any())
                            {
                                int itemnum;
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "Corn");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "Corn");
                                inventory.inventory.Add(new Tuple<string, int>("Corn", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("Corn", 1));
                            }
                            ResetTile(tilex, tiley);

                        }
                        if (CropType == "CarrotSeed")
                        {
                            if (inventory.inventory.Where(z => z.Item1 == "Carrot").Any())
                            {
                                int itemnum;
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "Carrot");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "Carrot");
                                inventory.inventory.Add(new Tuple<string, int>("Carrot", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("Carrot", 1));
                            }
                            ResetTile(tilex, tiley);

                        }
                        if (CropType == "TurnipSeed")
                        {
                            if (inventory.inventory.Where(z => z.Item1 == "Turnip").Any())
                            {
                                int itemnum;
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "Turnip");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "Turnip");
                                inventory.inventory.Add(new Tuple<string, int>("Turnip", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("Turnip", 1));
                            }
                            ResetTile(tilex, tiley);
                        }
                    }
                }
                if (XPos >= 0 && YPos >= 0 && XPos <= 50 && YPos <= 50)
                {
                    PauseGame();
                }
                else if (XPos >= PnlGame.Width - 290 && YPos >= PnlGame.Height - 125 && XPos <= PnlGame.Width && YPos <= PnlGame.Height)
                {
                    if (UnderAttack == false)
                    {
                        inventory.nextturn = false;
                        Turn += 1;
                        feildTile.PlantTurn(f, Turn);
                        for (int i = 0; i < 6; i++)
                        {
                            HosHealth[i] = 100;
                        }
                        if (feildTile.cropfield.Count != 0)
                        {
                            DrawHostile();
                            UnderAttack = true;
                        }
                        username = TbUsername.Text;
                        CalcualteScore();
                        highscore.RemoveAll(z => z.Item2 == username);
                        highscore.Add(new Tuple<int, string>(score, username));
                        CreateHighscore();
                    }
                }
                else if (XPos >= 100 && YPos >= 100 && XPos <= 200 && YPos <= 180)
                {
                    if (XPos >= -50 + HuAstar.xLoc && YPos >= -50 + HuAstar.yLoc && XPos <= 75 + HuAstar.xLoc && YPos <= 75 + HuAstar.yLoc)
                    {
                        shop.DrawShopMenu(g);
                        TmrTurn.Enabled = false;
                        TmrHosMovement.Enabled = false;
                        TmrHumMovement.Enabled = false;
                        TmrDam.Enabled = false;
                        GoneShopping = true;
                        InGame = false;

                    }

                }
                if (XPos >= PnlGame.Width - (4 * 50) && YPos >= inventory.PosY + 125 && XPos <= PnlGame.Width - (1 * 50) && YPos <= inventory.PosY + 125 + (6 * 50))
                {
                    int invsel = 0;
                    double invsqx = Math.Round(Convert.ToDouble(XPos) / 50, 0) - 18;
                    double invsqy = Math.Round(Convert.ToDouble(YPos) / 50, 0) - 3;
                    if (invsqx == 1)
                    {
                        invsel = Convert.ToInt32(invsqy);
                    }
                    if (invsqx == 2)
                    {
                        invsel = Convert.ToInt32(invsqy) + 6;
                    }
                    if (invsqx == 3)
                    {
                        invsel = Convert.ToInt32(invsqy) + 12;
                    }
                    if (inventory.inventory.Count > invsel)
                    {
                        string Item = inventory.inventory[invsel].Item1;
                        inventory.selitemname = Item;
                        feildTile.CropType = Item;
                    }
                }
                else
                {
                    if (LShift == true)
                    {
                        HuPathLoc = 0;
                        HuPathPos = 0;
                        HuAstar.EmptyList();

                        HuBX = XCord;
                        HuBY = YCord;
                        HuAstar.StartX = HuAX;
                        HuAstar.StartY = HuAY;
                        int tiletype;
                        string line = "";
                        //Draw the grid with the number of columns given
                        for (int y = 0; y * SquareSize < PnlGame.Height; y++)
                        {

                            //For each row until the number of rows is the same as the number of rows entered by the user
                            for (int x = 0; PnlGame.Width > x * SquareSize; x++)
                            {
                                try
                                {
                                    var tile = newTileMap.First(z => z.Item1 == x && z.Item2 == y);

                                    tiletype = tile.Item3;


                                    if (y == HuAY && x == HuAX)
                                    {
                                        line = line + "A";
                                        using (Font font = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel))
                                        {
                                            Point point1 = new Point(HuAX * 25, HuAY * 25);
                                            TextRenderer.DrawText(g, "A", font, point1, Color.Red);
                                        }
                                    }
                                    else if (x == HuBX && y == HuBY)
                                    {
                                        line = line + "B";
                                    }
                                    else if (x >= (PnlGame.Width / SquareSize) - 9)
                                    {
                                        line = line + "-";
                                    } else if (x >= (PnlGame.Width / SquareSize) - 12 && y > (PnlGame.Height / SquareSize) - 7)
                                    {
                                        line = line + "-";
                                    }
                                    else if (x >= 3 && y >= 3 && x <= 8 & y <= 8)
                                    {
                                        line = line + "-";
                                    }
                                    else
                                    {
                                        line = line + " ";
                                    }
                                }
                                catch
                                {

                                }
                            }
                            HuAstar.AddToList(line);
                            line = "";

                        }
                        HuAstar.Main(g);
                    }
                }
            }
            if (GoneShopping == true)
            {
                int shopscaled = Convert.ToInt32(shop.scalefactord);
                int shopscalem = Convert.ToInt32(shop.scalefactorm);


                if (XPos >= 100 && YPos >= 60 && XPos <= PnlGame.Width - 100 && YPos <= PnlGame.Height - 60)
                {
                    if (XPos >= shop.shopx + (((0 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((190 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (((0 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        if (inventory.Moneyz >= 25)
                        {
                            int itemnum = 0;
                            if (inventory.inventory.Where(z => z.Item1 == "TaterSeed").Any())
                            {
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "TaterSeed");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "TaterSeed");
                                inventory.inventory.Add(new Tuple<string, int>("TaterSeed", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("TaterSeed", 1));
                            }

                            //Tater
                            inventory.Moneyz = inventory.Moneyz - 25;
                            inventory.UpdateMoneyz(g);
                        }
                    }
                    if (XPos >= shop.shopx + (((1 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((190 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (((1 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {

                        if (inventory.Moneyz >= 35)
                        {
                            int itemnum = 0;
                            if (inventory.inventory.Where(z => z.Item1 == "WheatSeed").Any())
                            {
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "WheatSeed");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "WheatSeed");
                                inventory.inventory.Add(new Tuple<string, int>("WheatSeed", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("WheatSeed", 1));
                            }

                            //Wheat
                            inventory.Moneyz = inventory.Moneyz - 35;
                            inventory.UpdateMoneyz(g);
                        }

                    }
                    if (XPos >= shop.shopx + (((2 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((190 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (((2 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        if (inventory.Moneyz >= 50)
                        {
                            int itemnum = 0;
                            if (inventory.inventory.Where(z => z.Item1 == "CornSeed").Any())
                            {
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "CornSeed");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "CornSeed");
                                inventory.inventory.Add(new Tuple<string, int>("CornSeed", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("CornSeed", 1));
                            }

                            //Corn
                            inventory.Moneyz = inventory.Moneyz - 50;
                            inventory.UpdateMoneyz(g);
                        }
                    }
                    if (XPos >= shop.shopx + (((3 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((190 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (((3 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        if (inventory.Moneyz >= 100)
                        {
                            int itemnum = 0;
                            if (inventory.inventory.Where(z => z.Item1 == "CarrotSeed").Any())
                            {
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "CarrotSeed");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "CarrotSeed");
                                inventory.inventory.Add(new Tuple<string, int>("CarrotSeed", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("CarrotSeed", 1));
                            }

                            //Carrot
                            inventory.Moneyz = inventory.Moneyz - 100;
                            inventory.UpdateMoneyz(g);
                        }
                    }
                    if (XPos >= shop.shopx + (((4 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((190 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (((4 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        if (inventory.Moneyz >= 150)
                        {
                            int itemnum = 0;
                            if (inventory.inventory.Where(z => z.Item1 == "TurnipSeed").Any())
                            {
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "TurnipSeed");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "TurnipSeed");
                                inventory.inventory.Add(new Tuple<string, int>("TurnipSeed", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("TurnipSeed", 1));
                            }
                            //Turnip
                            inventory.Moneyz = inventory.Moneyz - 150;
                            inventory.UpdateMoneyz(g);
                        }
                    }
                    if (XPos >= shop.shopx + (600 / shopscaled) * shopscalem + (((0 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((190 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (600 / shopscaled) * shopscalem + (((0 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        int itemnum = 0;
                        if (inventory.inventory.Where(z => z.Item1 == "Tater").Any())
                        {
                            var itemtype = inventory.inventory.Find(z => z.Item1 == "Tater");
                            if (itemtype.Item2 == 0)
                            {

                            }
                            else
                            {
                                itemnum = itemtype.Item2 - 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "Tater");
                                inventory.inventory.Add(new Tuple<string, int>("Tater", itemnum));
                                //Sell Tater
                                inventory.Moneyz = inventory.Moneyz + 45;
                                inventory.UpdateMoneyz(g);
                            }
                        }
                    }
                    if (XPos >= shop.shopx + (600 / shopscaled) * shopscalem + (((1 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((190 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (600 / shopscaled) * shopscalem + (((1 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        int itemnum = 0;
                        if (inventory.inventory.Where(z => z.Item1 == "Wheat").Any())
                        {
                            var itemtype = inventory.inventory.Find(z => z.Item1 == "Wheat");
                            if (itemtype.Item2 == 0)
                            {

                            }
                            else
                            {
                                itemnum = itemtype.Item2 - 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "Wheat");
                                inventory.inventory.Add(new Tuple<string, int>("Wheat", itemnum));
                                //Sell Wheat
                                inventory.Moneyz = inventory.Moneyz + 60;
                                inventory.UpdateMoneyz(g);
                            }
                        }
                    }
                    if (XPos >= shop.shopx + (600 / shopscaled) * shopscalem + (((2 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((190 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (600 / shopscaled) * shopscalem + (((2 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        int itemnum = 0;
                        if (inventory.inventory.Where(z => z.Item1 == "Corn").Any())
                        {
                            var itemtype = inventory.inventory.Find(z => z.Item1 == "Corn");
                            if (itemtype.Item2 == 0)
                            {

                            }
                            else
                            {
                                itemnum = itemtype.Item2 - 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "Corn");
                                inventory.inventory.Add(new Tuple<string, int>("Corn", itemnum));
                                //Sell Corn
                                inventory.Moneyz = inventory.Moneyz + 100;
                                inventory.UpdateMoneyz(g);
                            }
                        }
                    }
                    if (XPos >= shop.shopx + (600 / shopscaled) * shopscalem + (((3 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((190 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (600 / shopscaled) * shopscalem + (((3 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        int itemnum = 0;
                        if (inventory.inventory.Where(z => z.Item1 == "Carrot").Any())
                        {
                            var itemtype = inventory.inventory.Find(z => z.Item1 == "Carrot");
                            if (itemtype.Item2 == 0)
                            {

                            }
                            else
                            {
                                itemnum = itemtype.Item2 - 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "Carrot");
                                inventory.inventory.Add(new Tuple<string, int>("Carrot", itemnum));
                                //Sell Carrot
                                inventory.Moneyz = inventory.Moneyz + 150;
                                inventory.UpdateMoneyz(g);
                            }
                        }
                    }
                    if (XPos >= shop.shopx + (600 / shopscaled) * shopscalem + (((4 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((190 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (600 / shopscaled) * shopscalem + (((4 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        int itemnum = 0;
                        if (inventory.inventory.Where(z => z.Item1 == "Turnip").Any())
                        {
                            var itemtype = inventory.inventory.Find(z => z.Item1 == "Turnip");
                            if (itemtype.Item2 == 0)
                            {

                            }
                            else
                            {
                                itemnum = itemtype.Item2 - 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "Turnip");
                                inventory.inventory.Add(new Tuple<string, int>("Turnip", itemnum));
                                //Sell Turnip
                                inventory.Moneyz = inventory.Moneyz + 250;
                                inventory.UpdateMoneyz(g);
                            }
                        }
                    }
                    if (XPos >= shop.shopx + (((0 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((300 / shopscaled) * shopscalem) + ((190 / shopscaled) * shopscalem) && YPos <= shop.shopy + 300 / shopscaled * shopscalem + 210 / shopscaled * shopscalem + 40 / shopscaled * shopscalem && XPos <= (shop.shopx + (((0 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        if (inventory.Moneyz >= 25)
                        {
                            int itemnum = 0;
                            if (inventory.inventory.Where(z => z.Item1 == "Fence").Any())
                            {
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "Fence");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "Fence");
                                inventory.inventory.Add(new Tuple<string, int>("Fence", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("Fence", 1));
                            }
                            //Fence
                            inventory.Moneyz = inventory.Moneyz - 25;
                            inventory.UpdateMoneyz(g);
                        }
                    }
                    if (XPos >= shop.shopx + (((1 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((300 / shopscaled) * shopscalem) + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((300 / shopscaled) * shopscalem) + ((210 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (((1 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        if (inventory.Moneyz >= 100)
                        {
                            int itemnum = 0;
                            if (inventory.inventory.Where(z => z.Item1 == "Wall").Any())
                            {
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "Wall");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "Wall");
                                inventory.inventory.Add(new Tuple<string, int>("Wall", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("Wall", 1));
                            }
                            //Wall
                            inventory.Moneyz = inventory.Moneyz - 100;
                            inventory.UpdateMoneyz(g);
                        }
                    }
                    if (XPos >= shop.shopx + (((2 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((300 / shopscaled) * shopscalem) + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((300 / shopscaled) * shopscalem) + ((210 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (((2 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        if (inventory.Moneyz >= 300)
                        {
                            if (ISwordPurchased == false)
                            {
                                //Iron Sword
                                inventory.Moneyz = inventory.Moneyz - 300;
                                inventory.UpdateMoneyz(g);
                            }
                        }
                    }
                    if (XPos >= shop.shopx + (((3 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((300 / shopscaled) * shopscalem) + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((300 / shopscaled) * shopscalem) + ((210 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (((3 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        if (inventory.Moneyz >= 600)
                        {
                            if (BSwordPurchased == false)
                            {
                                //Beast Sword
                                inventory.Moneyz = inventory.Moneyz - 600;
                                inventory.UpdateMoneyz(g);
                            }
                        }
                    }
                    if (XPos >= shop.shopx + (((4 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((300 / shopscaled) * shopscalem) + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((300 / shopscaled) * shopscalem) + ((210 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (((4 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        if (inventory.Moneyz >= 350)
                        {
                            if (BSwordPurchased == false)
                            {
                                //Iron Armour
                                HuAstar.maxhealth = 150;
                                inventory.Moneyz = inventory.Moneyz - 350;
                                inventory.UpdateMoneyz(g);
                            }
                        }
                    }
                    if (XPos >= shop.shopx + (((5 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((300 / shopscaled) * shopscalem) + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((300 / shopscaled) * shopscalem) + ((210 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (((5 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        if (inventory.Moneyz >= 700)
                        {
                            if (BSwordPurchased == false)
                            {
                                //Beast Armour
                                inventory.Moneyz = inventory.Moneyz - 700;
                                HuAstar.maxhealth = 200;
                                inventory.UpdateMoneyz(g);
                            }
                        }
                    }
                    if (XPos >= shop.shopx + (((6 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((300 / shopscaled) * shopscalem) + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((300 / shopscaled) * shopscalem) + ((210 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (((6 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        if (inventory.Moneyz >= 250)
                        {
                            int itemnum = 0;
                            if (inventory.inventory.Where(z => z.Item1 == "HealthPot").Any())
                            {
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "HealthPot");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "HealthPot");
                                inventory.inventory.Add(new Tuple<string, int>("HealthPot", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("HealthPot", 1));
                            }
                            //Health Potion
                            inventory.Moneyz = inventory.Moneyz - 250;
                            inventory.UpdateMoneyz(g);
                        }
                    }
                    if (XPos >= shop.shopx + (((7 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((300 / shopscaled) * shopscalem) + ((190 / shopscaled) * shopscalem) && YPos <= shop.shopy + (((300 / shopscaled) * shopscalem) + ((210 / shopscaled) * shopscalem) + (40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (((7 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
                    {
                        if (inventory.Moneyz >= 500)
                        {
                            int itemnum = 0;
                            if (inventory.inventory.Where(z => z.Item1 == "UselessPot").Any())
                            {
                                var itemtype = inventory.inventory.Find(z => z.Item1 == "UselessPot");
                                itemnum = itemtype.Item2 + 1;
                                inventory.inventory.RemoveAll(z => z.Item1 == "UselessPot");
                                inventory.inventory.Add(new Tuple<string, int>("UselessPot", itemnum));
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("UselessPot", 1));
                            }
                            //Useless Potion
                            inventory.Moneyz = inventory.Moneyz - 500;
                            inventory.UpdateMoneyz(g);
                        }
                    }
                    if (XPos >= shop.shopx + (830 / Convert.ToInt32(shop.scalefactord) * Convert.ToInt32(shop.scalefactorm)) && YPos >= shop.shopy + (280 / Convert.ToInt32(shop.scalefactord) * Convert.ToInt32(shop.scalefactorm)) && YPos <= shop.shopy + (280 / Convert.ToInt32(shop.scalefactord) * Convert.ToInt32(shop.scalefactorm)) + ((300 / shopscaled) * shopscalem) && XPos <= shop.shopx + (830 / Convert.ToInt32(shop.scalefactord) * Convert.ToInt32(shop.scalefactorm)) + (300 / Convert.ToInt32(shop.scalefactord) * Convert.ToInt32(shop.scalefactorm)))
                    {
                        GameOver();              
                    }
                    } else
                {
                    TmrTurn.Enabled = true;
                    TmrHosMovement.Enabled = true;
                    TmrHumMovement.Enabled = true;
                    TmrDam.Enabled = true;
                    GoneShopping = false;
                    InGame = true;
                }
            }
        }

        private void PnlGame_Validated(object sender, EventArgs e)
        {
            DrawMap();
        }

        public int GetId(string TileType, int id)
        {
            if (TileType == "Grass1")
            {
                id = 1;
            }
            if (TileType == "Grass2")
            {
                id = 2;
            }
            if (TileType == "Grass3")
            {
                id = 3;
            }
            if (TileType == "Grass4")
            {
                id = 4;
            }
            if (TileType == "Grass5")
            {
                id = 5;
            }
            if (TileType == "Field")
            {
                id = 6;
            }
            if (TileType == "Wheat")
            {
                id = 7;
            }
            if (TileType == "GrownWheat")
            {
                id = 8;
            }
            if (TileType == "Mud")
            {
                id = 9;
            }
            return id;
        }

        private void TmrGame_Tick(object sender, EventArgs e)
        {

            Graphics g = PnlGame.CreateGraphics();
            for (int i = 0; i < 6; i++)
            {
                PathPos[i]++;
                PathLoc[i]++;


                HAstar[i].PathLoc = PathLoc[i];
                HAstar[i].PathPos = PathPos[i];
                HAstar[i].PathFollow(g, i);
                PnlGame.Invalidate();
            }
            if (RShift == true)
            {
                inventory.Rshift = true;
            } else if (RShift == false)
            {
                inventory.Rshift = false;
            }
        }
        private void Invalme()
        {
            PnlGame.Invalidate();
        }


        private void moveGuyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics g = PnlGame.CreateGraphics();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PauseGame()
        {
            TmrHosMovement.Enabled = false;
            TmrDam.Enabled = false;
            TmrHumMovement.Enabled = false;
            TmrTurn.Enabled = false;
            PnlMenu.Visible = true;
            PnlMenu.Enabled = true;
            BtnGame.Text = "Resume";
        }

        private void GameOver()
        {
            MessageBox.Show("Thank You for playing. " + " You scored " + Convert.ToString(score) + "." + " You survived " + Convert.ToString(Turn) + " Turns.", " Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information) ;
            TmrHosMovement.Enabled = false;
            TmrDam.Enabled = false;
            TmrHumMovement.Enabled = false;
            TmrTurn.Enabled = false;
            PnlMenu.Visible = true;
            PnlMenu.Enabled = true;
            BtnGame.Text = "New Game";
            MapDrawn = false;
        }
        private void DrawHostile()
        {
            for (int i = 0; i <= 5; i++)
            {
                if (HAstar[i].attacking == false)
                {
                    int Max = ((PnlGame.Width / 25) * 2) + ((PnlGame.Height / 25) * 2);
                    int XCord;
                    int YCord;
                    Random Go = new Random();
                    int StartPos = Go.Next(1, Max);

                    if (StartPos * 25 > PnlGame.Width + PnlGame.Width + PnlGame.Height)
                    {
                        YCord = StartPos - (PnlGame.Width / 25) - (PnlGame.Width / 25) - (PnlGame.Height / 25);
                        XCord = 0;

                    }
                    if (StartPos * 25 > PnlGame.Width + PnlGame.Height)
                    {
                        XCord = StartPos - ((PnlGame.Width) / 25) - ((PnlGame.Height) / 25);
                        YCord = 0;

                    }
                    if (StartPos * 25 > PnlGame.Width)
                    {
                        YCord = StartPos - (PnlGame.Width / 25);
                        XCord = 0;

                    }
                    else
                    {
                        XCord = StartPos;
                        YCord = 0;
                    }
                    if (Drawn[i] == true)
                    {
                        if (feildTile.cropfield.Count > 0)
                        {
                            HosHealth[i] = 100;
                            int TargetF = Go.Next(0, feildTile.cropfield.Count);
                            BX = feildTile.cropfield[TargetF].Item3 / SquareSize;
                            BY = feildTile.cropfield[TargetF].Item4 / SquareSize;
                        }

                        PathPos[i] = 0;
                        PathLoc[i] = 0;
                        HAstar[i].EmptyList();

                        Graphics g = PnlGame.CreateGraphics();

                        AY = YCord;
                        AX = XCord;
                        HAstar[i].StartX = AX;
                        HAstar[i].StartY = AY;
                        int tiletype;
                        string line = "";

                        HAstar[i].x = XCord * SquareSize;
                        HAstar[i].y = YCord * SquareSize;
                        HAstar[i].DrawHostile(g, i);

                        //Draw the grid with the number of columns given
                        for (int y = 0; y * SquareSize < PnlGame.Height; y++)
                        {

                            //For each row until the number of rows is the same as the number of rows entered by the user
                            for (int x = 0; PnlGame.Width > x * SquareSize; x++)
                            {
                                try
                                {
                                    var tile = newTileMap.First(z => z.Item1 == x && z.Item2 == y);

                                    tiletype = tile.Item3;


                                    if (y == AY && x == AX)
                                    {
                                        line = line + "A";
                                        using (Font font = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel))
                                        {
                                            Point point1 = new Point(AX * 25, AY * 25);
                                            TextRenderer.DrawText(g, "A", font, point1, Color.Blue);
                                        }
                                    }
                                    else if (x == BX && y == BY)
                                    {
                                        line = line + "B";
                                    }
                                    else if (x >= (PnlGame.Width / SquareSize) - 8)
                                    {
                                        int height = (PnlGame.Height / SquareSize) - 6;
                                        if (y >= 6 && y <= height)
                                        {
                                            line = line + "-";
                                        }
                                        else
                                        {
                                            line = line + " ";
                                        }
                                    }
                                    else if (tiletype == 69)
                                    {
                                        line = line + "-";
                                    }
                                    else
                                    {
                                        line = line + " ";
                                    }
                                }
                                catch
                                {

                                }
                            }
                            HAstar[i].AddToList(line);
                            line = "";

                        }
                        HAstar[i].Main(g, i);
                    }
                    Drawn[i] = true;
                }
            }
              
        }

            private void TmrHumMovement_Tick(object sender, EventArgs e)
        {
            Graphics g = PnlGame.CreateGraphics();
            HuPathPos = HuPathPos + 1;
            HuPathLoc = HuPathLoc + 2;

            HuAstar.PathLoc = HuPathLoc;
            HuAstar.PathPos = HuPathPos;
            HuAX = HuAstar.xLoc / 25;
            HuAY = HuAstar.yLoc / 25;
            HuAstar.PathFollow(g);
        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e)
        {
            Graphics g = Graphics.FromImage(Background);
            Graphics f = PnlGame.CreateGraphics();

            if (e.KeyCode == Keys.ShiftKey)
            {
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.LShiftKey)))
                {
                    LShift = true; }
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.RShiftKey)))
                {
                    if (RShift == false)
                    {
                        RShift = true;
                        Console.WriteLine("Build Mode");
                    }
                    else
                    {
                        RShift = false;
                        Console.WriteLine("Build Mode Off");
                    }
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                TmrTurn.Enabled = true;
                TmrHosMovement.Enabled = true;
                TmrHumMovement.Enabled = true;
                TmrDam.Enabled = true;
                GoneShopping = false;
                InGame = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                if (spacebreak == false)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (HAstar[i].visible == true)
                        {
                            if (HuAstar.xLoc >= -50 + HAstar[i].xLoc && HuAstar.yLoc >= -50 + HAstar[i].yLoc && HuAstar.xLoc <= 75 + HAstar[i].xLoc && HuAstar.yLoc <= 75 + HAstar[i].yLoc)
                            {
                                int healthhos = HosHealth[i] - 6;
                                HAstar[i].DrawHealth(f, i);
                                HosHealth[i] = healthhos;
                            }
                        }
                    }
                    TmrAttack.Enabled = true;
                    spacebreak = true;
                }
            }
            }
           

        private void FormGame_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                LShift = (Convert.ToBoolean(GetAsyncKeyState(Keys.LShiftKey)));
            }
        }

        private void PnlGame_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDrag = true;
        }

        private void PnlGame_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDrag = false;
        }

        private void TmrDam_Tick(object sender, EventArgs e)
        {
            Graphics g = PnlGame.CreateGraphics();
            if (HuHealth <= 0)
            {
                GameOver();
            }

            for (int i = 0; i < 6; i++)
            {
                if (HAstar[i].visible == true)
                {
                    if (HAstar[i].xLoc >= -50 + HuAstar.xLoc && HAstar[i].yLoc >= -50 + HuAstar.yLoc && HAstar[i].xLoc <= 75 + HuAstar.xLoc && HAstar[i].yLoc <= 75 + HuAstar.yLoc)
                    {
                        int health = HuHealth - 5;
                        HuHealth = health;
                    }
                }
            }
            for (int f = 0; f < 6; f++)
            {   

                for (int i = 0; i < feildTile.cropfield.Count; i++)
                {
                    double FieldX = Convert.ToDouble(feildTile.cropfield[i].Item3) / SquareSize;
                    double FieldY = Convert.ToDouble(feildTile.cropfield[i].Item4) / SquareSize;
                    double HosX = Math.Floor(Convert.ToDouble(HAstar[f].xLoc) / 25);
                    double HosY = Math.Floor(Convert.ToDouble(HAstar[f].yLoc) / 25);

                    if (HosX == FieldX && HosY == FieldY)
                    {
                        int AX;
                        int AY;
                        int BX;
                        int BY;
                        int health = feildTile.cropfield[i].Item5 - 10;
                        int Turn = feildTile.cropfield[i].Item1;
                        string CropType = feildTile.cropfield[i].Item2;
                        feildTile.cropfield.RemoveAt(i);
                        feildTile.cropfield.Add(new Tuple<int, string, int, int, int>(Turn, CropType, Convert.ToInt32(FieldX) * SquareSize, Convert.ToInt32(FieldY) * SquareSize, health));
                        if (feildTile.cropfield[i].Item5 <= 0)
                        {
                            int FeildX = feildTile.cropfield[i].Item3 / 25;
                            int FeildY = feildTile.cropfield[i].Item4 / 25;
                            ResetTile(FeildX, FeildY);
                            feildTile.cropfield.RemoveAt(i);

                            if (feildTile.cropfield.Count > 0)
                            {
                                Random Go = new Random();
                                int TargetF = Go.Next(0, feildTile.cropfield.Count);

                                BX = feildTile.cropfield[TargetF].Item3 / SquareSize;
                                BY = feildTile.cropfield[TargetF].Item4 / SquareSize;

                                PathPos[f] = 0;
                                PathLoc[f] = 0;

                                HAstar[f].EmptyList();

                                AX = FeildX;
                                AY = FeildY;

                                HAstar[f].StartX = AX;
                                HAstar[f].StartY = AY;

                                int tiletype;
                                string line = "";

                                HAstar[f].DrawHostile(g, f);

                                //Draw the grid with the number of columns given
                                for (int y = 0; y * SquareSize < PnlGame.Height; y++)
                                {

                                    //For each row until the number of rows is the same as the number of rows entered by the user
                                    for (int x = 0; PnlGame.Width > x * SquareSize; x++)
                                    {
                                        var tile = newTileMap.First(z => z.Item1 == x && z.Item2 == y);

                                        tiletype = tile.Item3;


                                        if (y == AY && x == AX)
                                        {
                                            line = line + "A";
                                        }
                                        else if (x == BX && y == BY)
                                        {
                                            line = line + "B";
                                        }
                                        else if (x >= (PnlGame.Width / SquareSize) - 8)
                                        {
                                            int height = (PnlGame.Height / SquareSize) - 6;
                                            if (y >= 6 && y <= height)
                                            {
                                                line = line + "-";
                                            }
                                            else
                                            {
                                                line = line + " ";
                                            }
                                        }
                                        else if (tiletype == 69)
                                        {
                                            line = line + "-";
                                        }
                                        else
                                        {
                                            line = line + " ";
                                        }
                                    }
                                    HAstar[f].AddToList(line);
                                    line = "";

                                }
                                HAstar[f].Main(g, f);
                            } 
                        }
                    }
                }

            }
        }

        private void TmrTurn_Tick(object sender, EventArgs e)
        {
            HuAstar.health = HuHealth;
            inventory.score = score;
            UnderAttack = HAstar.Any(h => h.attacking);
            if (UnderAttack == false)
            {
                HuHealth = HuAstar.maxhealth;
                for (int i = 0; i < 6; i++)
                {
                    HAstar[i].xLoc = 0;
                    HAstar[i].yLoc = 0;
                }
                inventory.nextturn = true;
            }
        }

        private void ResetTile(int TileX,int TileY)
        {
            Graphics g = Graphics.FromImage(Background);

            var tile = newTileMap.First(z => z.Item1 == TileX && z.Item2 == TileY);
            int tiletype = tile.Item3;

            squareTile.tiletype = tiletype;
            squareTile.height = SquareSize;
            squareTile.width = SquareSize;
            squareTile.x = TileX;
            squareTile.y = TileY;

            squareTile.DrawSqaure(g);
        }

        private void LoadHighscore()
        {
            //Set
            string file = "..\\..\\..\\Highscore.txt";

            StreamReader reader;
    
            string fileline;

            List<string> filelines = new List<string>();

            int score = 0;
            string username = "";

            //If the file Exists
            if (File.Exists(file))
            {
                //Open the selected file
                reader = File.OpenText(file);
                //Repeat while it is not the end of file
                while (!reader.EndOfStream)
                {
                    //Read the object type from the file
                    fileline = reader.ReadLine();

                    filelines.Add(fileline);
                }
                for (int i = 0; i < filelines.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        username = filelines[i];
                    }
                    else
                    {
                        score = Convert.ToInt32(filelines[i]);
                        highscore.Add(new Tuple<int, string>(score, username));
                        UpdateHighscore();
                    }
                }
            }
        }

        private void UpdateHighscore()
        {
            int amount = highscore.Count;
            if (HighscoreAll == false)
            {
                if (highscore.Count > 10)
                {
                    amount = 10;
                }
            }
            highscore = highscore.OrderByDescending(z => z.Item1).ToList();
            LbHighscore.Items.Clear();
            LbHighscore.Items.Add("UserName:" + "Score:".PadLeft(10));
            for (int i = 0; i < amount; i++)
            {
                int score = highscore[i].Item1;
                string username = highscore[i].Item2;
                int padding = 20 - username.Length;
                LbHighscore.Items.Add(username + Convert.ToString(score).PadLeft(padding)) ;
            }
        }

        private void CreateHighscore()
        {
            //Set
            string file = "..\\..\\..\\Highscore.txt";
            //If the file Exists
            if (File.Exists(file))
            {
                //Delete original file
                File.Delete(file);
                // Create a new file     
                using (StreamWriter sw = File.CreateText(file))
                {
                    //For each score write line to file
                    for (int i = 0; i < highscore.Count; i++)
                    {
                        //Get the score
                        int score = highscore[i].Item1;
                        //Get the username for the score
                        string username = highscore[i].Item2;
                        //Write line to text file with Username, then Score
                        sw.WriteLine(Convert.ToString(username));
                        sw.WriteLine(Convert.ToString(score));

                    }
                }
            }
            else
            {
                // Create a new file
                using (StreamWriter sw = File.CreateText(file))
                {
                    //For each score write line to file
                    for (int i = 0; i < highscore.Count; i++)
                    {
                        //Get the score
                        int score = highscore[i].Item1;
                        //Get the username for the score
                        string username = highscore[i].Item2;
                        //Write line to text file with Username, then Score
                        sw.WriteLine(Convert.ToString(username));
                        sw.WriteLine(Convert.ToString(score));

                    }
                }
            }
            UpdateHighscore();
        }

        private void PnlMenu_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, PnlMenu.Width, PnlMenu.Height);
            Image menuImage = Image.FromFile("../../../Art/menubackground.png");
            g.DrawImage(menuImage, rect);
        }

        private void TbUsername_TextChanged(object sender, EventArgs e)
        {
            if (TbUsername.Text.All(chr => char.IsLetter(chr)))
            {
                if (TbUsername.Text.Length < 15)
                {
                    oldText = TbUsername.Text;
                    TbUsername.Text = oldText;
                }

                TbUsername.BackColor = System.Drawing.Color.White;
                TbUsername.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                TbUsername.Text = oldText;
                TbUsername.BackColor = System.Drawing.Color.White;
                TbUsername.ForeColor = System.Drawing.Color.Red;
            }
            if (TbUsername.Text.Length > 14)
            {
                TbUsername.Text = oldText;
                TbUsername.BackColor = System.Drawing.Color.White;
                TbUsername.ForeColor = System.Drawing.Color.Red;
            }
            TbUsername.SelectionStart = TbUsername.Text.Length;
            username = TbUsername.Text;
        }

        private void BtnGame_Click(object sender, EventArgs e)
        {
            if (TbUsername.Text != "")
            {
                if (MapDrawn == false)
                {
                    DrawMap();
                }
                PnlMenu.Visible = false;
                PnlMenu.Enabled = false;
                TmrDam.Enabled = true;
                TmrTurn.Enabled = true;
                TmrHosMovement.Enabled = true;
                TmrHumMovement.Enabled = true;
            } else
            {
                MessageBox.Show("Please ENTER a username!", "ERROR",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
                
        private void CalcualteScore()
        {
            string CropType = "";
            int plantscore = 0;

            for (int i = 0; i < feildTile.cropfield.Count; i++)
            {
                CropType = feildTile.cropfield[i].Item2;
                if (CropType == "TaterSeed")
                {
                    plantscore += 25;
                }
                if (CropType == "WheatSeed")
                {
                    plantscore += 50;
                }
                if (CropType == "CornSeed")
                {
                    plantscore += 100;
                }
                if (CropType == "CarrotSeed")
                {
                    plantscore += 175;
                }
                if (CropType == "TurnipSeed")
                {
                    plantscore += 250;
                }
                score = Turn + plantscore;

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (HighscoreAll == true)
            {
                HighscoreAll = false;
                LblHighSel.Text = "Top 10";
                btnSwitch.Text = "Show All";


            }
            else if (HighscoreAll == false)
            {
                HighscoreAll = true;
                LblHighSel.Text = "All";
                btnSwitch.Text = "Show Top 10";
            }
            UpdateHighscore();
        }

        private void TmrRegen_Tick(object sender, EventArgs e)
        {
            if (HuHealth < HuHealth - 4)
            {
              HuHealth = HuHealth + 4;
            }
        }

        private void TmrAttack_Tick(object sender, EventArgs e)
        {
            spacebreak = false;
            TmrAttack.Enabled = false;
        }

        private void BtnTutorial_Click(object sender, EventArgs e)
        {
            PnlTutorial.Visible = true;
            PnlTutorial.Enabled = true;
            tutorial = 0;
            DrawTutorialImage();
        }

        private void DrawTutorialImage()
        {
            Graphics g = PnlTutorial.CreateGraphics();

            Rectangle rectTut = new Rectangle(0, 0, PnlGame.Width, PnlGame.Height);
            Image tutorialImage = Image.FromFile("../../../Art/menubackground.png");

            if (tutorial == 0)
            {
                tutorialImage = Image.FromFile("../../../Art/ui/tutorial1.png");
            }
            if (tutorial == 1)
            {
                tutorialImage = Image.FromFile("../../../Art/ui/tutorial2.png");

            }
            if (tutorial == 2)
            {
                tutorialImage = Image.FromFile("../../../Art/ui/tutorial3.png");

            }
            if (tutorial == 3)
            {
                tutorialImage = Image.FromFile("../../../Art/ui/tutorial4.png");
            }
            if (tutorial == 4)
            {
                PnlTutorial.Visible = false;
                PnlTutorial.Enabled = false;
            }
            g.DrawImage(tutorialImage, rectTut);
        }
        private void PnlTutorial_MouseClick(object sender, MouseEventArgs e)
        {
            tutorial++;
            DrawTutorialImage();
        }
    }
}