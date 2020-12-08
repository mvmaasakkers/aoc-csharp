namespace Event2015.Day03
{
    public class Location
    {
        public override string ToString()
        {
            return $"y-{Y}-x-{X}";
        }

        public Location()
        {
        }

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}