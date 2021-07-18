using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;



namespace FarmWars
{
    class HostileAstar
    {
        public int CurrentX;
        public int CurrentY;
        public int xLoc;
        public int yLoc;
        public int PathPos = 1;
        public int PathLoc = 0;
        int pop = 0;

        public bool pathfollowed = false;
        public bool calcpath = true;

        public int LeftX, LeftY, RightX, RightY, UpX, UpY, DownX, DownY, StartX, StartY;
        bool pathmade = false;
        List<string> map = new List<string>();
        List<Tuple<int, int>> path = new List<Tuple<int, int>>();
        List<Tuple<int, int>> walkpath = new List<Tuple<int, int>>();

        Hostile hostile = new Hostile();

        public void AddToList(string mapline)
        {
            map.Add(mapline);
        }
        public void EmptyList()
        {
            map.Clear();
        }
        public void Main(Graphics g, int arnum)
        {
            try
            {
                path.Clear();
                Console.WriteLine("Path Cleared");
                walkpath.Clear();
                Console.WriteLine("Walk Path Cleared");
                calcpath = true;
                pathfollowed = false;

                PathPos = 1;
                PathLoc = 0;
                var start = new Tile();
                start.Y = map.FindIndex(x => x.Contains("A"));
                start.X = map[start.Y].IndexOf("A");


                var finish = new Tile();
                finish.Y = map.FindIndex(x => x.Contains("B"));
                finish.X = map[finish.Y].IndexOf("B");

                start.SetDistance(finish.X, finish.Y);

                var activeTiles = new List<Tile>();
                activeTiles.Add(start);
                var visitedTiles = new List<Tile>();

                while (activeTiles.Any())
                {
                    var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                    if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                    {
                        //We found the destination and we can be sure (Because the the OrderBy above)
                        //That it's the most low cost option. 
                        var tile = checkTile;
                        //Console.WriteLine("Retracing steps backwards...");
                        while (true)
                        {

                            path.Add(new Tuple<int, int>(tile.X, tile.Y));
                            //Console.WriteLine(path.Last());

                            using (Font font = new Font("Times New Roman", 36, FontStyle.Bold, GraphicsUnit.Pixel))
                            {
                                Point point1 = new Point(tile.X * 25, tile.Y * 25);
                                TextRenderer.DrawText(g, "*", font, point1, Color.Red);
                            }

                            if (map[tile.Y][tile.X] == ' ')
                            {
                                var newMapRow = map[tile.Y].ToCharArray();
                                newMapRow[tile.X] = '*';

                                map[tile.Y] = new string(newMapRow);
                                //Console.WriteLine(newMapRow);

                            }
                            tile = tile.Parent;
                            if (tile == null)
                            {
                                //Console.WriteLine("Map looks like :");
                                //map.ForEach(x => Console.WriteLine(x));
                                //Console.WriteLine("Done!");
                                path.Reverse();
                                ((FormGame)FormGame.ActiveForm).TmrHosMovement.Enabled = true;
                                pathmade = true;
                                return;
                            }
                        }
                    }

                    visitedTiles.Add(checkTile);
                    activeTiles.Remove(checkTile);

                    var walkableTiles = GetWalkableTiles(map, checkTile, finish);

                    foreach (var walkableTile in walkableTiles)
                    {
                        //We have already visited this tile so we don't need to do so again!
                        if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                            continue;

                        //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
                        if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        {
                            var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                            if (existingTile.CostDistance > checkTile.CostDistance)
                            {
                                activeTiles.Remove(existingTile);
                                activeTiles.Add(walkableTile);
                            }
                        }
                        else
                        {
                            //We've never seen this tile before so add it to the list. 
                            activeTiles.Add(walkableTile);
                        }
                    }
                }

                Console.WriteLine("No Path Found!");
            }
            catch
            {

            }
        }

        private List<Tile> GetWalkableTiles(List<string> map, Tile currentTile, Tile targetTile)
        {
            var possibleTiles = new List<Tile>()
                {
                new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                };

            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            var maxX = map.First().Length - 1;
            var maxY = map.Count - 1;

            return possibleTiles
                    .Where(tile => tile.X >= 0 && tile.X <= maxX)
                    .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                    .Where(tile => map[tile.Y][tile.X] == ' ' || map[tile.Y][tile.X] == 'B')
                    .ToList();
        }

        public void PathFollow(Graphics g, int arnum)
        {
            if (pathfollowed == false)
            {
                if (calcpath == true)
                {
                    try
                    {
                        int x4 = 0;
                        int y4 = 0;
                        int x1 = path[PathPos - 1].Item1 * 25;
                        int y1 = path[PathPos - 1].Item2 * 25;
                        int x2 = path[PathPos].Item1 * 25;
                        int y2 = path[PathPos].Item2 * 25;
                        int x3 = (x1 - x2) / 25;
                        int y3 = (y1 - y2) / 25;
                        for (int i = 0; i < 25; i++)
                        {
                            x4 = x1 - x3;
                            y4 = y1 - y3;
                            walkpath.Add(new Tuple<int, int>(x4, y4));
                            Console.WriteLine(x4 + ":" + y4);
                            Console.WriteLine(i);
                            x1 = x4;
                            y1 = y4;
                        }
                    }
                    catch
                    {
                        calcpath = false;
                    }
                }
                if (PathLoc < walkpath.Count)
                {
                    xLoc = walkpath[PathLoc].Item1;
                    yLoc = walkpath[PathLoc].Item2;
                    x = xLoc;
                    y = yLoc;
                    DrawHostile(g);

                }
                else
                {
                    try
                    {
                        pathfollowed = true;
                        ((FormGame)FormGame.ActiveForm).TmrHosMovement.Enabled = false;
                        ((FormGame)FormGame.ActiveForm).Drawn[arnum] = false;
                        Console.WriteLine("Path Walked");
                    } catch
                    {

                    }
                }
            }
        }
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


    class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Cost { get; set; }
        public int Distance { get; set; }
        public int CostDistance => Cost + Distance;
        public Tile Parent { get; set; }

        //The distance is essentially the estimated distance, ignoring walls to our target. 
        //So how many tiles left and right, up and down, ignoring walls, to get there. 
        public void SetDistance(int targetX, int targetY)
        {
            this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
        }
    }

}