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
        int MapSize = 100;
        int SquareSize = 25;
        int XCord = 0;
        int YCord = 0;
        int TrX;
        int TrY;
        bool feild;
        bool MapDrawn = false;
        int TileCount;
        public int Traversible = 1;
        int mapwidth = 0;

        int AX;
        int AY;
        int BX;
        int BY;

        square squareTile = new square();
        Feild feildTile = new Feild();
        water waterTile = new water();
        Lake lakeTile = new Lake();
        Astar astar = new Astar();
        Hostile hostile = new Hostile();

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
            astar.EmptyList();

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
                    Graphics g = PnlGame.CreateGraphics();

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
                            TextRenderer.DrawText(g, "A", font, point1, Color.Blue);
                        }
                    }
                    else if (x == BX && y == BY)
                    {
                        using (Font font = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel))
                        {
                            Point point1 = new Point(BX * 25, BY * 25);
                            TextRenderer.DrawText(g, "B", font, point1, Color.Blue);
                        }
                        line = line + "B";
                    }
                    else if(tiletype == 2)
                    {
                        line = line + "-";
                    } else
                    {
                        line = line + " ";
                    }
                }
                astar.AddToList(line);
                Console.WriteLine(line);
                mapwidth = line.Length;
                line = "";
            }
            SaveMap();
        }

        private void SaveMap()
        {
            int width = PnlGame.Size.Width;
            int height = PnlGame.Size.Height;
            Bitmap bm = new Bitmap(width, height);
            this.Background = bm;
            PnlGame.DrawToBitmap(bm, new Rectangle(0, 0, width, height));
            MapDrawn = true;
            PnlGame.Invalidate();
        }

        private void PnlGame_Paint(object sender, PaintEventArgs e)
        {
            if (MapDrawn == true)
            {
                //Graphics g = PnlGame.CreateGraphics();
                var g = e.Graphics;
                g.DrawImage(Background, 0, 0);
            }
        }
        private void PnlGame_MouseMove(object sender, MouseEventArgs e)
        {
            XCord = (e.X / 25);
            YCord = (e.Y / 25);

        }

        private void PnlGame_Click(object sender, EventArgs e)
        {
            astar.EmptyList();

            Graphics g = PnlGame.CreateGraphics();

            AY = YCord;
            AX = XCord;
            astar.StartX = AX;
            astar.StartY = AY;
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
                    else if (tiletype == 2)
                    {
                        line = line + "-";
                    }
                    else
                    {
                        line = line + " ";
                    }

                }
                astar.AddToList(line);
                Console.WriteLine(line);
                line = "";

            }
            astar.Main(g);


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
            Graphics g = PnlGame.CreateGraphics();

            TrX = XCord;
            TrY = YCord;


            feildTile.SquareSize = SquareSize;

            feildTile.TrX = TrX;
            feildTile.TrY = TrY;

            feildTile.DrawFeild(g);
        }

        private void TmrGame_Tick(object sender, EventArgs e)
        {
            int currentX = XCord;
            int currentY = YCord;
            astar.CurrentX = currentX;
            astar.CurrentY = currentY;
        }

        private void moveGuyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics g = PnlGame.CreateGraphics();
            astar.FollowPath(g);
        }
    }
}