using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
namespace FarmWars
{
    class Inventory
    {
        int SqaureSize = 25;
        int ScrollWidth = 8;
        int ScrollHeight = 22;
        public int SqaureWidth = 3;
        public int SqaureHeight = 6;
        public int PosX;
        public int PosY;
        public int InvX;
        public int InvY;
        public string itemselected = "Tater";
        public int width;
        public int height;
        public int Moneyz = 1000;
        public int score;
        public int PnlWidth;
        public int PnlHeight;
        public string selitemname = "";
        public bool nextturn = true;
        public bool Rshift = false;

       public  List<Tuple<string, int>> inventory = new List<Tuple<string, int>>();

        public void DrawMenu(Graphics g)
        {
            Rectangle rect = new Rectangle(0, 0, PnlWidth, PnlHeight);
            Image menuImage = Image.FromFile("../../../Art/menubackground.png");
            g.DrawImage(menuImage, rect);
        }
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

                InvX = PnlWidth - width + 25;
                InvY = PosY + 125;

                int numofinv = 0;

                // Create rectangle for ellipse.
                Rectangle rect = new Rectangle(PosX, PosY, width, height);

                Thread.Sleep(10);
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
                            if (itemamount > 0)
                            {
                                int itemid = ItemDict(itemname);
                                string itempict = ItemPictDict(itemid);
                                Image ItemImage = Image.FromFile(itempict);
                                g.DrawImage(ItemImage, inv);
                                g.DrawString(itemamount.ToString(), invfont, br, invamo);
                            }
                            numofinv += 1;

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
            if (selitemname != "")
            {
                DrawSelectSquare(g, selitemname);

            }
            DrawUI(g);
            DrawNextTurn(g);
            DrawBuildIcon(g);
            DrawScore(g);
        }

        public void DrawNextTurn(Graphics g)
        {
            if (nextturn == true)
            {
                Rectangle rectnt = new Rectangle(PnlWidth - 290, PnlHeight - 125, 270, 105);
                Image newImagent = Image.FromFile("../../../Art/nextturn.png");
                g.DrawImage(newImagent, rectnt);
            }
        }

        public void DrawBuildIcon(Graphics g)
        {
            if (Rshift == true){
                Rectangle rectbd = new Rectangle(SqaureSize * 2, PnlHeight - SqaureSize * 10, SqaureSize * 4, SqaureSize * 4);
                Image newImagent = Image.FromFile("../../../Art/buildmode.png");
                g.DrawImage(newImagent, rectbd);
            }
        }

        private void DrawUI(Graphics g)
        {
            width = SqaureSize;
            height = SqaureSize;

            Rectangle iconPause = new Rectangle(SqaureSize * 0, 0, width * 2, height * 2);
            Rectangle iconCoin = new Rectangle(SqaureSize * 12, 0, width * 2, height * 2);



            Image PauseImage = Image.FromFile("../../../Art/ui/pause.png");
            Image CoinImage = Image.FromFile("../../../Art/ui/Coin.png");


            g.DrawImage(PauseImage, iconPause);
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

        private void DrawScore(Graphics g)
        {
            Rectangle RecScore = new Rectangle((SqaureSize * 20) + 15, 0, width * 12, height * 2);
            Font drawFont = new Font("Arial", 32);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            g.DrawString("Score:" + score.ToString(), drawFont, drawBrush, RecScore);
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

        public void DrawSelectSquare(Graphics g, string itemname)
        {
            int itemid;
            string itempict;
            itemid = ItemDict(itemname);
            itempict = ItemPictDict(itemid);
            itemselected = itemname;

            SolidBrush whitebr = new SolidBrush(Color.Wheat);

            Image ItemImage = Image.FromFile(itempict);

            Rectangle selectSquare = new Rectangle(SqaureSize * 2, PnlHeight - SqaureSize * 6, SqaureSize * 4, SqaureSize * 4);

            g.FillRectangle(whitebr, selectSquare);
            g.DrawImage(ItemImage, selectSquare);
        }

    }
}
