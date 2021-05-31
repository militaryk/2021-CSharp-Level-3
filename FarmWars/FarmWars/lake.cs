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
    class Lake
    {
        public int width;
        public int height;
        public int y;
        public int x;
        public int waterchance;
        public int PosX;
        public int PosY;
        public int LX;
        public int LY;
        public int RX;
        public int RY;
        public int UX;
        public int UY;
        public int DX;
        public int DY;
        public int lakelength;
        public int lakeheight;
        bool newlake = true;
        Image newImage = Image.FromFile("../../../Art/ground/water.jpg");

        
        public void DrawLake(Graphics g)
        {
            Random water = new Random();
            int waterchance = water.Next(1, 100);

            if (waterchance == 1)
            {
                Random rndlength = new Random();
                lakelength = rndlength.Next(1, 5);

                Random rndheight = new Random();
                lakeheight = rndheight.Next(1, 5);

                WaterCentre(g);

            }

        }

        public void WaterCentre(Graphics g)
        {
            //Define the solid brush with a default colour of orange
            SolidBrush br = new SolidBrush(Color.SandyBrown);

            //Define the pen with the colour black
            Pen pen1 = new Pen(Color.Black);

            Thread.Sleep(2);

            for (int l = 0; l < lakelength; l ++)
            {
                //For each row until the number of rows is the same as the number of rows entered by the user
                for (int u = 0; lakeheight > u; u++)
                {
                    //Calcualte the X and Y of each indervidual square
                    PosX = width * (x + l);
                    PosY = height * (y + u);


                    // Create rectangle for ellipse.
                    Rectangle rect = new Rectangle(PosX, PosY, width, height);
                    g.DrawImage(newImage, rect);
                }
            }

        }

        private void GetNearest()
        {
            LX = x - 1;
            LY = y;
            RX = x + 1;
            RY = y;
            UX = x;
            UY = y + 1;
            DX = x;
            DY = y - 1;
        }
    }
}
