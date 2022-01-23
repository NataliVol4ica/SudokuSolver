using System.Collections.Generic;
using System.Drawing;
using Application.MiscTodo.AlgoSharedTools;
using Application.Models;
using Application.Tools;

namespace Application.MiscTodo.AlgoNakedPairsRules
{
    public abstract class BaseNakedPairRule
    {
        public abstract int ApplyToAll(Context context);

        protected int ProcessPair(List<int> candidates, Point firstCellAbsolutePosition, Point secondCellAbsolutePosition, Context context)
        {
            var message = Message(candidates, firstCellAbsolutePosition, secondCellAbsolutePosition);

            var numOfChanges = context.RemoveCandidatesForPairs(candidates, firstCellAbsolutePosition, secondCellAbsolutePosition, message);

            if (numOfChanges > 0)
            {
                foreach (var candidate in candidates)
                {
                    context.History.AddHighlightCandidateEntry(candidate + 1, context, firstCellAbsolutePosition, message);
                    context.History.AddHighlightCandidateEntry(candidate + 1, context, secondCellAbsolutePosition, message);
                }
            }

            return numOfChanges;
        }

        private string Message(List<int> candidates, Point firstAbsolutePoint, Point secondAbsolutePoint) =>
            $"Found naked pair ({candidates[0] + 1},{candidates[1] + 1}) at {firstAbsolutePoint.ToSudokuCoords()} " +
            $"and {secondAbsolutePoint.ToSudokuCoords()}. Removing other candidates in block, row and column";
    }
}
