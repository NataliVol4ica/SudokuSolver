using System.Collections.Generic;
using System.Drawing;
using Application.Infrastructure;
using Application.Models.SudokuAlgo;

namespace Application.MiscTodo.AlgoRestrictions
{
    public class NonRepeatingRowDigitsBasicRule : BasicRule
    {
        public override List<Point> ApplyRule(Sudoku sudoku, Point position)
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
                    cellsThatAcquiredValue.Add(new Point(position.X, i));
            }

            return cellsThatAcquiredValue;
        }
    }
}
