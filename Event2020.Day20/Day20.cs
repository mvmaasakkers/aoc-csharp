using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2020.Day20
{
    public class Day20
    {
        private List<string> _input;

        public Day20(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
        }

        public long ComputePart1()
        {
            const int cardSize = 10;
            List<(char sym, long id)[][,]> panels = new List<(char, long)[][,]>();
            var image = new Dictionary<(int x, int y), (char sym, long id)>();
            var usedCards = new Dictionary<long, bool>();

            var line = 0;
            while (line < _input.Count)
            {
                if (_input[line].StartsWith("Tile"))
                {
                    var panelNum = long.Parse(_input[line].Substring(5, 4));
                    var panel = new (char sym, long id)[8][,];

                    line++;
                    for (var k = 0; k < 8; k++)
                    {
                        panel[k] = new (char, long)[cardSize, cardSize];
                        for (var i = 0; i < cardSize; i++)
                        {
                            for (var j = 0; j < cardSize; j++)
                            {
                                panel[k][j, i] = (_input[line + i][j], panelNum);
                            }
                        }
                    }

                    for (var i = 1; i < 8; i++)
                    {
                        if ((i & 1) == 1)
                        {
                            panel[i] = RotateMatrix(panel[i]);
                        }

                        if ((i & 2) == 2)
                        {
                            panel[i] = FlipVertically(panel[i]);
                        }

                        if ((i & 4) == 4)
                        {
                            panel[i] = FlipHorizontally(panel[i]);
                        }
                    }

                    panels.Add(panel);
                }

                line++;
            }

            PlaceCard(usedCards, image, cardSize, 0, 0, panels[0][0], true);

            int minx;
            int miny;
            int maxx;
            int maxy;
            var last = -1;
            while (usedCards.Count != last)
            {
                last = usedCards.Count;
                minx = image.Min(item => item.Key.x / cardSize) - 1;
                miny = image.Min(item => item.Key.y / cardSize) - 1;
                maxx = image.Max(item => item.Key.x / cardSize) + 1;
                maxy = image.Max(item => item.Key.y / cardSize) + 1;
                for (var i = miny; i <= maxy; i++)
                {
                    for (var j = minx; j <= maxx; j++)
                    {
                        foreach (var item in panels)
                        {
                            for (var k = 0; k < 8; k++)
                            {
                                if (PlaceCard(usedCards, image, cardSize, j, i, item[k]))
                                {
                                    goto panelPicked;
                                }
                            }
                        }

                        panelPicked: ;
                    }
                }
            }

            minx = image.Min(item => item.Key.x);
            miny = image.Min(item => item.Key.y);
            maxx = image.Max(item => item.Key.x);
            maxy = image.Max(item => item.Key.y);


            return image[(minx, miny)].id * image[(maxx, miny)].id * image[(minx, maxy)].id * image[(maxx, maxy)].id;
        }

        bool PlaceCard(Dictionary<long, bool> usedCards, Dictionary<(int x, int y), (char sym, long id)> image,
            int cardSize, int x, int y, (char sym, long id)[,] card, bool skipPlaceCheck = false)
        {
            if (usedCards.ContainsKey(card[0, 0].id))
            {
                return false;
            }

            if (image.ContainsKey((x * cardSize, y * cardSize)))
            {
                return false;
            }

            if (!skipPlaceCheck)
            {
                for (var i = 0; i < cardSize; i++)
                {
                    (char sym, long id) thing;
                    if (image.TryGetValue((x * cardSize + i, y * cardSize - 1), out thing))
                    {
                        if (thing.sym != card[i, 0].sym) return false;
                    }
                    else if (image.TryGetValue((x * cardSize + i, (y + 1) * cardSize), out thing))
                    {
                        if (thing.sym != card[i, cardSize - 1].sym) return false;
                    }
                    else if (image.TryGetValue((x * cardSize - 1, y * cardSize + i), out thing))
                    {
                        if (thing.sym != card[0, i].sym) return false;
                    }
                    else if (image.TryGetValue(((x + 1) * cardSize, y * cardSize + i), out thing))
                    {
                        if (thing.sym != card[cardSize - 1, i].sym) return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            usedCards[card[0, 0].id] = true;
            for (var i = 0; i < cardSize; i++)
            {
                for (var j = 0; j < cardSize; j++)
                {
                    image[(x * cardSize + i, y * cardSize + j)] = card[i, j];
                }
            }

            return true;
        }

        static int Mod(int a, int b)
        {
            return (int) (a - Math.Floor(a * 1.0 / b) * b);
        }

        public static T[,] FlipVertically<T>(T[,] matrix)
        {
            var n = matrix.GetLength(0);
            if (matrix.GetLength(0) != matrix.GetLength(1)) throw new ArgumentException();
            var ret = new T[n, n];

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    ret[j, i] = matrix[n - j - 1, i];
                }
            }

            return ret;
        }

        public static T[,] FlipHorizontally<T>(T[,] matrix)
        {
            var n = matrix.GetLength(0);
            if (matrix.GetLength(0) != matrix.GetLength(1)) throw new ArgumentException();
            var ret = new T[n, n];

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    ret[i, j] = matrix[i, n - j - 1];
                }
            }

            return ret;
        }

        public long ComputePart2()
        {
            const int cardSize = 10;
            List<(char sym, long id)[][,]> panels = new List<(char, long)[][,]>();
            var image = new Dictionary<(int x, int y), (char sym, long id)>();
            var usedCards = new Dictionary<long, bool>();

            var line = 0;
            while (line < _input.Count)
            {
                if (_input[line].StartsWith("Tile"))
                {
                    var panelNum = long.Parse(_input[line].Substring(5, 4));
                    var panel = new (char sym, long id)[8][,];

                    line++;
                    for (var k = 0; k < 8; k++)
                    {
                        panel[k] = new (char, long)[cardSize, cardSize];
                        for (var i = 0; i < cardSize; i++)
                        {
                            for (var j = 0; j < cardSize; j++)
                            {
                                panel[k][j, i] = (_input[line + i][j], panelNum);
                            }
                        }
                    }

                    for (var i = 1; i < 8; i++)
                    {
                        if ((i & 1) == 1)
                            panel[i] = RotateMatrix(panel[i]);
                        if ((i & 2) == 2)
                            panel[i] = FlipVertically(panel[i]);
                        if ((i & 4) == 4)
                            panel[i] = FlipHorizontally(panel[i]);
                    }

                    panels.Add(panel);
                }

                line++;
            }

            PlaceCard(usedCards, image, cardSize, 0, 0, panels[0][0], true);

            int minx;
            int miny;
            int maxx;
            int maxy;
            var last = -1;
            while (usedCards.Count != last)
            {
                last = usedCards.Count;
                minx = image.Min(item => item.Key.x / cardSize) - 1;
                miny = image.Min(item => item.Key.y / cardSize) - 1;
                maxx = image.Max(item => item.Key.x / cardSize) + 1;
                maxy = image.Max(item => item.Key.y / cardSize) + 1;
                for (var i = miny; i <= maxy; i++)
                {
                    for (var j = minx; j <= maxx; j++)
                    {
                        foreach (var item in panels)
                        {
                            for (var k = 0; k < 8; k++)
                            {
                                if (PlaceCard(usedCards, image, cardSize, j, i, item[k]))
                                {
                                    goto panelPicked;
                                }
                            }
                        }

                        panelPicked: ;
                    }
                }
            }

            minx = image.Min(item => item.Key.x);
            miny = image.Min(item => item.Key.y);
            maxx = image.Max(item => item.Key.x);
            maxy = image.Max(item => item.Key.y);

            var nauticalMap = new List<List<char>>();
            var seaRough = 0;

            for (var i = miny; i <= maxy; i++)
            {
                if (Mod(i, 10) == 0 || Mod(i, 10) == 9)
                {
                    continue;
                }

                var row = new List<char>();
                for (var j = minx; j <= maxx; j++)
                {
                    if (Mod(j, 10) == 0 || Mod(j, 10) == 9)
                    {
                        continue;
                    }

                    row.Add(image[(j, i)].sym);
                    if (image[(j, i)].sym == '#')
                    {
                        seaRough++;
                    }
                }

                nauticalMap.Add(row);
            }

            var map = new char[8][,];
            for (var k = 0; k < 8; k++)
            {
                map[k] = new char[nauticalMap.Count, nauticalMap.Count];
                for (var i = 0; i < nauticalMap.Count; i++)
                {
                    for (var j = 0; j < nauticalMap.Count; j++)
                    {
                        map[k][j, i] = nauticalMap[i][j];
                    }
                }
            }

            for (var i = 1; i < 8; i++)
            {
                map[i] = map[0];
                if ((i & 1) == 1)
                    map[i] = RotateMatrix(map[i]);
                if ((i & 2) == 2)
                    map[i] = FlipVertically(map[i]);
                if ((i & 4) == 4)
                    map[i] = FlipHorizontally(map[i]);
            }

            var monster = new[]
            {
                "                  # ",
                "#    ##    ##    ###",
                " #  #  #  #  #  #   "
            };
            var roughness = new int[8];
            for (var i = 0; i < 8; i++)
            {
                var hits = 0;
                for (var y = 0; y < nauticalMap.Count - monster.Length; y++)
                {
                    for (var x = 0; x < nauticalMap.Count - monster[0].Length; x++)
                    {
                        var subHits = 0;
                        for (var j = 0; j < monster.Length; j++)
                        {
                            for (var k = 0; k < monster[0].Length; k++)
                            {
                                var spotHasRough = map[i][x + k, y + j] == '#';
                                var spotRequiresRough = monster[j][k] == '#';
                                if (spotHasRough && spotRequiresRough)
                                {
                                    subHits++;
                                }

                                if (spotRequiresRough && !spotHasRough)
                                {
                                    goto skip;
                                }
                            }
                        }

                        hits += subHits;
                        skip: ;
                    }
                }

                roughness[i] = seaRough - hits;
            }

            return roughness.Min();
        }

        private static T[,] RotateMatrix<T>(T[,] matrix)
        {
            var n = matrix.GetLength(0);
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new ArgumentException();
            }

            var ret = new T[n, n];

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    ret[j, i] = matrix[i, n - j - 1];
                }
            }

            return ret;
        }
    }
}