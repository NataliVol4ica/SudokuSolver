using System.Drawing;
using System.Linq;
using Application.Models;
using Application.Tools.Enums;

namespace Application.MiscTodo.AlgoNakedPairsRules
{
    public class BlockNakedPairRule : BaseNakedPairRule
    {
        private int ApplyToBlock(int blockId, Context context)
        {
            var numOfChanges = 0;

            var blockToApply = context.SudokuUnderSolution.PlainBlock(blockId);

            for (int i = 0; i < 9; i++) //todo 9
            {
                if (blockToApply[i].NumOfRemainingCandidates != 2)
                    continue;
                if (i == 8)
                    continue;
                var firstCell = blockToApply[i];
                var firstCellCandidates = firstCell.GetCandidates();
                for (int j = i + 1; j < 9; j++) //todo 9
                {
                    if (blockToApply[j].NumOfRemainingCandidates != 2)
                        continue;
                    if (!firstCellCandidates.SequenceEqual(blockToApply[j].GetCandidates()))
                        continue;
                    //reaching this point means that we have found a naked pair
                    context.InitNewContextId();
                    var firstCellAbsolutePosition = new Point(blockId / 3 * 3 + i / 3, (blockId % 3 * 3) + i % 3);
                    var secondCellAbsolutePosition = new Point(blockId / 3 * 3 + j / 3, (blockId % 3 * 3) + j % 3);
                    ProcessPair(firstCellCandidates, firstCellAbsolutePosition, secondCellAbsolutePosition, context);
                }
            }

            return numOfChanges;
        }

        public override int ApplyToAll(Context context)
        {
            context.HistoryEntryLevel = HistoryEntryLevel.CandidateSet;

            var numOfChanges = 0;
            for (int blockId = 0; blockId < 9; blockId++) //todo 9
            {
                numOfChanges += ApplyToBlock(
                    blockId,
                    context);
            }

            return numOfChanges;
        }

    }
}
