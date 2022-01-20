using System;
using System.Collections.Generic;
using System.Drawing;
using Application.Models;
using Application.Models.SudokuAlgo;
using Application.Tools.Enums;

namespace Application.MiscTodo.AlgoCandidateScannerRules
{
    public abstract class BaseHiddenSingleRule
    {
        private int MaxDigits = 9;//todo 9
        protected int ApplyToSingle(SudokuCell[] setOfCells, Context context, string entityName, Func<int, Point> generatePosition)
        {
            context.HistoryEntryLevel = HistoryEntryLevel.SolutionValueSet;
            var numOfChangedCells = 0;
            var statistics = InitializeStatistics(MaxDigits);

            for (int entityId = 0; entityId < MaxDigits; entityId++)
            {
                //todo optimize: skip entity if it already has all values set
                var cell = setOfCells[entityId];
                if (cell.HasValue)
                {
                    statistics[cell.Value - 1].NumOfOccurencies = 999;
                    continue;
                }
                //todo optimize: already know which digits are missing
                for (int candidateId = 0; candidateId < 9; candidateId++) //todo 9
                {
                    if (cell.HasCandidate(candidateId) == false)
                        continue;
                    statistics[candidateId].NumOfOccurencies++;
                    statistics[candidateId].LatestPosition = generatePosition(entityId);


                }
            }

            //digits which have only one suitable position must be set
            for (int digit = 0; digit < 9; digit++)
            {
                if (statistics[digit].NumOfOccurencies == 1)
                {
                    //todo refactor. requite this data for value set
                    context.CellUnderAction = statistics[digit].LatestPosition;
                    context.SudokuUnderSolution[statistics[digit].LatestPosition].SetValue(digit + 1, context, $"this is the only cell in a {entityName} that can have this value");
                    numOfChangedCells++;
                }
            }

            return numOfChangedCells;
        }

        private List<DigitStatistics> InitializeStatistics(int size)
        {
            var statistics = new List<DigitStatistics>(9); //todo 9
            for (int i = 0; i < 9; i++)
                statistics.Add(new DigitStatistics());
            return statistics;
        }

        public abstract int ApplyToAll(Context context);
    }
}
