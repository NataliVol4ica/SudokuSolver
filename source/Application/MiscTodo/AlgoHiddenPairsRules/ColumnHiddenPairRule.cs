using System.Drawing;
using Application.Models;
using Application.Tools.Enums;

namespace Application.MiscTodo.AlgoHiddenPairsRules
{
    public class RowHiddenPairRule : BaseHiddenPairRule
    {
        private int ApplyToRow(int rowId, Context context)
        {
            var numOfChanges = 0;

            var statistics = InitializeStatistics(9); //todo 9

            var row = context.SudokuUnderSolution.Row(rowId);
            for (int columnId = 0; columnId < 9; columnId++)
            {
                var cell = row[columnId];
                if (cell.HasValue)
                    continue;
                for (int candidateId = 0; candidateId < 9; candidateId++) //todo 9
                {
                    if (cell.HasCandidate(candidateId))
                    {
                        statistics[candidateId].NumOfOccurencies++;
                        statistics[candidateId].Positions.Add(new Point(rowId, columnId));
                    }
                }
            }

            numOfChanges += ProcessStatistics(statistics, context);

            return numOfChanges;
        }

        public override int ApplyToAll(Context context)
        {
            context.HistoryEntryLevel = HistoryEntryLevel.CandidateSet;

            var numOfChanges = 0;
            for (int rowId = 0; rowId < 9; rowId++) //todo 9
            {
                numOfChanges += ApplyToRow(
                    rowId,
                    context);
            }

            return numOfChanges;
        }

    }
}
