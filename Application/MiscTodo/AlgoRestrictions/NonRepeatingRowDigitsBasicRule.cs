using System.Collections.Generic;
using System.Drawing;
using Application.Infrastructure;
using Application.Models.SudokuAlgo;

namespace Application.MiscTodo.AlgoRestrictions
{
    public class NonRepeatingRowDigitsBasicRule : BasicRule
    {
        public override List<Point> ApplyRule(Sudoku sudoku, Point position, SolutionHistory history)
        {
            var cellsThatAcquiredValue = new List<Point>();
            var cellUnderRule = sudoku[position];

            if (!cellUnderRule.HasValue)
                return cellsThatAcquiredValue;

            var row = sudoku.Row(position);
            for (int i = 0; i < row.Length; i++)
            {
                var rowCell = row[i];
                if (cellUnderRule == rowCell)
                    continue;
                if (rowCell.HasValue)
                    continue;
                if (rowCell.RemoveCandidate(cellUnderRule.Value) == CandidateRemovalResult.RemovedAndHasSingleValue)
                {
                    var pos = new Point(position.X, i);
                    cellsThatAcquiredValue.Add(pos);
                    history.AddEntry(sudoku, pos, rowCell.Value, "the digit is the only candidate that can be put in this cell");
                }
            }

            return cellsThatAcquiredValue;
        }
    }
}
