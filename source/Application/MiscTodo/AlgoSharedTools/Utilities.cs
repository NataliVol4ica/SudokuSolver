using System;
using System.Drawing;

namespace Application.MiscTodo.AlgoSharedTools
{
    public static class Utilities
    {
        public static bool IsHorizontalPair(Point p1, Point p2)
        {
            return p1.X == p2.X;
        }

        public static bool IsVerticalPair(Point p1, Point p2)
        {
            return p1.Y == p2.Y;
        }

        public static bool IsSameBlockPair(Point p1, Point p2)
        {
            return p1.X / 3 == p2.X / 3 && p1.Y / 3 == p2.Y / 3;
        }

        public static bool IsHorizontalTriple(Point p1, Point p2, Point p3)
        {
            return p1.X == p2.X && p2.X == p3.X;
        }

        public static bool IsVerticalTriple(Point p1, Point p2, Point p3)
        {
            return p1.Y == p2.Y && p2.Y == p3.Y;
        }

        public static bool IsSameBlockTriple(Point p1, Point p2, Point p3)
        {
            throw new NotImplementedException();
        }
    }
}