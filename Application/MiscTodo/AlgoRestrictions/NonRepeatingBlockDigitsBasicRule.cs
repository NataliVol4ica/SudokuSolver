using System.Collections.Generic;
using System.Drawing;
using Application.Infrastructure;
using Application.Models.SudokuAlgo;

namespace Application.MiscTodo.AlgoRestrictions
{
    public class NonRepeatingBlockDigitsBasicRule : BasicRule
    {
        public override List<Point> ApplyRule(Sudoku sudoku, Point position, SolutionHistory history) 
        {
            var cellsThatAcquiredValue = new List<Point>();
            var cellUnderRule = sudoku[position];

            if (!cellUnderRule.HasValue)
                return cellsThatAcquiredValue;

            var block = sudoku.Block(position);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var blockCell = block[i, j];
                    if (cellUnderRule == blockCell)
                        continue;
                    if (blockCell.HasValue)
                        continue;
                    if (blockCell.RemoveCandidate(cellUnderRule.Value) ==
                        CandidateRemovalResult.RemovedAndHasSingleValue)
                    {
                        var pos = new Point(position.X / 3 * 3 + i, position.Y / 3 * 3 + j);
                        cellsThatAcquiredValue.Add(pos);
                        history.AddEntry(sudoku, pos, blockCell.Value, "the digit is the only candidate that can be put in this cell");
                    }
                }
            }

            return cellsThatAcquiredValue;
        }
    }
}
