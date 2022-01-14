using System.Drawing;

namespace Application.Tools
{
    public static class PointExtensions
    {
        public static string ToSudokuCoords(this Point source)
        {
            char rowLetter = (char)(source.X + 'A');
            return $"[{rowLetter}{source.Y}]";
        }
    }
}
