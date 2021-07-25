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


namespace FarmWars
{
    public partial class FormGame : Form
    {
        public bool[] Drawn = new bool[6];
        int MapSize = 100;
        int SquareSize = 25;
        int XCord = 0;
        int YCord = 0;
        int TrX;
        int TrY;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);


        bool feild;
        int StartGo = 0;
        bool MapDrawn = false;
        int TileCount;
        public int Traversible = 1;
        int mapwidth = 0;
        int XPos;
        int YPos;
        public int PathPos = 0;
        public int PathLoc = 0;
        public int HuPathLoc = 0;
        public int HuPathPos = 0;
        public int HuHealth = 100;
        public int HosHealth = 100;
        bool LShift = false;
        bool RShift = false;
        bool MouseDrag = false;
        bool GoneShopping = false;
        bool InGame = true;
        bool InMenu = false;

        public bool HuDrawn;
        public bool HostileDrawn = false;


        int AX;
        int AY;
        int BX;
        int BY;

        int HuAX = 1;
        int HuAY = 1;
        int HuBX;
        int HuBY;

        square squareTile = new square();
        Feild feildTile = new Feild();
        water waterTile = new water();
        Lake lakeTile = new Lake();
        HostileAstar[] HAstar = new HostileAstar[6];
        HumanAstar HuAstar = new HumanAstar();

        Inventory inventory = new Inventory();
        Shop shop = new Shop();
        Human human = new Human();

        //Calculate the width and height of the square so that it all fits in the picturebox
        public int width = 0;
        public int height = 0;

        //Calcualte the X and Y of each indervidual square
        int PosX = 0;
        int PosY = 0;

        int TextPosX = 0;
        int TextPosY = 0;

        //Create list to store marks
        public List<double> tileList = new List<double>();
        public List<double> tileType = new List<double>();

        //i, j, value
        List<Tuple<int, int, int, int, int>> newTileMap = new List<Tuple<int, int, int, int, int>>();
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

            DrawMap();

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
            inventory.DrawButtons();
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

                    TrX = XCord;
                    TrY = YCord;

