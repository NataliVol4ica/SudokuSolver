using System;
using Application.MiscTodo.FileAccess;
using Application.MiscTodo.UserInput;
using Application.Services;

namespace SudokuSolver
{
    public class Program
    {
        public static void Main()
        {
            string path = @"D:\DotNet\SudokuSolver\samples\Classic\Simple00_3x3.sud";
            var sudoku = new SudokuReader().ReadFrom(path);
            var solution = new Solver().Solve(sudoku);
            new UserConsole().Start(solution);
            Console.ReadKey();
        }
    }
}
