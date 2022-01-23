using System.Drawing;
using Application.Models;
using Application.Tools;
using Application.Tools.Enums;

namespace Application.MiscTodo.AlgoHiddenPairsRules
{
    public class ColumnHiddenPairRule : BaseHiddenPairRule
    {
        private int ApplyToColumn(int columnId, Context context)
        {
            var numOfChanges = 0;

            var statistics = InitializeStatistics(9); //todo 9

            var column = context.SudokuUnderSolution.Column(columnId);
            for (int rowId = 0; rowId < 9; rowId++)
            {
                var cell = column[rowId];
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
            for (int columnId = 0; columnId < 9; columnId++) //todo 9
            {
                numOfChanges += ApplyToColumn(
                    columnId,
                    context);
            }

            return numOfChanges;
        }

    }
}
