namespace Lith.FlatFile.Core.Models
{
    public struct Point
    {
        public Point(int start, int length)
        {
            Start = start;
            Length = length;
        }

        public int Start { get; set; }
        public int Length { get; set; }
    }
}
