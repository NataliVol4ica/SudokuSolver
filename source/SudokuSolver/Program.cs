using System;
using Application.MiscTodo.FileAccess;
using Application.MiscTodo.UserInput;
using Application.Models;
using Application.Services;

namespace SudokuSolver
{
    public class Program
    {
        public static void Main()
        {
            string path = @"D:\DotNet\SudokuSolver\samples\Classic\BestClaimedByYoutube00_3x3.sud";
            //string path = @"D:\DotNet\SudokuSolver\samples\Classic\Simple00_3x3.sud";
            var context = new Context();
            new SudokuReader(context).ReadFrom(path);
            ExtendedSudokuPrinter.Print(context.SudokuUnderSolution);
            Console.WriteLine($"[{DateTime.Now}] Solving sudoku.");
            Console.WriteLine("\n===========================================================");
            new Solver(context).Solve();
            Console.WriteLine($"[{DateTime.Now}] Solving is complete. See the results:");
            Console.WriteLine("\n===========================================================");
            new UserConsole().Start(context);
            Console.ReadKey();
        }
    }
}
