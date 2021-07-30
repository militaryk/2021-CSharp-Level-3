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
        int ScrollWidth = 8;
        int ScrollHeight = 22;
        int SqaureWidth = 3;
        int SqaureHeight = 6;
        public int PosX;
        public int PosY;
        int InvX;
        int InvY;
        public int width;
        public int height;
        public int Moneyz = 1000;
        public int PnlWidth;
        public int PnlHeight;

       public  List<Tuple<string, int>> inventory = new List<Tuple<string, int>>();

        public void DrawInventory(Graphics g)
        {
            try
            {
                width = SqaureSize * ScrollWidth;
                height = SqaureSize * ScrollHeight;

                //Define the solid brush with a default colour of orange
                SolidBrush br = new SolidBrush(Color.Black);

                //Define the pen with the colour black
                Pen pen1 = new Pen(Color.Sienna);

                //Calcualte the X and Y of each indervidual square
                PosX = PnlWidth - width;
                PosY = PnlHeight - height - ((PnlHeight / 5));

                InvX = PnlWidth - width;
                InvY = PosY + 125;

                int numofinv = 0;

                // Create rectangle for ellipse.
                Rectangle rect = new Rectangle(PosX, PosY, width, height);

                Image newImage = Image.FromFile("../../../Art/ui/scroll.png");
                g.DrawImage(newImage, rect);

                for (int i = 0; i < SqaureWidth; i++)
                {
                    for (int j = 0; j < SqaureHeight; j++)
                    {
                        Rectangle inv = new Rectangle(InvX, InvY, 50, 50);
                        Rectangle invamo = new Rectangle(InvX, InvY + 20, 50, 30);

                        Font invfont = new Font("Arial", 20);


                        g.DrawRectangle(pen1, inv);
                        InvY = InvY + 50;

                        if (numofinv < inventory.Count)
                        {
                            string itemname = inventory[numofinv].Item1;
                            int itemamount = inventory[numofinv].Item2;
                            int itemid = ItemDict(itemname);
                            string itempict = ItemPictDict(itemid);
                            Console.WriteLine(itempict);
                            numofinv += 1;
                            Image ItemImage = Image.FromFile(itempict);
                            g.DrawImage(ItemImage, inv);
                            g.DrawString(itemamount.ToString(), invfont, br, invamo);
                        }
                        else {
                        }
                    }
                    InvY = PosY + 125;
                    InvX = InvX + 50;
                }
            } catch
            {

            }

            DrawUI(g);
            DrawNextTurn(g);
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

        public void UpdateMoneyz(Graphics g)
        {
            ((FormGame)FormGame.ActiveForm).PnlGame.Invalidate();
            DrawMoneyz(g);
        }
        private void DrawMoneyz(Graphics g) {
            Rectangle RecMoneyz = new Rectangle((SqaureSize * 13)+15, 0, width * 5, height * 2);
            Font drawFont = new Font("Arial", 32);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            g.DrawString(Moneyz.ToString(), drawFont, drawBrush, RecMoneyz);
        }

        private int ItemDict(string itemname)
        {
            int itemid = 0;
            if (itemname == "Tater")
            {
                itemid = 1;
            }
            if (itemname == "Wheat")
            {
                itemid = 2;
            }
            if (itemname == "Corn")
            {
                itemid = 3;
            }
            if (itemname == "Carrot")
            {
                itemid = 4;
            }
            if (itemname == "Turnip")
            {
                itemid = 5;
            }
            if (itemname == "Fence")
            {
                itemid = 6;
            }
            if (itemname == "Wall")
            {
                itemid = 7;
            }
            if (itemname == "HealthPot")
            {
                itemid = 8;
            }
            if (itemname == "UselessPot")
            {
                itemid = 9;
            }
            if (itemname == "TaterSeed")
            {
                itemid = 10;
            }
            if (itemname == "WheatSeed")
            {
                itemid = 11;
            }
            if (itemname == "CornSeed")
            {
                itemid = 12;
            }
            if (itemname == "CarrotSeed")
            {
                itemid = 13;
            }
            if (itemname == "TurnipSeed")
            {
                itemid = 14;
            }
            return itemid;
        }
        private string ItemPictDict(int itemid)
        {
            string itempict = "url/";
            if (itemid == 1)
            {
                //Tater
                itempict = "../../../Art/Items/Tater.png";
            }
            if (itemid == 2)
            {
                //Wheat
                itempict = "../../../Art/Items/Wheat.png";
            }
            if (itemid == 3)
            {
                //Corn
                itempict = "../../../Art/Items/Corn.png";
            }
            if (itemid == 4)
            {
                //Carrot
                itempict = "../../../Art/Items/Carrot.png";
            }
            if (itemid == 5)
            {
                //Turnip
                itempict = "../../../Art/Items/Turnip.png";
            }
            if (itemid == 6)
            {
                //Fence
                itempict = "../../../Art/Items/fence.png";
            }
            if (itemid == 7)
            {
                //Wall
                itempict = "../../../Art/Items/Wall.png";
            }
            if (itemid == 8)
            {
                //HealthPot
                itempict = "../../../Art/Items/HealthPotion.png";
            }
            if (itemid == 9)
            {
                //UselessPot
                itempict = "../../../Art/Items/UselessPotion.png";
            }
            if (itemid == 10)
            {
                //TaterSeed
                itempict = "../../../Art/Items/tatersseed.png";
            }
            if (itemid == 11)
            {
                //WheatSeed
                itempict = "../../../Art/Items/wheatseed.png";
            }
            if (itemid == 12)
            {
                //CornSeed
                itempict = "../../../Art/Items/cornseed.png";
            }
            if (itemid == 13)
            {
                //CarrotSeed
                itempict = "../../../Art/Items/carrotseed.png";
            }
            if (itemid == 14)
            {
                //TurnipSeed
                itempict = "../../../Art/Items/turnipseed.png";
            }
            return itempict;
        }

    }
}
