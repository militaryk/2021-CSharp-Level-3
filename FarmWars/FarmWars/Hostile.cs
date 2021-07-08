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
        public int health = 100;
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
            DrawHealth(g);
            DrawHealth(g);
        }
        public void DrawHealth(Graphics g)
        {
            int health = ((FormGame)FormGame.ActiveForm).HosHealth;
            SolidBrush grayBrush = new SolidBrush(Color.DarkGray);
            Rectangle HlBrect = new Rectangle(x - 10, y + 30, width + 20, 10);
            g.FillRectangle(grayBrush, HlBrect);

            SolidBrush healthBrush = new SolidBrush(Color.Yellow);

            if (health > 80 && health <= 100)
            {
                healthBrush = new SolidBrush(Color.Green);
            }
            else if (health > 40 && health <= 80)
            {
                healthBrush = new SolidBrush(Color.Yellow);
            }
            else if (health > 0 && health < 40)
            {
                healthBrush = new SolidBrush(Color.Red);
            }
            int healthlength = ((health) * 36 / 100);
            Rectangle Hlrect = new Rectangle(x - 8, y + 31, healthlength, 8);
            g.FillRectangle(healthBrush, Hlrect);
        }
    }
}
