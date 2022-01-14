namespace Application.Models.SudokuAlgo
{
    public class SudokuSolution
    {
        public bool IsSolved => Sudoku.IsSolved();

        public Sudoku Sudoku { get; set; }

        public SolutionHistory History { get; set; } 
    }
}
