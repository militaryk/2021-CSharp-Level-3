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


            //Define the pen with the colour black
            Pen pen1 = new Pen(Color.Black);

            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(x, y, width, height);
            Image newImage = Image.FromFile("../../../Art/shop.png");
            g.DrawImage(newImage, rect);

        }

        public void DrawShopMenu(Graphics g)
        {
            SolidBrush br = new SolidBrush(Color.Peru);

            Rectangle ShowMenu = new Rectangle(100, 60, ((FormGame)FormGame.ActiveForm).PnlGame.Width - 200, ((FormGame)FormGame.ActiveForm).PnlGame.Height - 120);
            g.FillRectangle(br, ShowMenu);

        }
    }
}
