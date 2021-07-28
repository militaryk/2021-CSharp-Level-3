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
    class Feild
    {
        public int SquareSize;

        public List<Tuple<int, string, int, int>> cropfield = new List<Tuple<int, string, int, int>>();


        public void DrawFeild(Graphics g, int Turn, string CropType, int XCord, int YCord)
        {
            //Calcualte the X and Y of each indervidual square
            int PosX = SquareSize * XCord;
            int PosY = SquareSize * YCord;

            cropfield.Add(new Tuple<int, string, int, int>(Turn, CropType, PosX, PosY));

            // Create rectangle for feild.
            Rectangle rect = new Rectangle(PosX, PosY, 25, 25);
            PlantSeed(g, PosX, PosY, CropType);
        }

        public void PlantSeed(Graphics g, int PosX, int PosY, string CropType)
        {
            CropType = "Wheat";
            Image SeedImage = Image.FromFile("../../../Art/ground/grownwheat.png");

            if (CropType == "Tater")
            {
                SeedImage = Image.FromFile("../../../Art/crops/taters1.png");
            }
            if (CropType == "Wheat")
            {
                SeedImage = Image.FromFile("../../../Art/crops/wheats1.png");
            }
            if (CropType == "Corn")
            {
                SeedImage = Image.FromFile("../../../Art/crops/corns1.png");
            }
            if (CropType == "Carrot")
            {
                SeedImage = Image.FromFile("../../../Art/crops/carrots1.png");
            }
            if (CropType == "Turnip")
            {
                SeedImage = Image.FromFile("../../../Art/crops/turnips1.png");
            }

            Rectangle rect = new Rectangle(PosX, PosY, 25, 25);
            g.DrawImage(SeedImage, rect);
        }
        public void PlantTurn(Graphics g, int Turn)
        {
            Image YoungCropImage = Image.FromFile("../../../Art/ground/grownwheat.png");
            for (int i = 0; i < cropfield.Count; i++)
            {
                int CropTurn = cropfield[i].Item1;
                string CropType = cropfield[i].Item2;
                int XCord = cropfield[i].Item3;
                int YCord = cropfield[i].Item4;
                CropType = "Carrot";

                if (CropTurn == Turn - 1)
                {

                    if (CropType == "Tater")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/taters2.png");
                    }
                    if (CropType == "Wheat")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/wheats2.png");
                    }
                    if (CropType == "Corn")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/corns2.png");
                    }
                    if (CropType == "Carrot")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/carrots2.png");
                    }
                    if (CropType == "Turnip")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/turnips2.png");
                    }
                    Rectangle rect = new Rectangle(XCord, YCord, 25, 25);
                    g.DrawImage(YoungCropImage, rect);
                }
                if (CropTurn == Turn - 2)
                {

                    if (CropType == "Tater")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/taters3.png");
                    }
                    if (CropType == "Wheat")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/wheats3.png");
                    }
                    if (CropType == "Corn")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/corns3.png");
                    }
                    if (CropType == "Carrot")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/carrots3.png");
                    }
                    if (CropType == "Turnip")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/turnips3.png");
                    }
                    Rectangle rect = new Rectangle(XCord, YCord, 25, 25);
                    g.DrawImage(YoungCropImage, rect);
                }
                if (CropTurn == Turn - 3)
                {

                    if (CropType == "Tater")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/taters4.png");
                    }
                    if (CropType == "Wheat")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/wheats4.png");
                    }
                    if (CropType == "Corn")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/corns4.png");
                    }
                    if (CropType == "Carrot")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/carrots4.png");
                    }
                    if (CropType == "Turnip")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/turnips4.png");
                    }
                    Rectangle rect = new Rectangle(XCord, YCord, 25, 25);
                    g.DrawImage(YoungCropImage, rect);
                }
                if (CropTurn == Turn - 4)
                {

                    if (CropType == "Tater")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/taters5.png");
                    }
                    if (CropType == "Wheat")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/wheats5.png");
                    }
                    if (CropType == "Corn")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/corns5.png");
                    }
                    if (CropType == "Carrot")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/carrots5.png");
                    }
                    if (CropType == "Turnip")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/turnips5.png");
                    }
                    Rectangle rect = new Rectangle(XCord, YCord, 25, 25);
                    g.DrawImage(YoungCropImage, rect);
                }
            }
        }
    }
}
