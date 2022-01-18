using System.Drawing;
using System.Linq;
using Application.Models;
using Application.Tools.Enums;

namespace Application.MiscTodo.AlgoNakedPairsRules
{
    public class RowNakedPairRule : BaseNakedPairRule
    {
        private int ApplyToRow(int rowId, Context context)
        {
            var numOfChanges = 0;

            var row = context.SudokuUnderSolution.Row(rowId);

            for (int columnId1 = 0; columnId1 < 9; columnId1++) //todo 9
            {
                if (row[columnId1].NumOfRemainingCandidates != 2)
                    continue;
                if (columnId1 == 8)
                    continue;
                var firstCell = row[columnId1];
                var firstCellCandidates = firstCell.GetCandidates();
                for (int columnId2 = columnId1 + 1; columnId2 < 9; columnId2++) //todo 9
                {
                    if (row[columnId2].NumOfRemainingCandidates != 2)
                        continue;
                    if (!firstCellCandidates.SequenceEqual(row[columnId2].GetCandidates()))
                        continue;
                    //reaching this point means that we have found a naked pair
                    context.InitNewContextId();
                    var firstCellAbsolutePosition = new Point(rowId, columnId1);
                    var secondCellAbsolutePosition = new Point(rowId, columnId2);
                    ProcessPair(firstCellCandidates, firstCellAbsolutePosition, secondCellAbsolutePosition, context);
                }
            }

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
