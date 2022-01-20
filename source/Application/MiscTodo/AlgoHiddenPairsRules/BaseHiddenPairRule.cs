using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Application.Models;
using Application.Tools;

namespace Application.MiscTodo.AlgoHiddenPairsRules
{
    public abstract class BaseHiddenPairRule
    {
        public abstract int ApplyToAll(Context context);

        protected int ProcessStatistics(List<DigitStatistics> statistics, Context context)
        {
            int numOfChanges = 0;

            for (int digit1 = 0; digit1 < 9; digit1++) //todo 9
            {
                if (statistics[digit1].NumOfOccurencies != 2)
                    continue;
                for (int digit2 = digit1 + 1; digit2 < 9; digit2++)
                {
                    if (statistics[digit1].NumOfOccurencies != 2)
                        continue;
                    if (!statistics[digit1].Positions.SequenceEqual(statistics[digit2].Positions))
                        continue;
                    if (context.SudokuUnderSolution[statistics[digit1].Positions[0]].NumOfRemainingCandidates == 2 &&
                        context.SudokuUnderSolution[statistics[digit1].Positions[1]].NumOfRemainingCandidates == 2)
                        continue;//we dont want to process hidden pairs as naked pairs
                    //reaching this point means that we have found a hidden pair
                    context.InitNewContextId();
                    numOfChanges += ProcessPair(new List<int> { digit1, digit2 },
                        statistics[digit1].Positions[0],
                        statistics[digit1].Positions[1],
                        context
                    );
                }
            }

            return numOfChanges;
        }

        private int ProcessPair(List<int> candidates, Point firstCellPosition, Point secondCellPosition, Context context)
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

        protected List<DigitStatistics> InitializeStatistics(int size)
        {
            var statistics = new List<DigitStatistics>(size);
            for (int i = 0; i < size; i++)
                statistics.Add(new DigitStatistics());
            return statistics;
        }

        private string Message(List<int> candidates, Point firstAbsolutePoint, Point secondAbsolutePoint) =>
            $"Found hidden pair ({candidates[0] + 1},{candidates[1] + 1}) at {firstAbsolutePoint.ToSudokuCoords()} " +
            $"and {secondAbsolutePoint.ToSudokuCoords()}. Removing other candidates in block, row and column";
    }
}
