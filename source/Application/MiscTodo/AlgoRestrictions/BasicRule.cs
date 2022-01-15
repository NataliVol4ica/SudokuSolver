﻿using System.Collections.Generic;
using System.Drawing;
using Application.Models.SudokuAlgo;
using Application.Models.SudokuAlgo.History;

namespace Application.MiscTodo.AlgoRestrictions
{
    public abstract class BasicRule
    {
        public abstract List<Point> ApplyRule(Sudoku sudoku, Point position, SolutionHistory history);
    }
}