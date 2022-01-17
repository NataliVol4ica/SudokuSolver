using System.Collections.Generic;
using System.Drawing;
using Application.Models;
using Application.Tools;

namespace Application.MiscTodo.AlgoNakedPairsRules
{
    public abstract class BaseNakedPairRule
    {
        public abstract int ApplyToAll(Context context);

        protected int ProcessPair(List<int> candidates, Point firstAbsolutePoint, Point secondAbsolutePoint, Context context)
        {
            var numOfChanges = 0;
            if (IsHorizontalPair(firstAbsolutePoint, secondAbsolutePoint))
            {
                numOfChanges += ProcessHorizontalPair(candidates, firstAbsolutePoint, secondAbsolutePoint, context);
            }
            if (IsVerticalPair(firstAbsolutePoint, secondAbsolutePoint))
            {
                numOfChanges += ProcessVerticalPair(candidates, firstAbsolutePoint, secondAbsolutePoint, context);
            }
            if (IsSameBlockPair(firstAbsolutePoint, secondAbsolutePoint))
            {
                numOfChanges += ProcessBlockPair(candidates, firstAbsolutePoint, secondAbsolutePoint, context);
            }
            return numOfChanges;
        }

        private int ProcessHorizontalPair(List<int> candidates, Point firstAbsolutePoint, Point secondAbsolutePoint, Context context)
        {
            var numOfChanges = 0;

            var row = context.SudokuUnderSolution.Row(firstAbsolutePoint.X);
            for (int colId = 0; colId < 9; colId++) //todo 9
            {
                var currentAbsolutePos = new Point(firstAbsolutePoint.X, colId);
                if (currentAbsolutePos.Y == firstAbsolutePoint.Y || currentAbsolutePos.Y == secondAbsolutePoint.Y)
                    continue;
                if (row[colId].HasValue)
                    continue;
                foreach (var candidate in candidates)
                {
                    if (row[colId].RemoveCandidate(candidate + 1, context, currentAbsolutePos, Message(candidates, firstAbsolutePoint, secondAbsolutePoint)))
                        numOfChanges++;
                }
            }

            return numOfChanges;
        }

        private int ProcessVerticalPair(List<int> candidates, Point firstAbsolutePoint, Point secondAbsolutePoint, Context context)
        {
            var numOfChanges = 0;

            var column = context.SudokuUnderSolution.Column(firstAbsolutePoint.Y);
            for (int rowId = 0; rowId < 9; rowId++) //todo 9
            {
                var currentAbsolutePos = new Point(rowId, firstAbsolutePoint.Y);
                if (currentAbsolutePos.X == firstAbsolutePoint.X || currentAbsolutePos.X == secondAbsolutePoint.X)
                    continue;
                if (column[rowId].HasValue)
                    continue;
                foreach (var candidate in candidates)
                {
                    if (column[rowId].RemoveCandidate(candidate + 1, context, currentAbsolutePos, Message(candidates, firstAbsolutePoint, secondAbsolutePoint)))
                        numOfChanges++;
                }
            }

            return numOfChanges;
        }

        private int ProcessBlockPair(List<int> candidates, Point firstAbsolutePoint, Point secondAbsolutePoint, Context context)
        {
            var numOfChanges = 0;

            var block = context.SudokuUnderSolution.Block(firstAbsolutePoint);
            var blockStartPos = new Point(firstAbsolutePoint.X / 3 * 3, firstAbsolutePoint.Y / 3 * 3);
            for (int blockI = 0; blockI < 3; blockI++) //todo 9
            {
                for (int blockJ = 0; blockJ < 3; blockJ++) //todo 9
                {
                    var currentAbsolutePos = new Point(blockStartPos.X + blockI, blockStartPos.Y + blockJ);
                    if (currentAbsolutePos == firstAbsolutePoint || currentAbsolutePos == secondAbsolutePoint)
                        continue;
                    if (block[blockI, blockJ].HasValue)
                        continue;
                    foreach (var candidate in candidates)
                    {
                        if (block[blockI, blockJ].RemoveCandidate(candidate + 1, context, currentAbsolutePos, Message(candidates, firstAbsolutePoint, secondAbsolutePoint)))
                            numOfChanges++;
                    }
                }
            }

            return numOfChanges;
        }

        private bool IsHorizontalPair(Point p1, Point p2)
        {
            return p1.X == p2.X;
        }

        private bool IsVerticalPair(Point p1, Point p2)
        {
            return p1.Y == p2.Y;
        }

        private bool IsSameBlockPair(Point p1, Point p2)
        {
            return p1.X / 3 == p2.X / 3 && p1.Y / 3 == p2.Y / 3;
        }

        private string Message(List<int> candidates, Point firstAbsolutePoint, Point secondAbsolutePoint) =>
            $"Found naked pair ({candidates[0] + 1},{candidates[1] + 1}) at {firstAbsolutePoint.ToSudokuCoords()} " +
            $"and {secondAbsolutePoint.ToSudokuCoords()}. Removing other candidates in block, row and column";
    }
}
