using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FarmWars
{
    class Square
    {
        public int width;
        public int height;
        public int y;
        public int x;
        public int tiletype;

        public void DrawSqaure(Graphics g)
        {
            //Define the solid brush with a default colour of orange
            SolidBrush br = new SolidBrush(Color.SandyBrown);

            //Define the pen with the colour black
            Pen pen1 = new Pen(Color.Black);

            //Calcualte the X and Y of each indervidual square
            int PosX = width * x;
            int PosY = height * y;

            int TextPosX = PosX + width / 2;
            int TextPosY = PosY + width / 2;

            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(PosX, PosY, width, height);
            if (tiletype == 1)
            {
                Image newImage = Image.FromFile("../../../Art/ground/tallgrass.png");
                g.DrawImage(newImage, rect);

            }
            if (tiletype == 2)
            {
                Image newImage = Image.FromFile("../../../Art/ground/tallgrass2.png");
                g.DrawImage(newImage, rect);

            }
            if (tiletype == 3)
            {
                Image newImage = Image.FromFile("../../../Art/ground/tallgrass3.png");
                g.DrawImage(newImage, rect);

            }
            if (tiletype == 4)
            {
                Image newImage = Image.FromFile("../../../Art/ground/tallgrass4.png");
                g.DrawImage(newImage, rect);

            }
            if (tiletype == 5)
            {
                Image newImage = Image.FromFile("../../../Art/ground/tallgrass5.png");
                g.DrawImage(newImage, rect);

            }

        }
    }
}
