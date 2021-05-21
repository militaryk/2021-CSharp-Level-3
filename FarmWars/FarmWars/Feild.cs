using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FarmWars
{
    class Feild
    {
        public int width;
        public int height;
        public int TrY;
        public int TrX;
        public int SquareSize;
        int clicked = 0;

        int PosX;
        int PosY;
        int TextPosX;
        int TextPosY;
        public void DrawFeild(Graphics g)
        {
            //Define the solid brush with a default colour of orange
            SolidBrush br = new SolidBrush(Color.SandyBrown);

            //Define the pen with the colour black
            Pen pen1 = new Pen(Color.Black);
            Image YouImage = Image.FromFile("../../../Art/youngwheat.png");


            //Calculate the width and height of the square so that it all fits in the picturebox
            int width = SquareSize;
            int height = SquareSize;

            //Calcualte the X and Y of each indervidual square
            PosX = width * TrX;
            PosY = height * TrY;

            TextPosX = PosX + width / 2;
            TextPosY = PosY + width / 2;



            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(PosX, PosY, width, height);

            g.DrawImage(YouImage, rect);


            GrowWheat(g);
        }

        public void GrowWheat(Graphics g)
        {
            Thread.Sleep(2000);
            Image GrownImage = Image.FromFile("../../../Art/grownwheat.png");
            Rectangle rect = new Rectangle(PosX, PosY, 25, 25);
            g.DrawImage(GrownImage, rect);
        }
    }
}
