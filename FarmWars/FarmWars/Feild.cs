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
        public string CropType;
        int health = 100;


        public List<Tuple<int, string, int, int, int>> cropfield = new List<Tuple<int, string, int, int, int>>();

        Inventory inventory = new Inventory();
        Square square = new Square();

        public void DrawFeild(Graphics g, int Turn, int XCord, int YCord)
        {
            if (CropType == "TaterSeed" || CropType == "WheatSeed" || CropType == "CornSeed" || CropType == "CarrotSeed" || CropType == "TurnipSeed")
            {

                //Calcualte the X and Y of each indervidual square
                int PosX = SquareSize * XCord;
                int PosY = SquareSize * YCord;

                if (cropfield.Where(z => z.Item3 == PosX && z.Item4 == PosY).Any())
                {

                }
                else
                {
                  cropfield.Add(new Tuple<int, string, int, int, int>(Turn, CropType, PosX, PosY, health));
                    // Create rectangle for feild.
                    PlantSeed(g, PosX, PosY, CropType);
                }
            }
        }

        public void PlantSeed(Graphics g, int PosX, int PosY, string CropType)
        {
            Image SeedImage = Image.FromFile("../../../Art/ground/grownwheat.png");

            if (CropType == "TaterSeed")
            {
                SeedImage = Image.FromFile("../../../Art/crops/taters1.png");
            }
            if (CropType == "WheatSeed")
            {
                SeedImage = Image.FromFile("../../../Art/crops/wheats1.png");
            }
            if (CropType == "CornSeed")
            {
                SeedImage = Image.FromFile("../../../Art/crops/corns1.png");
            }
            if (CropType == "CarrotSeed")
            {
                SeedImage = Image.FromFile("../../../Art/crops/carrots1.png");
            }
            if (CropType == "TurnipSeed")
            {
                SeedImage = Image.FromFile("../../../Art/crops/turnips1.png");
            }

            Rectangle rect = new Rectangle(PosX, PosY, 25, 25);
            g.DrawImage(SeedImage, rect);
        }

        public string HarvestField(int XCord, int YCord, int Turn)
        {
            string CropName = "";
            for (int i = 0; i < cropfield.Count; i++)
            {
                if (cropfield[i].Item3 / 25 == XCord / 25 && cropfield[i].Item4 / 25 == YCord / 25)
                {
                    int CropTurn = cropfield[i].Item1;
                    if (CropTurn <= Turn - 4) {
                        CropName = cropfield[i].Item2;
                        cropfield.RemoveAt(i);
                        Console.WriteLine(CropName);
                    }
                }
            }
            return CropName;
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

                if (CropTurn == Turn - 1)
                {

                    if (CropType == "TaterSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/taters2.png");
                    }
                    if (CropType == "WheatSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/wheats2.png");
                    }
                    if (CropType == "CornSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/corns2.png");
                    }
                    if (CropType == "CarrotSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/carrots2.png");
                    }
                    if (CropType == "TurnipSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/turnips2.png");
                    }
                    Rectangle rect = new Rectangle(XCord, YCord, 25, 25);
                    g.DrawImage(YoungCropImage, rect);
                }
                if (CropTurn == Turn - 2)
                {

                    if (CropType == "TaterSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/taters3.png");
                    }
                    if (CropType == "WheatSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/wheats3.png");
                    }
                    if (CropType == "CornSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/corns3.png");
                    }
                    if (CropType == "CarrotSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/carrots3.png");
                    }
                    if (CropType == "TurnipSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/turnips3.png");
                    }
                    Rectangle rect = new Rectangle(XCord, YCord, 25, 25);
                    g.DrawImage(YoungCropImage, rect);
                }
                if (CropTurn == Turn - 3)
                {

                    if (CropType == "TaterSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/taters4.png");
                    }
                    if (CropType == "WheatSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/wheats4.png");
                    }
                    if (CropType == "CornSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/corns4.png");
                    }
                    if (CropType == "CarrotSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/carrots4.png");
                    }
                    if (CropType == "TurnipSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/turnips4.png");
                    }
                    Rectangle rect = new Rectangle(XCord, YCord, 25, 25);
                    g.DrawImage(YoungCropImage, rect);
                }
                if (CropTurn == Turn - 4)
                {

                    if (CropType == "TaterSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/taters5.png");
                    }
                    if (CropType == "WheatSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/wheats5.png");
                    }
                    if (CropType == "CornSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/corns5.png");
                    }
                    if (CropType == "CarrotSeed")
                    {
                        YoungCropImage = Image.FromFile("../../../Art/crops/carrots5.png");
                    }
                    if (CropType == "TurnipSeed")
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
