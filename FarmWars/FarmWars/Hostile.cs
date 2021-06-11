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
    class Hostile
    {

        public int width = 20;
        public int height = 25;
        public int y;
        public int x;
        public int tiletype;
        //Astar astar = new Astar();
        public void DrawHostile(Graphics g)
        {
            //Define the solid brush with a default colour of orange
            SolidBrush br = new SolidBrush(Color.SandyBrown);

            //Define the pen with the colour black
            Pen pen1 = new Pen(Color.Black);

            //Calcualte the X and Y of each indervidual square



            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(x, y, width, height);
            Image newImage = Image.FromFile("../../../Art/character/bad.png");
            g.DrawImage(newImage, rect);

        }
    }
}