                    if (XPos >= -50 + HuAstar.xLoc && YPos >= -50 + HuAstar.yLoc && XPos <= 75 + HuAstar.xLoc && YPos <= 75 + HuAstar.yLoc)
                    {
                        feildTile.SquareSize = SquareSize;

                        feildTile.TrX = TrX;
                        feildTile.TrY = TrY;

                        feildTile.DrawFeild(f);
                    }
                }
            }
        }

        private void PnlGame_Click(object sender, EventArgs e)
        {
            Graphics g = PnlGame.CreateGraphics();
            if (InGame == true)
            {
                if (XPos >= 0 && YPos >= 0 && XPos <= 50 && YPos <= 50)
                {
                    PauseGame();
                }
                else if (XPos >= 50 && YPos >= 0 && XPos <= 100 && YPos <= 50)
                {
                    PlayGame();
                }
                else if (XPos >= 100 && YPos >= 0 && XPos <= 150 && YPos <= 50)
                {
                    ExitGame();
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
                if (XPos >= 100 && YPos >= 60 && XPos <= PnlGame.Width - 100 && YPos <= PnlGame.Height - 60)
                {
                    if (XPos >= 200 && YPos >= 360 && XPos <= 300 && YPos <= 390)
                    {
                        //Tater
                        inventory.Moneyz = inventory.Moneyz - 25;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 300 && YPos >= 360 && XPos <= 400 && YPos <= 390)
                    {
                        //Wheat
                        inventory.Moneyz = inventory.Moneyz - 35;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 400 && YPos >= 360 && XPos <= 500 && YPos <= 390)
                    {
                        //Corn
                        inventory.Moneyz = inventory.Moneyz - 50;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 500 && YPos >= 360 && XPos <= 600 && YPos <= 390)
                    {
                        //Carrot
                        inventory.Moneyz = inventory.Moneyz - 100;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 600 && YPos >= 360 && XPos <= 700 && YPos <= 390)
                    {
                        //Turnip
                        inventory.Moneyz = inventory.Moneyz - 150;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 800 && YPos >= 360 && XPos <= 900 && YPos <= 390)
                    {
                        //Sell Tater
                        inventory.Moneyz = inventory.Moneyz + 45;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 900 && YPos >= 360 && XPos <= 1000 && YPos <= 390)
                    {
                        //Sell Wheat
                        inventory.Moneyz = inventory.Moneyz + 60;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 1000 && YPos >= 360 && XPos <= 1100 && YPos <= 390)
                    {
                        //Sell Corn
                        inventory.Moneyz = inventory.Moneyz + 100;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 1200 && YPos >= 360 && XPos <= 1300 && YPos <= 390)
                    {
                        //Sell Carrot
                        inventory.Moneyz = inventory.Moneyz + 150;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 1300 && YPos >= 360 && XPos <= 1400 && YPos <= 390)
                    {
                        //Sell Turnip
                        inventory.Moneyz = inventory.Moneyz - 250;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 200 && YPos >= 690 && XPos <= 300 && YPos <= 720)
                    {
                        //Fence
                        inventory.Moneyz = inventory.Moneyz - 25;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 300 && YPos >= 690 && XPos <= 400 && YPos <= 720)
                    {
                        //Wall
                        inventory.Moneyz = inventory.Moneyz - 100;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 400 && YPos >= 690 && XPos <= 500 && YPos <= 720)
                    {
                        //Iron Sword
                        inventory.Moneyz = inventory.Moneyz - 300;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 500 && YPos >= 690 && XPos <= 600 && YPos <= 720)
                    {
                        //Beast Sword
                        inventory.Moneyz = inventory.Moneyz - 600;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 600 && YPos >= 690 && XPos <= 700 && YPos <= 720)
                    {
                        //Iron Sword
                        inventory.Moneyz = inventory.Moneyz - 350;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 700 && YPos >= 690 && XPos <= 800 && YPos <= 720)
                    {
                        //Beast Sword
                        inventory.Moneyz = inventory.Moneyz - 700;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 800 && YPos >= 690 && XPos <= 900 && YPos <= 720)
                    {
                        //Health Potion
                        inventory.Moneyz = inventory.Moneyz - 250;
                        inventory.UpdateMoneyz(g);
                    }
                    if (XPos >= 1000 && YPos >= 690 && XPos <= 1100 && YPos <= 720)
                    {
                        //Useless Potion
                        inventory.Moneyz = inventory.Moneyz - 500;
                        inventory.UpdateMoneyz(g);
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
            for (int i = 0; i < 6; i++)
            {
                Graphics g = PnlGame.CreateGraphics();
                PathPos++;
                PathLoc++;

                HAstar[i].PathLoc = PathLoc;
                HAstar[i].PathPos = PathPos;
                HAstar[i].PathFollow(g, i);
                PnlGame.Invalidate();
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

        private void PlayGame()
        {
            TmrHosMovement.Enabled = true;
        }

        private void PauseGame()
        {
            TmrHosMovement.Enabled = true;
        }

        private void TmrTurn_Tick(object sender, EventArgs e)
        {
            int Max = ((PnlGame.Width / 25) * 2) + ((PnlGame.Height / 25) * 2);
            Random Go = new Random();
            int StartPos = Go.Next(1, Max);
            StartGo = Go.Next(1, 4);
            if (StartGo == 6)
            {
                if (StartPos * 25 > PnlGame.Width + PnlGame.Width + PnlGame.Height)
                {
                    int EdgePosY = StartPos - (PnlGame.Width / 25) - (PnlGame.Width / 25) - (PnlGame.Height / 25);
                    int EdgePosX = 0;
                    DrawHostile(EdgePosX, EdgePosY);

                }
                if (StartPos * 25 > PnlGame.Width + PnlGame.Height)
                {
                    int EdgePosX = StartPos - ((PnlGame.Width) / 25) - ((PnlGame.Height) / 25);
                    int EdgePosY = 0;
                    DrawHostile(EdgePosX, EdgePosY);

                }
                if (StartPos * 25 > PnlGame.Width)
                {
                    int EdgePosY = StartPos - (PnlGame.Width / 25);
                    int EdgePosX = 0;
                    DrawHostile(EdgePosX, EdgePosY);

                }
                else
                {
                    int EdgePosX = StartPos;
                    int EdgePosY = 0;
                    DrawHostile(EdgePosX, EdgePosY);
                }

            }
        }

        private void DrawHostile(int XCord, int YCord)
        {
            for (int i = 0; i <= 0; i++)
            {
                if (Drawn[i] == true)
                {
                    PathPos = 0;
                    PathLoc = 0;
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
                    HAstar[i].DrawHostile(g);

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

            private void TmrHumMovement_Tick(object sender, EventArgs e)
        {
            Graphics g = PnlGame.CreateGraphics();
            HuPathPos++;
            HuPathLoc++;

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
                if (HAstar[i].xLoc >= -50 + HuAstar.xLoc && HAstar[i].yLoc >= -50 + HuAstar.yLoc && HAstar[i].xLoc <= 75 + HuAstar.xLoc && HAstar[i].yLoc <= 75 + HuAstar.yLoc)
                {
                    int health = HuHealth - 2;
                    human.DrawHealth(g);
                    Console.WriteLine(health);
                    HuHealth = health;
                }
            }
            for (int i = 0; i < 6; i++)
            {
                if (HuAstar.xLoc >= -50 + HAstar[i].xLoc && HuAstar.yLoc >= -50 + HAstar[i].yLoc && HuAstar.xLoc <= 75 + HAstar[i].xLoc && HuAstar.yLoc <= 75 + HAstar[i].yLoc)
                {
                    int healthhos = HosHealth - 10;
                    HAstar[i].DrawHealth(g);
                    Console.WriteLine(healthhos);
                    HosHealth = healthhos;
                }
                if (HosHealth <= 0)
                {
                    PauseGame();
                }
            }
        }
    }
}