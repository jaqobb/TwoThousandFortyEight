namespace TwoThousandFortyEight.Logic
{
    public class Direction
    {
        public readonly char[] Keys;
        public readonly int    Width;
        public readonly int    Height;

        public Direction(char[] keys, int width, int height)
        {
            Keys   = keys;
            Width  = width;
            Height = height;
        }
    }
}
