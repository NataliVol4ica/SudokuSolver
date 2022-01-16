using System.Collections.Generic;
using System.Drawing;
using Application.Models;
using Application.Tools.Enums;

namespace Application.MiscTodo.AlgoHiddenPairsRules
{
    public class HiddenPairRule
    {
        //todo
        private int ApplyToBlock(int blockId, Context context)
        {
            var numOfChanges = 0;

            var statistics = InitializeStatistics(9);

            var block = context.SudokuUnderSolution.Block(blockId);
            for (int i = 0; i < 3; i++) //todo 9
            {
                for (int j = 0; j < 3; j++)
                {
                    var cell = block[i, j];
                    if (cell.HasValue)
                        continue;
                    for (int candidateId = 0; candidateId < 9; candidateId++) //todo 9
                    {
                        if (cell.HasCandidate(candidateId))
                        {
                            statistics[candidateId].NumOfOccurencies++;
                            statistics[candidateId].Positions.Add(new Point(blockId / 3 + i, blockId % 3 + j));
                        }

                    }
                }
            }

            return numOfChanges;
        }

        private List<DigitStatistics> InitializeStatistics(int size)
        {
            var statistics = new List<DigitStatistics>(9); //todo 9
            for (int i = 0; i < 9; i++)
                statistics.Add(new DigitStatistics());
            return statistics;
        }

        public int ApplyToAll(Context context)
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
