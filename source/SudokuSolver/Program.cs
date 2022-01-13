using System;
using Application.MiscTodo.FileAccess;
using Application.Services;

namespace SudokuSolver
{
    public class Program
    {
        public static void Main()
        {
            string path = @"D:\DotNet\SudokuSolver\samples\Classic\BestClaimedByYoutube00_3x3.sud";
            var sudoku = new SudokuReader().ReadFrom(path);
            Console.WriteLine(sudoku);
            var solution = new Solver().Solve(sudoku);
            //new UserConsole().Start(solution);
            Console.ReadKey();
        }
    }
}
