using System.Drawing;

namespace FarmWars
{
    class Hostile
    {
        public int width = 20;
        public int height = 25;
        public int y;
        public int x;
        public int tiletype;
        //HostileAstar HAstar = new HostileAstar();
        public void DrawHostile(Graphics g)
        {
            //Define the solid brush with a default colour of orange
            SolidBrush br = new SolidBrush(Color.SandyBrown);

            //Define the pen with the colour black
            Pen pen1 = new Pen(Color.Black);


            // Create pen.
            Pen blackPen = new Pen(Color.Red, 3);

            // Create location and size of ellipse.            
            int widthc = 75;
            int heightc = 75;

            // Draw ellipse to screen.
            g.DrawEllipse(blackPen, x - (25), y - (25), widthc, heightc);

            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(x, y, width, height);
            Image newImage = Image.FromFile("../../../Art/character/bad.png");
            g.DrawImage(newImage, rect);

        }
    }
}
