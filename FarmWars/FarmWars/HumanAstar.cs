using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;



namespace FarmWars
{
    class HumanAstar
    {
        public int CurrentX;
        public int CurrentY;

        public int PathPos = 1;
        public int PathLoc = 0;
        int pop = 0;


        public int xLoc;
        public int yLoc;

        public bool pathfollowed = false;
        public bool calcpath = true;

        public int LeftX, LeftY, RightX, RightY, UpX, UpY, DownX, DownY, StartX, StartY;
        bool pathmade = false;
        List<string> map = new List<string>();
        List<Tuple<int, int>> path = new List<Tuple<int, int>>();
        List<Tuple<int, int>> walkpath = new List<Tuple<int, int>>();

        Human human = new Human();

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
            try
            {
                path.Clear();
            
                walkpath.Clear();
                calcpath = true;
                pathfollowed = false;

                PathPos = 1;
                PathLoc = 0;
                var start = new HumanTile();
                start.Y = map.FindIndex(x => x.Contains("A"));
                start.X = map[start.Y].IndexOf("A");


                var finish = new HumanTile();
                finish.Y = map.FindIndex(x => x.Contains("B"));
                finish.X = map[finish.Y].IndexOf("B");

                start.SetDistance(finish.X, finish.Y);

                var activeTiles = new List<HumanTile>();
                activeTiles.Add(start);
                var visitedTiles = new List<HumanTile>();

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
                                TextRenderer.DrawText(g, "*", font, point1, Color.Aqua);
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

                                ((FormGame)FormGame.ActiveForm).TmrHumMovement.Enabled = true;
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

        private List<HumanTile> GetWalkableTiles(List<string> map, HumanTile currentTile, HumanTile targetTile)
        {
            var possibleTiles = new List<HumanTile>()
                {
                new HumanTile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new HumanTile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new HumanTile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new HumanTile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
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

        public void PathFollow(Graphics g)
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
                    human.x = xLoc;
                    human.y = yLoc;
                }
                else
                {
                    pathfollowed = true;
                    //((FormGame)FormGame.ActiveForm).TmrHumMovement.Enabled = false;
                   // ((FormGame)FormGame.ActiveForm).HuDrawn = false;
                    Console.WriteLine("Path Walked cause Human smart");
                    walkpath.Clear();
                    path.Clear();
                }
            }
            human.DrawHuman(g);
        }

        public void ClearWalkPath()
        {
            walkpath.Clear();
            path.Clear();
            pathfollowed = true;
        }
    }


    class HumanTile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Cost { get; set; }
        public int Distance { get; set; }
        public int CostDistance => Cost + Distance;
        public HumanTile Parent { get; set; }

        //The distance is essentially the estimated distance, ignoring walls to our target. 
        //So how many tiles left and right, up and down, ignoring walls, to get there. 
        public void SetDistance(int targetX, int targetY)
        {
            this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
        }
    }

}