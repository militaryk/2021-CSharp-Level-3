using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace FarmWars
{
    class Shop
    {
        public int width = 100;
        public int height = 80;
        public int tiletype;
        public void DrawShop(Graphics g, int y, int x)
        {
            //Define the solid brush with a default colour of orange
            SolidBrush br = new SolidBrush(Color.SandyBrown);

            //Define the pen with the colour black
            Pen pen1 = new Pen(Color.Black);

            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(x, y, width, height);
            Image newImage = Image.FromFile("../../../Art/shop.png");
            g.DrawImage(newImage, rect);

        }
    }
}
