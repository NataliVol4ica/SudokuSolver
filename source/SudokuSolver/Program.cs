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
            string path = @"D:\DotNet\SudokuSolver\samples\Classic\BestClaimedByYoutube00_3x3.sud";
            var sudoku = new SudokuReader().ReadFrom(path);
            SudokuPrinter.Print(sudoku);
            Console.WriteLine($"[{DateTime.Now}] Solving sudoku.");
            Console.WriteLine("\n===========================================================");
            var solution = new Solver().Solve(sudoku);
            Console.WriteLine($"[{DateTime.Now}] Solving is complete. See the results:");
            Console.WriteLine("\n===========================================================");
            new UserConsole().Start(solution);
            Console.ReadKey();
        }
    }
}
