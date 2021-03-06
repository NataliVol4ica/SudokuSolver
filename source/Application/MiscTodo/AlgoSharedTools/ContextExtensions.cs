using System.Collections.Generic;
using System.Drawing;
using Application.Models;

namespace Application.MiscTodo.AlgoSharedTools
{
    public static class ContextExtensions
    {
        public static int RemoveCandidatesForPairs(this Context context, List<int> candidates, Point firstAbsolutePoint, Point secondAbsolutePoint, string message)
        {
            var numOfChanges = 0;
            if (Utilities.IsHorizontalPair(firstAbsolutePoint, secondAbsolutePoint))
            {
                numOfChanges += ProcessHorizontalPair(candidates, firstAbsolutePoint, secondAbsolutePoint, context,
                    message);
            }

            if (Utilities.IsVerticalPair(firstAbsolutePoint, secondAbsolutePoint))
            {
                numOfChanges +=
                    ProcessVerticalPair(candidates, firstAbsolutePoint, secondAbsolutePoint, context, message);
            }

            if (Utilities.IsSameBlockPair(firstAbsolutePoint, secondAbsolutePoint))
            {
                numOfChanges += ProcessBlockPair(candidates, firstAbsolutePoint, secondAbsolutePoint, context, message);
            }

            return numOfChanges;
        }

        private static int ProcessHorizontalPair(List<int> candidates, Point firstAbsolutePoint,
            Point secondAbsolutePoint, Context context, string message)
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
                    if (row[colId].RemoveCandidate(candidate, context, currentAbsolutePos, message))
                        numOfChanges++;
                }
            }

            return numOfChanges;
        }

        private static int ProcessVerticalPair(List<int> candidates, Point firstAbsolutePoint,
            Point secondAbsolutePoint, Context context, string message)
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
                    if (column[rowId].RemoveCandidate(candidate, context, currentAbsolutePos, message))
                        numOfChanges++;
                }
            }

            return numOfChanges;
        }

        private static int ProcessBlockPair(List<int> candidates, Point firstAbsolutePoint, Point secondAbsolutePoint,
            Context context, string message)
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
                        if (block[blockI, blockJ].RemoveCandidate(candidate, context, currentAbsolutePos, message))
                            numOfChanges++;
                    }
                }
            }

            return numOfChanges;
        }

        public static int RemoveCellCandidatesExcept(this Context context, Point cellPosition, List<int> except, string message)
        {
            var numOfChanges = 0;
            var cell = context.SudokuUnderSolution[cellPosition];
            //todo 9
            for (int candidate = 0; candidate < 9; candidate++)
            {
                if (except.Contains(candidate))
                    continue;
                if (cell.RemoveCandidate(candidate, context, cellPosition, message))
                    numOfChanges++;
            }

            return numOfChanges;
        }
    }
}
