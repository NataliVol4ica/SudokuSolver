using System.Drawing;
using Application.Models;
using Application.Tools.Enums;

namespace Application.MiscTodo.AlgoHiddenPairsRules
{
    public class BlockHiddenPairRule : BaseHiddenPairRule
    {
        private int ApplyToBlock(int blockId, Context context)
        {
            var numOfChanges = 0;

            var statistics = InitializeStatistics(9); //todo 9

            var block = context.SudokuUnderSolution.Block(blockId);
            for (int blockRowId = 0; blockRowId < 3; blockRowId++) //todo 9
            {
                for (int blockColumnId = 0; blockColumnId < 3; blockColumnId++)
                {
                    var cell = block[blockRowId, blockColumnId];
                    if (cell.HasValue)
                        continue;
                    for (int candidateId = 0; candidateId < 9; candidateId++) //todo 9
                    {
                        if (cell.HasCandidate(candidateId))
                        {
                            statistics[candidateId].NumOfOccurencies++;
                            statistics[candidateId].Positions.Add(new Point(blockId / 3 * 3 + blockRowId, blockId % 3 * 3 + blockColumnId));
                        }
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
