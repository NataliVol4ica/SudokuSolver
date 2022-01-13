using System.Collections.Generic;
using System.Drawing;
using Application.MiscTodo.AlgoRestrictions.Models;
using Application.Models.SudokuAlgo;

namespace Application.MiscTodo.AlgoRestrictions
{
    public class SinglePossiblePositionInARowRule : BasicRule
    {
        public override List<Point> ApplyRule(Sudoku sudoku, Point position)
        {
            var cellsThatAcquiredValue = new List<Point>();
            var row = sudoku.Row(position);

            var statistics = new List<DigitStatistics>(9); //todo 9
            for (int i = 0; i < 9; i++)
                statistics.Add(new DigitStatistics());
            
            //for each digit, count the number of occurencies in a row
            for (int i = 0; i < row.Length; i++)
            {
                //todo optimize: skip row if it already has all values set
                var rowCell = row[i];
                if (rowCell.HasValue)
                    continue;
                for (int candidateId = 0; candidateId < 9; candidateId++) //todo 9
                {
                    if (rowCell.Candidates[candidateId] == false)
                        continue;
                    statistics[candidateId].NumOfOccurencies++;
                    statistics[candidateId].LatestPosition = new Point(position.X, i);
                }
            }

            //digits which have only one suitable position must be set
            for (int digit = 0; digit < 9; digit++)
            {
                if (statistics[digit].NumOfOccurencies == 1)
                {
                    sudoku[statistics[digit].LatestPosition].SetValue(digit + 1);
                    cellsThatAcquiredValue.Add(statistics[digit].LatestPosition);
                }
            }

            return cellsThatAcquiredValue;
        }
    }
}
