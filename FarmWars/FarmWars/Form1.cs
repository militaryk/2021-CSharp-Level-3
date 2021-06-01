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
        bool MapDraw = true;
        int TileCount;
        public int Traversible = 1;

        square squareTile = new square();
        Feild feildTile = new Feild();
        water waterTile = new water();
        Lake lakeTile = new Lake();

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


        public FormGame()
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, PnlGame, new object[] { true });

        }

        private void DrawMap()
        {
                //Draw the grid with the number of columns given
                for (int x = 0; x * SquareSize < PnlGame.Width; x++)
                {
                    //For each row until the number of rows is the same as the number of rows entered by the user
                    for (int y = 0; PnlGame.Height > y * SquareSize; y++)
                    {
                        
                        TileCount++;
                        Graphics g = PnlGame.CreateGraphics();

                        Random tile = new Random();
                        int tiletype = tile.Next(1, 6);

                        

                        squareTile.height = SquareSize;
                        squareTile.width = SquareSize;
                        squareTile.x = x;
                        squareTile.y = y;
                        squareTile.tiletype = tiletype;

                        newTileMap.Add(new Tuple<int, int, int, int, int>(x, y, tiletype, Traversible, 0));


                    squareTile.DrawSqaure(g);
                        Thread.Sleep(5);

                }
            }

        }

        private void PnlGame_Paint(object sender, PaintEventArgs e)
        {
        }
        private void PnlGame_MouseMove(object sender, MouseEventArgs e)
        {
            XCord = (e.X / 25);
            YCord = (e.Y / 25);

        }

        private void PnlGame_Click(object sender, EventArgs e)
        {
            TrX = XCord;
            TrY = YCord;


            feildTile.SquareSize = SquareSize;

            feildTile.TrX = TrX;
            feildTile.TrY = TrY;

            Graphics g = PnlGame.CreateGraphics();

            feildTile.DrawFeild(g);
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
    }
}