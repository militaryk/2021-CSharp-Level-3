using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;



namespace FarmWars
{
    class Astar
    {
        public int CurrentX;
        public int CurrentY;
        public int LeftX, LeftY, RightX, RightY, UpX, UpY, DownX, DownY, StartX, StartY;
        List<string> map = new List<string>();
        List<Tuple<int, int>> path = new List<Tuple<int, int>>();
        Hostile hostile = new Hostile();

        public void AddToList(string mapline)
        {
            map.Add(mapline);
        }
        public void EmptyList()
        {
            map.Clear();
        }
        public void Main(Graphics g)
            {
            path.Clear();
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
                        Console.WriteLine("Retracing steps backwards...");
                        while (true)
                        {

                        path.Add(new Tuple<int, int>(tile.X, tile.Y));
                        Console.WriteLine(path.Last());

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
                                Console.WriteLine(newMapRow);

                        }
                        tile = tile.Parent;
                            if (tile == null)
                            {
                                Console.WriteLine("Map looks like :");
                                map.ForEach(x => Console.WriteLine(x));
                                Console.WriteLine("Done!");
                                FollowPath(g);

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

            public void FollowPath(Graphics g)
            {
            int p = path.RemoveAll(z => z.Item1 == CurrentX && z.Item2 == CurrentY);
            for (int i = 0; i <= 100; i++)
            {
                LeftX = CurrentX - 1;
                LeftY = CurrentY;
                UpX = CurrentX;
                UpY = CurrentY - 1;
                DownX = CurrentX;
                DownY = CurrentY + 1;
                RightX = CurrentX + 1;
                RightY = CurrentY;
                //Console.WriteLine("Left" + Convert.ToString(LeftX) + ":" + Convert.ToString(LeftY));
                //Console.WriteLine("Up" + Convert.ToString(UpX) + ":" + Convert.ToString(UpY));
                //Console.WriteLine("Down" + Convert.ToString(DownX) + ":" + Convert.ToString(DownY));
                //Console.WriteLine("Right" + Convert.ToString(RightX) + ":" + Convert.ToString(RightY));
                bool left = path.Any(z => z.Item1 == LeftX && z.Item2 == LeftY);
                bool right = path.Any(z => z.Item1 == RightX && z.Item2 == RightY);
                bool up = path.Any(z => z.Item1 == UpX && z.Item2 == UpY);
                bool down = path.Any(z => z.Item1 == DownX && z.Item2 == DownY);

                if (down == true)
                {
                    Console.WriteLine("Down");
                    CurrentX = DownX;
                    CurrentY = DownY;
                    int v = path.RemoveAll(z => z.Item1 == DownX && z.Item2 == DownY);
                    Move(g);

                }
                else if (up == true)
                {
                    Console.WriteLine("Up");
                    CurrentX = UpX;
                    CurrentY = UpY;
                    int v = path.RemoveAll(z => z.Item1 == UpX && z.Item2 == UpY);
                    Move(g);

                }
                else if (left == true)
                {
                    Console.WriteLine("Left");
                    CurrentX = LeftX;
                    CurrentY = LeftY;
                    int v = path.RemoveAll(z => z.Item1 == LeftX && z.Item2 == LeftY);
                    Move(g);

                }
                else if (right == true)
                {
                    Console.WriteLine("Right");
                    CurrentX = RightX;
                    CurrentY = RightY;
                    int v = path.RemoveAll(z => z.Item1 == RightX && z.Item2 == RightY);
                    Move(g);

                }
            }
        }
        public void Move(Graphics g)
            {
            hostile.x = CurrentX * 25;
            hostile.y = CurrentY* 25;
            hostile.DrawHostile(g);
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