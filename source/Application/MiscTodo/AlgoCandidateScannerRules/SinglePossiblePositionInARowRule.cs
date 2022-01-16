using System.Collections.Generic;
using System.Drawing;
using Application.MiscTodo.AlgoRestrictions.Models;
using Application.Models;

namespace Application.MiscTodo.AlgoCandidateScannerRules
{
    public class SinglePossiblePositionInARowRule
    {
        private int ApplyToRow(int rowId, Context context)
        {
            var row = context.SudokuUnderSolution.Row(rowId);
            var numOfChangedCells = 0;
            
            //todo repetitive code
            var statistics = new List<DigitStatistics>(9); //todo 9
            for (int i = 0; i < 9; i++)
                statistics.Add(new DigitStatistics());

            //for each digit, count the number of occurencies in a row
            for (int i = 0; i < row.Length; i++)
            {
                //todo optimize: skip column if it already has all values set
                var cell = row[i];
                if (cell.HasValue)
                {
                    statistics[cell.Value - 1].NumOfOccurencies = 999;
                    continue;
                }
                //todo optimize: already know which digits are missing
                for (int candidateId = 0; candidateId < 9; candidateId++) //todo 9
                {
                    if (cell.Candidates[candidateId] == false)
                        continue;
                    statistics[candidateId].NumOfOccurencies++;
                    statistics[candidateId].LatestPosition = new Point(rowId, i);
                }
            }
             
            //digits which have only one suitable position must be set
            for (int digit = 0; digit < 9; digit++)
            {
                if (statistics[digit].NumOfOccurencies == 1)
                {
                    //todo refactor. requite this data for value set
                    context.CellUnderAction = statistics[digit].LatestPosition;
                    context.SudokuUnderSolution[statistics[digit].LatestPosition].SetValue(digit + 1, context, true, "this is the only cell in a ROW that can have this value");
                    numOfChangedCells++;
                }
            }

            return numOfChangedCells;
        }

        public int ApplyToAll(Context context)
        {
            var numOfNewValueCells = 0;
            for (int rowId = 0; rowId < 9; rowId++) //todo 9
            {
                numOfNewValueCells += ApplyToRow(rowId, context);
            }

            return numOfNewValueCells;
        }
    }
}
