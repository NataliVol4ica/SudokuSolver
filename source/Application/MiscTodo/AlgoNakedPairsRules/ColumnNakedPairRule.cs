using System.Drawing;
using System.Linq;
using Application.Models;
using Application.Tools.Enums;

namespace Application.MiscTodo.AlgoNakedPairsRules
{
    public class ColumnNakedPairRule : BaseNakedPairRule
    {
        private int ApplyToColumn(int columnId, Context context)
        {
            var numOfChanges = 0;

            var column = context.SudokuUnderSolution.Column(columnId);

            for (int rowId1 = 0; rowId1 < 9; rowId1++) //todo 9
            {
                if (column[rowId1].NumOfRemainingCandidates != 2)
                    continue;
                if (rowId1 == 8)
                    continue;
                var firstCell = column[rowId1];
                var firstCellCandidates = firstCell.GetCandidates();
                for (int rowId2 = rowId1 + 1; rowId2 < 9; rowId2++) //todo 9
                {
                    if (column[rowId2].NumOfRemainingCandidates != 2)
                        continue;
                    if (!firstCellCandidates.SequenceEqual(column[rowId2].GetCandidates()))
                        continue;
                    //reaching this point means that we have found a naked pair
                    context.InitNewContextId();
                    var firstCellAbsolutePosition = new Point(rowId1, columnId);
                    var secondCellAbsolutePosition = new Point(rowId2, columnId);
                    ProcessPair(firstCellCandidates, firstCellAbsolutePosition, secondCellAbsolutePosition, context);
                }
            }

            return numOfChanges;
        }

        public override int ApplyToAll(Context context)
        {
            context.HistoryEntryLevel = HistoryEntryLevel.CandidateSet;

            var numOfChanges = 0;
            for (int colId = 0; colId < 9; colId++) //todo 9
            {
                numOfChanges += ApplyToColumn(
                    colId,
                    context);
            }

            return numOfChanges;
        }

    }
}
