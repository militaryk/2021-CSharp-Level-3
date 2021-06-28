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
        public bool drawn = false;
        int MapSize = 100;
        int SquareSize = 25;
        int XCord = 0;
        int YCord = 0;
        int TrX;
        int TrY;
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

        public bool HostileDrawn = false;

        int AX;
        int AY;
        int BX;
        int BY;

        int HuAX;
        int HuAY;
        int HuBX;
        int HuBY;

        square squareTile = new square();
        Feild feildTile = new Feild();
        water waterTile = new water();
        Lake lakeTile = new Lake();
        HostileAstar HAstar = new HostileAstar();
        HumanAstar HuAstar = new HumanAstar();
        Hostile hostile = new Hostile();
        Inventory inventory = new Inventory();

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

        }

        private void DrawMap()
        {

            Background = new Bitmap(PnlGame.Width, PnlGame.Height);
            Graphics g = Graphics.FromImage(Background);
            HAstar.EmptyList();

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
                    else if (tiletype == 2)
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
                HAstar.AddToList(line);
                HuAstar.AddToList(line);
                Console.WriteLine(line);
                mapwidth = line.Length;
                line = "";
            }
            //SaveMap();
            MapDrawn = true;
            inventory.DrawButtons();
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
        }
        private void PnlGame_MouseMove(object sender, MouseEventArgs e)
        {
            XCord = (e.X / 25);
            YCord = (e.Y / 25);
            XPos = e.X;
            YPos = e.Y;
        }

        private void PnlGame_Click(object sender, EventArgs e)
        {
            Graphics g = PnlGame.CreateGraphics();

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

            HuAY = YCord;
            HuAX = XCord;
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
                        else if (tiletype == 2)
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

        private void reDrawMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawMap();
        }

        private void pathFindingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PnlGame_DoubleClick(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(Background);

            TrX = XCord;
            TrY = YCord;


            feildTile.SquareSize = SquareSize;

            feildTile.TrX = TrX;
            feildTile.TrY = TrY;

            feildTile.DrawFeild(g);
            PnlGame.Invalidate();
        }

        private void TmrGame_Tick(object sender, EventArgs e)
        {
            Graphics g = PnlGame.CreateGraphics();
            PathPos++;
            PathLoc++;
            HuPathPos++;
            HuPathLoc++;
            HAstar.PathLoc = PathLoc;
            HAstar.PathPos = PathPos;
            /HuAstar.PathLoc = HuPathLoc;
           //HuAstar.PathPos = HuPathPos;
            PnlGame.Invalidate();
            HAstar.PathFollow(g);
            HuAstar.PathFollow(g);
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
            TmrMovement.Enabled = true;
        }

        private void PauseGame()
        {
            TmrMovement.Enabled = false;
        }

        private void TmrTurn_Tick(object sender, EventArgs e)
        {
            int Max = ((PnlGame.Width / 25) * 2) + ((PnlGame.Height / 25) * 2);
            Random Go = new Random();
            int StartPos = Go.Next(1, Max);
            StartGo = Go.Next(1, 100);
            StartGo = 2;
            if (StartGo == 2){
                if (StartPos * 25 > PnlGame.Width + PnlGame.Width + PnlGame.Height)
                {
                    int EdgePosY = StartPos - (PnlGame.Width / 25) - (PnlGame.Width /25) - (PnlGame.Height /25);
                    int EdgePosX = 0;
                    DrawHostile(EdgePosX, EdgePosY);

                }
                if (StartPos * 25 > PnlGame.Width + PnlGame.Height)
                {
                    int EdgePosX = StartPos - ((PnlGame.Width) /25)- ((PnlGame.Height) /25);
                    int EdgePosY = 0;
                    DrawHostile(EdgePosX, EdgePosY);

                }
                if (StartPos  * 25 > PnlGame.Width)
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
            if (drawn == false)
            {
                PathPos = 0;
                PathLoc = 0;
                HAstar.EmptyList();

                Graphics g = PnlGame.CreateGraphics();

                AY = YCord;
                AX = XCord;
                HAstar.StartX = AX;
                HAstar.StartY = AY;
                int tiletype;
                string line = "";

                hostile.x = XCord * SquareSize;
                hostile.y = YCord * SquareSize;
                hostile.DrawHostile(g);

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
                            else if (tiletype == 2)
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
                    HAstar.AddToList(line);
                    Console.WriteLine(line);
                    line = "";

                }
                HAstar.Main(g);
            }
            drawn = true;
        }
    }
}