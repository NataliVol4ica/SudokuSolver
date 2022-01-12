using System.Collections.Generic;

namespace Application.Models.SudokuAlgo
{
    public class SudokuSolution
    {
        public bool IsSolved { get; set; } = false;

        public Sudoku SolvedSudoku { get; set; }

        public List<SolutionStep> Steps { get; set; } 
    }
}
