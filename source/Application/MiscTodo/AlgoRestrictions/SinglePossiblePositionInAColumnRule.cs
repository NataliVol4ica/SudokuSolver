//using System.Collections.Generic;
//using System.Drawing;
//using Application.MiscTodo.AlgoRestrictions.Models;
//using Application.Models.SudokuAlgo;
//using Application.Models.SudokuAlgo.History;

//namespace Application.MiscTodo.AlgoRestrictions
//{
//    public class SinglePossiblePositionInAColumnRule : BasicRule
//    {
//        public override List<Point> ApplyRule(Sudoku sudoku, Point position, SolutionHistory history)
//        {
//            var cellsThatAcquiredValue = new List<Point>();
//            var column = sudoku.Column(position);

//            var statistics = new List<DigitStatistics>(9); //todo 9
//            for (int i = 0; i < 9; i++)
//                statistics.Add(new DigitStatistics());

//            //for each digit, count the number of occurencies in a row
//            for (int i = 0; i < column.Length; i++)
//            {
//                //todo optimize: skip column if it already has all values set
//                var columnCell = column[i];
//                if (columnCell.HasValue)
//                    continue;
//                for (int candidateId = 0; candidateId < 9; candidateId++) //todo 9
//                {
//                    if (columnCell.Candidates[candidateId] == false)
//                        continue;
//                    statistics[candidateId].NumOfOccurencies++;
//                    statistics[candidateId].LatestPosition = new Point(i, position.Y);
//                }
//            }

//            //digits which have only one suitable position must be set
//            for (int digit = 0; digit < 9; digit++)
//            {
//                if (statistics[digit].NumOfOccurencies == 1)
//                {
//                    sudoku[statistics[digit].LatestPosition].SetValue(digit + 1);
//                    cellsThatAcquiredValue.Add(statistics[digit].LatestPosition);
//                    history.AddEntry(sudoku, statistics[digit].LatestPosition, digit + 1, "this is the only cell in a COLUMN that can have this value");
//                }
//            }

//            return cellsThatAcquiredValue;
//        }
//    }
//}
