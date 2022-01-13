﻿using System.Collections.Generic;
using System.Drawing;
using Application.Infrastructure;
using Application.Models.SudokuAlgo;

namespace Application.MiscTodo.AlgoRestrictions
{
    public class NonRepeatingColumnDigitsBasicRule : BasicRule
    {
        public override List<Point> ApplyRule(Sudoku sudoku, Point position)
        {
            var cellsThatAcquiredValue = new List<Point>();
            var cellUnderRule = sudoku[position];

            if (!cellUnderRule.HasValue)
                return cellsThatAcquiredValue;

            var column = sudoku.Column(position);
            for (int i = 0; i < column.Length; i++)
            {
                var columnCell = column[i];
                if (cellUnderRule == columnCell)
                    continue;
                if (columnCell.HasValue)
                    continue;
                if (columnCell.RemoveCandidate(cellUnderRule.Value) == CandidateRemovalResult.RemovedAndHasSingleValue)
                    cellsThatAcquiredValue.Add(new Point(i, position.Y));
            }

            return cellsThatAcquiredValue;
        }
    }
}