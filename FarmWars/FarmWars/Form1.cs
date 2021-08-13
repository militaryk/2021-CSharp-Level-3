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
        public bool[] Drawn = new bool[6];
        int SquareSize = 25;
        int XCord = 0;
        int YCord = 0;
        int Turn = 0;



        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);



        bool MapDrawn = false;
        int TileCount;
        public int Traversible = 1;
        int mapwidth = 0;
        int XPos;
        int YPos;
        public int[] PathPos = new int[] { 0, 0, 0, 0, 0, 0 };
        public int[] PathLoc = new int[] { 0, 0, 0, 0, 0, 0 };
        public int HuPathLoc = 0;
        public int HuPathPos = 0;
        public int HuHealth = 100;
        public int[] HosHealth = new int[] { 100, 100, 100, 100, 100, 100 };
        bool LShift = false;
        public bool RShift = false;
        bool MouseDrag = false;
        bool GoneShopping = false;
        bool InGame = true;
        bool InMenu = false;

        public bool HuDrawn;
        public bool HostileDrawn = false;
        public bool ISwordPurchased = false;
        public bool BSwordPurchased = false;
        public bool IArmourPurchased = false;
        public bool BArmourPurchased = false;
        public bool HighscoreAll = false;
        public bool UnderAttack = false;

        public bool FormActive = true;


        string oldText = string.Empty;




        int AX;
        int AY;
        int BX;
        int BY;

        int HuAX = 1;
        int HuAY = 1;
        int HuBX;
        int HuBY;

        Square squareTile = new Square();
        Feild feildTile = new Feild();

        HostileAstar[] HAstar = new HostileAstar[6];
        HumanAstar HuAstar = new HumanAstar();

        Human human = new Human();
        Inventory inventory = new Inventory();
        Shop shop = new Shop();

        //Calculate the width and height of the square so that it all fits in the picturebox
        public int width = 0;
        public int height = 0;

        //Create list to store marks
        public List<double> tileList = new List<double>();
        public List<double> tileType = new List<double>();

        List<Tuple<int, int, int, int, int>> newTileMap = new List<Tuple<int, int, int, int, int>>();

        List<Tuple<int, string>> highscore = new List<Tuple<int, string>>();
        int score = 0;
        string username = "";

        private Bitmap Background;

        public FormGame()
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, PnlGame, new object[] { true });

            //Send the Panel Width and Height to the Inventory class
            inventory.PnlHeight = PnlGame.Height;
            inventory.PnlWidth = PnlGame.Width;
            shop.PnlHeight = PnlGame.Height;
            shop.PnlWidth = PnlGame.Width;

            for (int i = 0; i < HAstar.Length; i++)
            {
                HAstar[i] = new HostileAstar();
            }

            DrawMenu();
        }


        protected override void OnDeactivate(EventArgs e)
        {
            Console.WriteLine("Form Deslected");
            if (MapDrawn == true)
            {
                BtnGame.Text = "Resume";
            }
            FormActive = false;
            TmrDam.Enabled = false;
            TmrHosMovement.Enabled = false;
            TmrHumMovement.Enabled = false;
            TmrTurn.Enabled = false;
            PauseGame();
        }
        private void DrawMenu()
        {
            Graphics g = PnlMenu.CreateGraphics();
            LoadHighscore();
            inventory.DrawMenu(g);

        }
        private void DrawMap()
        {
            Background = new Bitmap(PnlGame.Width, PnlGame.Height);
            Console.WriteLine(Background);
            Graphics g = Graphics.FromImage(Background);
            for (int i = 0; i < 6; i++)
            {
                HAstar[i].EmptyList();
            }
            string line = " ";

            Random tile = new Random();
            Random AB = new Random();
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
                    TileCount++;

                    int tiletype = tile.Next(1, 6);




                    squareTile.height = SquareSize;
                    squareTile.width = SquareSize;
                    squareTile.x = x;
                    squareTile.y = y;
                    squareTile.tiletype = tiletype;

                    newTileMap.Add(new Tuple<int, int, int, int, int>(x, y, tiletype, Traversible, 0));

                    squareTile.DrawSqaure(g);

                    if (y == AY && x == AX)
                    {
                        line = line + "A";
                        using (Font font = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel))
                        {
                            Point point1 = new Point(AX * 25, AY * 25);
                            //TextRenderer.DrawText(g, "A", font, point1, Color.Blue);
                        }
                    }
                    else if (x == BX && y == BY)
                    {
                        using (Font font = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel))
                        {
                            Point point1 = new Point(BX * 25, BY * 25);
                            //6TextRenderer.DrawText(g, "B", font, point1, Color.Blue);
                        }
                        line = line + "B";
                    }
                    else if (tiletype == 69)
                    {
                        line = line + "-";
                    }
                    else if (x >= (PnlGame.Width / SquareSize) - 9)
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
                    else
                    {
                        line = line + " ";
                    }
                }
                for (int i = 0; i < HAstar.Length; i++)
                {
                    HAstar[i].AddToList(line);
                }
                Console.WriteLine(line);
                mapwidth = line.Length;
                line = "";
            }
            //SaveMap();
            MapDrawn = true;
            shop.DrawShop(g, 100, 100);
            PnlGame.Invalidate();
            TmrTurn.Enabled = true;
        }

        private void SaveMap()
        {
            int width = PnlGame.Size.Width;
            int height = PnlGame.Size.Height;
            Bitmap bm = new Bitmap(width, height);
            this.Background = bm;
            PnlGame.DrawToBitmap(bm, new Rectangle(0, 0, width, height));
            MapDrawn = true;
        }

        private void PnlGame_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            if (MapDrawn == true)
            {
                g.DrawImage(Background, 0, 0);
                inventory.PnlHeight = PnlGame.Height;
                inventory.PnlWidth = PnlGame.Width;
                inventory.DrawInventory(g);

            }
            if (GoneShopping == true)
            {
                shop.DrawShopMenu(g);
            }
        }
        private void PnlGame_MouseMove(object sender, MouseEventArgs e)
        {
            XCord = (e.X / 25);
            YCord = (e.Y / 25);
            XPos = e.X;
            YPos = e.Y;
            if (MouseDrag == true)
            {
                if (RShift == true)
                {
                    Graphics f = Graphics.FromImage(Background);

                    if (XPos >= -50 + HuAstar.xLoc && YPos >= -50 + HuAstar.yLoc && XPos <= 75 + HuAstar.xLoc && YPos <= 75 + HuAstar.yLoc)
                    {
                        int itemnum = 0;
                        if (inventory.inventory.Where(z => z.Item1 == feildTile.CropType).Any())
                        {
                            var itemtype = inventory.inventory.Find(z => z.Item1 == feildTile.CropType);
                            Console.WriteLine(itemtype.Item1);
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
                            Console.WriteLine(itemtype.Item1);
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
                                Console.WriteLine("Item Remvoed then added to Inv");
                            }
                            else
                            {
                                inventory.inventory.Add(new Tuple<string, int>("Turnip", 1));
                                Console.WriteLine("Item Added to Inv");
                            }
                            ResetTile(tilex, tiley);
                        }
                    }
                }
                if (XPos >= 0 && YPos >= 0 && XPos <= 50 && YPos <= 50)
                {
                    PauseGame();
                }
                else if (XPos >= 100 && YPos >= 0 && XPos <= 150 && YPos <= 50)
                {
                    ExitGame();
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
                        DrawHostile();
                        UnderAttack = true;
                        username = TbUsername.Text;
                        score = Turn;
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
                    Console.WriteLine(invsqy);
                    Console.WriteLine(Convert.ToInt32(invsqx));
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
                        Console.WriteLine(Convert.ToString(invsel));
                        Console.WriteLine(Item);
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
                            HuAstar.AddToList(line);
                            Console.WriteLine(line);
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
                    if (XPos >= shop.shopx + (((0 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((190 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <=  (shop.shopx + (((0 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
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
                    if (XPos >= shop.shopx + (600 / shopscaled) * shopscalem  + (((0 * 100) / shopscaled) * shopscalem) && YPos >= shop.shopy + ((190 / shopscaled) * shopscalem) && YPos <= (shop.shopy + ((190 / shopscaled) * shopscalem)) + ((40 / shopscaled) * shopscalem) && XPos <= (shop.shopx + (600 / shopscaled) * shopscalem + (((0 * 100) / shopscaled) * shopscalem)) + ((100 / shopscaled) * shopscalem))
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
                            Console.WriteLine(itemtype.Item1);
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
                                inventory.inventory.Add( new Tuple<string, int>("Turnip", itemnum));
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
                                human.maxhealth = 150;
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
                                human.maxhealth = 200;
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

        private void reLoadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newTileMap.ForEach(i => Console.Write("{0}\t", i));
            Console.WriteLine("hi");
            Console.WriteLine(newTileMap.Last());
        }

        private void pathFindingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PnlGame_DoubleClick(object sender, EventArgs e)
        {
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

        private void ExitGame()
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
                            Console.WriteLine(line);
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
            if (e.KeyCode == Keys.ShiftKey)
                {
                    if (Convert.ToBoolean(GetAsyncKeyState(Keys.LShiftKey)))
                    {
                        LShift = true;
                        Console.WriteLine("Left");
                    }
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
            for (int i = 0; i < 6; i++)
            {
                if (HAstar[i].visible == true)
                {
                    if (HAstar[i].xLoc >= -50 + HuAstar.xLoc && HAstar[i].yLoc >= -50 + HuAstar.yLoc && HAstar[i].xLoc <= 75 + HuAstar.xLoc && HAstar[i].yLoc <= 75 + HuAstar.yLoc)
                    {
                        int health = HuHealth - 2;
                        Console.WriteLine(health);
                        HuHealth = health;
                    }
                }
            }
            for (int i = 0; i < 6; i++)
            {
                if (HAstar[i].visible == true)
                {
                    if (HuAstar.xLoc >= -50 + HAstar[i].xLoc && HuAstar.yLoc >= -50 + HAstar[i].yLoc && HuAstar.xLoc <= 75 + HAstar[i].xLoc && HuAstar.yLoc <= 75 + HAstar[i].yLoc)
                    {
                        int healthhos = HosHealth[i] - 10;
                        HAstar[i].DrawHealth(g, i);
                        Console.WriteLine(healthhos);
                        HosHealth[i] = healthhos;
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

                    if (HosX == 0 && HosY == 0)
                    {
                        HAstar[f].attacking = false;
                    }


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

                            if (feildTile.cropfield.Count == 0)
                            {
                                for (int h = 0; h < 6; h++)
                                {
                                    HAstar[h].health = 0;
                                    HosHealth[h] = 0;
                                }
                            }
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
                                    Console.WriteLine(line + "Pog");
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
            human.health = HuHealth;
            Console.WriteLine(Convert.ToInt32(HuHealth));
            UnderAttack = HAstar.Any(h => h.attacking);
            if (UnderAttack == false)
            {
                //HuHealth = human.maxhealth;
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
            HuHealth =+ 4;
        }
    }
}