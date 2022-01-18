using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Application.Models;
using Application.Tools.Enums;

namespace Application.MiscTodo.AlgoHiddenPairsRules
{
    public class HiddenPairRule : BaseHiddenPairRule
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
                    ProcessPair(new List<int> { digit1, digit2 },
                        statistics[digit1].Positions[0],
                        statistics[digit1].Positions[1],
                        context
                    );
                }
            }

            return numOfChanges;
        }
        

        private List<DigitStatistics> InitializeStatistics(int size)
        {
            var statistics = new List<DigitStatistics>(size);
            for (int i = 0; i < size; i++)
                statistics.Add(new DigitStatistics());
            return statistics;
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
