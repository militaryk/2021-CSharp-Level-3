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
    class water
    {
        public int width;
        public int height;
        public int i;
        public int f;
        public int waterchance;

        public void DrawWater(Graphics g)
        {
            Random water = new Random();
            int waterchance = water.Next(1, 10);

            if (waterchance == 1)
            {
                //Define the solid brush with a default colour of orange
                SolidBrush br = new SolidBrush(Color.SandyBrown);

                //Define the pen with the colour black
                Pen pen1 = new Pen(Color.Black);

                //Calcualte the X and Y of each indervidual square
                int PosX = width * f;
                int PosY = height * i;


                int TextPosX = PosX + width / 2;
                int TextPosY = PosY + width / 2;

                // Create rectangle for ellipse.
                Rectangle rect = new Rectangle(PosX, PosY, width, height);

                Image newImage = Image.FromFile("../../../Art/water.jpg");
                g.DrawImage(newImage, rect);
            }
        }
    }
}
