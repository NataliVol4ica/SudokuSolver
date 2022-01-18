using System.Collections.Generic;
using System.Drawing;
using Application.Models;
using Application.Tools;

namespace Application.MiscTodo.AlgoHiddenPairsRules
{
    public abstract class BaseHiddenPairRule
    {
        public abstract int ApplyToAll(Context context);

        protected int ProcessPair(List<int> candidates, Point firstCellPosition, Point secondCellPosition, Context context)
        {
            var numOfChanges = 0;
            var message = Message(candidates, firstCellPosition, secondCellPosition);
            numOfChanges += RemoveCellCandidatesExcept(context, firstCellPosition, candidates, message);
            numOfChanges += RemoveCellCandidatesExcept(context, secondCellPosition, candidates, message);
            numOfChanges += AlgoSharedTools.Tools.RemoveCandidatesForPairs(
                candidates,
                firstCellPosition,
                secondCellPosition,
                context,
                message);
            return numOfChanges;
        }

        private int RemoveCellCandidatesExcept(Context context, Point cellPosition, List<int> except, string message)
        {
            var numOfChanges = 0;
            var cell = context.SudokuUnderSolution[cellPosition];
            //todo 9
            for (int candidate = 0; candidate < 9; candidate++)
            {
                if (except.Contains(candidate))
                    continue;
                if (cell.RemoveCandidate(candidate + 1, context, cellPosition, message))
                    numOfChanges++;
            }

            return numOfChanges;
        }

        private string Message(List<int> candidates, Point firstAbsolutePoint, Point secondAbsolutePoint) =>
            $"Found hidden pair ({candidates[0] + 1},{candidates[1] + 1}) at {firstAbsolutePoint.ToSudokuCoords()} " +
            $"and {secondAbsolutePoint.ToSudokuCoords()}. Removing other candidates in block, row and column";
    }
}
