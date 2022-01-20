using System.Collections.Generic;
using System.Drawing;
using Application.Models;
using Application.Tools;

namespace Application.MiscTodo.AlgoNakedPairsRules
{
    public abstract class BaseNakedPairRule
    {
        public abstract int ApplyToAll(Context context);

        protected int ProcessPair(List<int> firstCellCandidates, Point firstCellAbsolutePosition, Point secondCellAbsolutePosition, Context context)
        {
            return AlgoSharedTools.Tools.RemoveCandidatesForPairs(
                firstCellCandidates,
                firstCellAbsolutePosition,
                secondCellAbsolutePosition,
                context,
                Message(firstCellCandidates, firstCellAbsolutePosition, secondCellAbsolutePosition));
        }

        private string Message(List<int> candidates, Point firstAbsolutePoint, Point secondAbsolutePoint) =>
            $"Found naked pair ({candidates[0] + 1},{candidates[1] + 1}) at {firstAbsolutePoint.ToSudokuCoords()} " +
            $"and {secondAbsolutePoint.ToSudokuCoords()}. Removing other candidates in block, row and column";
    }
}
