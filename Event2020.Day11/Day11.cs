using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Event2020.Day11
{
    public class Tile
    {
        public string Type { get; set; }

        public Coordinate Position { get; private set; }

        public Tile(string type, Coordinate c)
        {
            Type = type;
            Position = c;
        }
    }

    public class Day11
    {
        private Dictionary<string, Tile> _grid = new Dictionary<string, Tile>();
        private readonly Dictionary<string, Tile> _baseGrid = new Dictionary<string, Tile>();

        public Day11(string input)
        {
            var lines = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();

            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    _baseGrid[new Coordinate(x, y).ToString()] =
                        new Tile(lines[y][x].ToString(), new Coordinate(x, y));
                }
            }
        }

        private Dictionary<string, Tile> CloneGrid()
        {
            var newGrid = new Dictionary<string, Tile>();
            foreach (var g in _grid)
            {
                newGrid[new Coordinate(g.Value.Position.X, g.Value.Position.Y).ToString()] =
                    new Tile(g.Value.Type, new Coordinate(g.Value.Position.X, g.Value.Position.Y));
            }

            return newGrid;
        }

        private Dictionary<string, Tile> CloneFromBaseGrid()
        {
            var newGrid = new Dictionary<string, Tile>();
            foreach (var g in _baseGrid)
            {
                newGrid[new Coordinate(g.Value.Position.X, g.Value.Position.Y).ToString()] =
                    new Tile(g.Value.Type, new Coordinate(g.Value.Position.X, g.Value.Position.Y));
            }

            return newGrid;
        }

        public long ComputePart1()
        {
            _grid = CloneFromBaseGrid();

            while (true)
            {
                if (IterateOverGrid(1, 4) == 0)
                {
                    return _grid.Count(t => t.Value.Type == "#");
                }
            }
        }

        public int IterateOverGrid(int maxDistance, int minOccupied)
        {
            var changes = 0;
            var grid = CloneGrid();

            foreach (var p in _grid)
            {
                if (p.Value.Type == ".")
                {
                    // Floor tiles never change
                    continue;
                }

                var dir = GetDirectionalEncounters(grid, p.Value.Position, maxDistance);
                if (p.Value.Type == "L" && dir.Count(t => _grid[t.ToString()].Type == "#") == 0)
                {
                    grid[p.Value.Position.ToString()].Type = "#";
                    changes++;
                }

                if (p.Value.Type == "#" && dir.Count(t => _grid[t.ToString()].Type == "#") >= minOccupied)
                {
                    grid[p.Value.Position.ToString()].Type = "L";
                    changes++;
                }
            }

            _grid = grid;
            return changes;
        }


        public long ComputePart2()
        {
            _grid = CloneFromBaseGrid();
            while (true)
            {
                if (IterateOverGrid(-1, 5) == 0)
                {
                    return _grid.Count(t => t.Value.Type == "#");
                }
            }
        }

        private List<Coordinate> GetDirectionalEncounters(Dictionary<string, Tile> grid, Coordinate pos, int maxDistance)
        {
            var l = new List<Coordinate>();

            (int y, int x)[] traversalOptions =
            {
                (-1, -1),
                (-1, 0),
                (-1, 1),
                (0, -1),
                (0, 1),
                (1, -1),
                (1, 0),
                (1, 1),
            };

            foreach (var option in traversalOptions)
            {
                var curY = option.y;
                var curX = option.x;
                var found = false;
                var i = 0;
                while (!found)
                {
                    var newPos = new Coordinate(pos.X + curX, pos.Y + curY);
                    if (grid.ContainsKey(newPos.ToString()))
                    {
                        if (grid[newPos.ToString()].Type != ".")
                        {
                            l.Add(newPos);
                            found = true;
                        }
                    }
                    else
                    {
                        found = true;
                    }

                    curY += option.y;
                    curX += option.x;
                    i++;
                    if (maxDistance > 0 && maxDistance <= i)
                    {
                        found = true;
                    }
                }
            }


            return l;
        }
    }
}