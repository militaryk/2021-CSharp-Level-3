using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;


namespace FarmWars
{
    class Shop
    {
        public int width = 100;
        public int height = 80;
        public int tiletype;
        public int PnlHeight;
        public int PnlWidth;

        public void DrawShop(Graphics g, int y, int x)
        {
            //Define the pen with the colour black
            Pen pen1 = new Pen(Color.Black);

            Rectangle rect = new Rectangle(x, y, width, height);
            Image newImage = Image.FromFile("../../../Art/shop.png");
            g.DrawImage(newImage, rect);

        }

        public void DrawShopMenu(Graphics g)
        {
            Color ui = Color.FromArgb(210, 183, 119);

            SolidBrush br = new SolidBrush(ui);
            Pen penblack = new Pen(Color.Black, 4);
            Rectangle ShowMenu = new Rectangle(100, 60, PnlWidth - 200, PnlHeight - 120);
            Rectangle ShopSign = new Rectangle(125, 100, 400,100);

            Font shopFont = new Font("Times New Roman Bold", 52);
            SolidBrush shopBrush = new SolidBrush(Color.White);

            String drawString = "Error";

            Font drawFont = new Font("Arial Bold", 12);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush buyBrush = new SolidBrush(Color.FromArgb(94, 72, 20));

            Font coinFont = new Font("Arial", 20);
            Font buyFont = new Font("Arial", 26);

            g.FillRectangle(br, ShowMenu);
            g.DrawString("Shop", shopFont, shopBrush, ShopSign);

            for (int i = 0; i < 5; i++)
            {
                int x = 200;
                int y = 200;

                Image SeedImage = Image.FromFile("../../../Art/Items/tatersseed.png");
                Image CoinImage = Image.FromFile("../../../Art/ui/Coin.png");
                int Price = 25;

                Rectangle MenuSeeds = new Rectangle(x + (i * 100), y, 100, 100);
                Rectangle MenuTitle = new Rectangle(x + (i * 100), y + 100, 100, 30);
                Rectangle MenuPrice = new Rectangle(x + (i * 100), y + 130, 100, 30);
                Rectangle MenuCoin = new Rectangle(x + (i * 100), y + 130, 30, 30);
                Rectangle MenuCost = new Rectangle(x + 30 + (i * 100), y + 130, 100, 30);
                Rectangle MenuBuy = new Rectangle(x + (i * 100), y + 160, 100, 40);
                Rectangle MenuBuyInside = new Rectangle(x + 2 + (i * 100), y + 160, 96, 40);
                Rectangle MenuBuyText = new Rectangle(x + 12 + (i * 100), y + 160, 76, 40);

                drawString = "Tater Seeds";


                g.DrawRectangle(penblack, MenuSeeds);
                g.DrawRectangle(penblack, MenuTitle);
                g.DrawRectangle(penblack, MenuPrice);
                g.DrawRectangle(penblack, MenuBuy);
                g.FillRectangle(buyBrush, MenuBuyInside);

                if (i == 1)
                {
                     SeedImage = Image.FromFile("../../../Art/Items/wheatseed.png");
                    drawString = "Wheat Seed";
                    Price = 35;
                }
                else if (i == 2)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/cornseed.png");
                    drawString = "Corn Seed";
                    Price = 50;
                }
                else if (i == 3)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/carrotseed.png");
                    drawString = "Carrot Seed";
                    Price = 100;
                }
                else if (i == 4)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/turnipseed.png");
                    drawString = "Turnip Seed ";
                    Price = 150;
                }
                g.DrawImage(SeedImage, MenuSeeds);
                g.DrawImage(CoinImage, MenuCoin);
                g.DrawString(drawString, drawFont, drawBrush, MenuTitle);
                g.DrawString("Buy", buyFont, whiteBrush, MenuBuyText);
                g.DrawString(Convert.ToString(Price), coinFont, drawBrush, MenuCost);


            }
            for (int i = 0; i < 8; i++)
            {
                int x = 200;
                int y = 500;

                Image SeedImage = Image.FromFile("../../../Art/Items/fence.png");
                Image CoinImage = Image.FromFile("../../../Art/ui/Coin.png");
                int Price = 25;

                Rectangle MenuSeeds = new Rectangle(x + (i * 100), y, 100, 100);
                Rectangle MenuTitle = new Rectangle(x + (i * 100), y + 100, 100, 60);
                Rectangle MenuPrice = new Rectangle(x + (i * 100), y + 160, 100, 30);
                Rectangle MenuCoin = new Rectangle(x + (i * 100), y + 160, 30, 30);
                Rectangle MenuCost = new Rectangle(x + 30 + (i * 100), y + 160, 100, 30);
                Rectangle MenuBuy = new Rectangle(x + (i * 100), y + 190, 100, 40);
                Rectangle MenuBuyInside = new Rectangle(x + 2 + (i * 100), y + 190, 96, 40);
                Rectangle MenuBuyText = new Rectangle(x + 12 + (i * 100), y + 190, 76, 40);

                drawString = "Fence";


                g.DrawRectangle(penblack, MenuSeeds);
                g.DrawRectangle(penblack, MenuTitle);
                g.DrawRectangle(penblack, MenuPrice);
                g.DrawRectangle(penblack, MenuBuy);
                g.FillRectangle(buyBrush, MenuBuyInside);

                if (i == 1)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/Wall.png");
                    drawString = "Wall";
                    Price = 100;
                }
                else if (i == 2)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/Sword.png");
                    drawString = "Iron Sword";
                    Price = 300;
                }
                else if (i == 3)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/BeastSword.png");
                    drawString = "Beast Sword";
                    Price = 600;
                }
                else if (i == 4)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/IronArmour.png");
                    drawString = "Iron Armour";
                    Price = 350;
                }
                else if (i == 5)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/BeastArmour.png");
                    drawString = "Beast Armour";
                    Price = 700;
                }
                else if (i == 6)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/HealthPotion.png");
                    drawString = "Health Potion";
                    Price = 250;
                }
                else if (i == 7)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/UselessPotion.png");
                    drawString = "Useless Potion";
                    Price = 500;
                }
                g.DrawImage(SeedImage, MenuSeeds);
                g.DrawImage(CoinImage, MenuCoin);
                g.DrawString(drawString, drawFont, drawBrush, MenuTitle);
                g.DrawString("Buy", buyFont, whiteBrush, MenuBuyText);
                g.DrawString(Convert.ToString(Price), coinFont, drawBrush, MenuCost);


            }
            for (int i = 0; i < 5; i++)
            {
                int x = 800;
                int y = 200;

                Image SeedImage = Image.FromFile("../../../Art/Items/Tater.png");
                Image CoinImage = Image.FromFile("../../../Art/ui/Coin.png");
                int Price = 45;

                Rectangle MenuSeeds = new Rectangle(x + (i * 100), y, 100, 100);
                Rectangle MenuTitle = new Rectangle(x + (i * 100), y + 100, 100, 30);
                Rectangle MenuPrice = new Rectangle(x + (i * 100), y + 130, 100, 30);
                Rectangle MenuCoin = new Rectangle(x + (i * 100), y + 130, 30, 30);
                Rectangle MenuCost = new Rectangle(x + 30 + (i * 100), y + 130, 100, 30);
                Rectangle MenuBuy = new Rectangle(x + (i * 100), y + 160, 100, 40);
                Rectangle MenuBuyInside = new Rectangle(x + 2 + (i * 100), y + 160, 96, 40);
                Rectangle MenuBuyText = new Rectangle(x + 12 + (i * 100), y + 160, 76, 40);

                drawString = "Tater";


                g.DrawRectangle(penblack, MenuSeeds);
                g.DrawRectangle(penblack, MenuTitle);
                g.DrawRectangle(penblack, MenuPrice);
                g.DrawRectangle(penblack, MenuBuy);
                g.FillRectangle(buyBrush, MenuBuyInside);

                if (i == 1)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/Wheat.png");
                    drawString = "Wheat";
                    Price = 60;
                }
                else if (i == 2)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/Corn.png");
                    drawString = "Corn";
                    Price = 100;
                }
                else if (i == 3)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/Carrot.png");
                    drawString = "Carrot";
                    Price = 150;
                }
                else if (i == 4)
                {
                    SeedImage = Image.FromFile("../../../Art/Items/Turnip.png");
                    drawString = "Turnip";
                    Price = 250;
                }
                g.DrawImage(SeedImage, MenuSeeds);
                g.DrawImage(CoinImage, MenuCoin);
                g.DrawString(drawString, drawFont, drawBrush, MenuTitle);
                g.DrawString("Sell", buyFont, whiteBrush, MenuBuyText);
                g.DrawString(Convert.ToString(Price), coinFont, drawBrush, MenuCost);


            }

        }
    }
}
