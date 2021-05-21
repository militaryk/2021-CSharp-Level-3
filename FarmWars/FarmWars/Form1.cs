using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        square squareTile = new square();
        Feild feildTile = new Feild();
        water waterTile = new water();

        //Calculate the width and height of the square so that it all fits in the picturebox
        public int width = 0;
        public int height = 0;

        //Calcualte the X and Y of each indervidual square
        int PosX = 0;
        int PosY = 0;

        int TextPosX = 0;
        int TextPosY = 0;


        public FormGame()
        {
            InitializeComponent();
        }

        private void TmrGame_Tick(object sender, EventArgs e)
        {        }

        private void DrawMap()
        {
          
        }

        private void PnlGame_Paint(object sender, PaintEventArgs e)
        {
            if (MapDraw == true)
            {

                //Draw the grid with the number of columns given
                for (int f = 0; f * 25 < PnlGame.Width; f++)
                {
                    //For each row until the number of rows is the same as the number of rows entered by the user
                    for (int i = 0; PnlGame.Height > i * 25; i++)
                    {
                        Graphics g = PnlGame.CreateGraphics();

                        squareTile.height = SquareSize;
                        squareTile.width = SquareSize;
                        squareTile.f = f;
                        squareTile.i = i;

                        squareTile.DrawSqaure(g);


                        waterTile.height = SquareSize;
                        waterTile.width = SquareSize;
                        waterTile.f = f;
                        waterTile.i = i;
                        waterTile.DrawWater(g);
                        Thread.Sleep(2);

                    }
                }
                MapDraw = false;
            }

        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            DrawMap();
        }

        private void PnlGame_MouseMove(object sender, MouseEventArgs e)
        {
            XCord = (e.X / 25);
            YCord = (e.Y / 25);
            LblCord.Text = Convert.ToString(XCord);
            LblCordY.Text = Convert.ToString(YCord);
            label1.Text = Convert.ToString(e.X);
            label2.Text = Convert.ToString(e.Y);
        }

        private void PnlGame_Click(object sender, EventArgs e)
        {
            TrX = XCord;
            TrY = YCord;
            label3.Text = Convert.ToString(TrX);
            label4.Text = Convert.ToString(TrY);

            feildTile.SquareSize = SquareSize;

            feildTile.TrX = TrX;
            feildTile.TrY = TrY;

            Graphics g = PnlGame.CreateGraphics();

            feildTile.DrawFeild(g);

        }
    }
}
