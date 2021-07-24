using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FarmWars
{
    class Inventory
    {
        int SqaureSize = 25;
        int SqaureWidth = 8;
        int SqaureHeight = 22;
        int PosX;
        int PosY;
        int InvX;
        int InvY;
        int width;
        int height;
        public int Moneyz = 1000;
        public int PnlWidth;
        public int PnlHeight;
        public void DrawInventory(Graphics g)
        {
            try
            {
                width = SqaureSize * SqaureWidth;
                height = SqaureSize * SqaureHeight;

                //Define the solid brush with a default colour of orange
                SolidBrush br = new SolidBrush(Color.SandyBrown);

                //Define the pen with the colour black
                Pen pen1 = new Pen(Color.Sienna);

                //Calcualte the X and Y of each indervidual square
                PosX = PnlWidth - width;
                PosY = PnlHeight - height - ((PnlHeight / 5));

                InvX = PnlWidth - width + (SqaureSize / 3);
                InvY = PosY + 125;


                // Create rectangle for ellipse.
                Rectangle rect = new Rectangle(PosX, PosY, width, height);

                Image newImage = Image.FromFile("../../../Art/ui/scroll.png");
                g.DrawImage(newImage, rect);

                for (int i = 0; i < SqaureWidth - 1; i++)
                {
                    for (int j = 0; j < SqaureHeight - 11; j++)
                    {
                        Rectangle inv = new Rectangle(InvX, InvY, SqaureSize, SqaureSize);
                        g.DrawRectangle(pen1, inv);
                        InvY = InvY + SqaureSize;
                    }
                    InvY = PosY + 125;
                    InvX = InvX + SqaureSize;
                }
                DrawUI(g);
                DrawNextTurn(g);
            } catch
            {

            }
        }

        public void DrawNextTurn(Graphics g)
        {
            Rectangle rectnt = new Rectangle(((FormGame)FormGame.ActiveForm).PnlGame.Width - 290, ((FormGame)FormGame.ActiveForm).PnlGame.Height - 125, 270, 105);
            Image newImagent = Image.FromFile("../../../Art/nextturn.png");
            g.DrawImage(newImagent, rectnt);
        }
        public void DrawButtons()
        {
        //    try
        //    {
        //        ((FormGame)FormGame.ActiveForm).BtnExit.Visible = true;
        //       ((FormGame)FormGame.ActiveForm).BtnExit.Width = width - 25;
        //        ((FormGame)FormGame.ActiveForm).BtnExit.Height = height / 10;
        //        ((FormGame)FormGame.ActiveForm).BtnExit.Location = new Point(PosX + 30, PosY + 490);
        //        ((FormGame)FormGame.ActiveForm).BtnExit.TabStop = false;
        //        ((FormGame)FormGame.ActiveForm).BtnExit.FlatStyle = FlatStyle.Flat;
        //       ((FormGame)FormGame.ActiveForm).BtnExit.FlatAppearance.BorderSize = 0;
        //    }
        //    catch { }
        }

        private void DrawUI(Graphics g)
        {
            width = SqaureSize;
            height = SqaureSize;

            Rectangle iconPause = new Rectangle(SqaureSize * 0, 0, width * 2, height * 2);
            Rectangle iconPlay = new Rectangle(SqaureSize * 2, 0, width * 2, height * 2);
            Rectangle iconExit = new Rectangle(SqaureSize * 4, 0, width * 2, height * 2);
            Rectangle iconCoin = new Rectangle(SqaureSize * 12, 0, width * 2, height * 2);


            Image PauseImage = Image.FromFile("../../../Art/ui/pause.png");
            Image PlayImage = Image.FromFile("../../../Art/ui/play.png");
            Image ExitImage = Image.FromFile("../../../Art/ui/exit.png");
            Image CoinImage = Image.FromFile("../../../Art/ui/Coin.png");


            g.DrawImage(PauseImage, iconPause);
            g.DrawImage(PlayImage, iconPlay);
            g.DrawImage(ExitImage, iconExit);
            g.DrawImage(CoinImage, iconCoin);
            DrawMoneyz(g);
        }

        private void DrawMoneyz(Graphics g) {
            Rectangle RecMoneyz = new Rectangle((SqaureSize * 13)+15, 0, width * 5, height * 2);
            Font drawFont = new Font("Arial", 32);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            g.DrawString(Moneyz.ToString(), drawFont, drawBrush, RecMoneyz);
        }
    }
}
