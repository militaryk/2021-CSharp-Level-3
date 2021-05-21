﻿using System;
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
    class square
    {
        public int width;
        public int height;
        public int i;
        public int f;

        public void DrawSqaure(Graphics g)
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

                Image newImage = Image.FromFile("../../../Art/tallgrass.png");
                g.DrawImage(newImage, rect);
                using (Font font = new Font("Times New Roman", 12, FontStyle.Bold, GraphicsUnit.Pixel))
                {
                    Point point1 = new Point(TextPosX - 25, TextPosY);
                    TextRenderer.DrawText(g, Convert.ToString(f) + Convert.ToString(i), font, point1, Color.Blue);
                }
            }
        }
}
