using System;
using Application.Models;
using Application.Models.SudokuAlgo.History;

namespace Application.MiscTodo.UserInput
{
    public class UserConsole
    {
        enum Command
        {
            None = 0,
            Next = 1,
            Previous,
            Last,
            First,
            Increase10,
            Decrease10,
            Help,
            Exit
        }

        public void Start(Context solution)
        {
            //todo split into smaller methods
            Command command;
            
            Console.WriteLine($"Sudoku is fully solved? {solution.SudokuUnderSolution.IsSolved()}");
            
            var firstEntry = solution.History.GetNextEntry();
            if (firstEntry is null)
            {
                Console.WriteLine("Failed to find even a single digit.");
                PrintGoodbye();
                return;
            }
            PrintResult(firstEntry);

            while ((command = ReadInputKey()) != Command.Exit)
            {
                switch (command)
                {
                    case Command.Help:
                        PrintHelp();
                        break;
                    case Command.Next:
                        PrintResult(solution.History.GetNextEntry());
                        break;
                    case Command.Previous:
                        PrintResult(solution.History.GetPreviousEntry());
                        break;
                    case Command.First:
                        PrintResult(solution.History.GetFirstEntry());
                        break;
                    case Command.Last:
                        PrintResult(solution.History.GetLastEntry());
                        break;
                    case Command.Decrease10:
                        PrintResult(solution.History.JumpToEntry(-10));
                        break;
                    case Command.Increase10:
                        PrintResult(solution.History.JumpToEntry(10));
                        break;
                }
            }
        }

        private void PrintGoodbye()
        {
            Console.WriteLine("\n===========================================================");
            Console.WriteLine("Goodbye!");
            Console.ReadKey();
        }

        private void PrintHelp()
        {
            Console.WriteLine("TODO :D");
        }

        private void PrintResult(SolutionHistoryEntryForPrint entry)
        {
            if (entry is null)
                return;
            var stepText = entry.ToString();
            if (entry.IsLast)
                Console.Write("FINAL.");
            else if (entry.IsFirst)
                Console.Write("FIRST.");
            entry.Print();
            Console.WriteLine("\n");
        }

        private Command ReadInputKey()
        {
            var button = Console.ReadKey(false).Key;
         
            //todo: make a dictionary for help
            switch (button)
            {
                case ConsoleKey.Escape:
                    return Command.Exit;
                case ConsoleKey.LeftArrow:
                    return Command.Previous;
                case ConsoleKey.RightArrow:
                    return Command.Next;
                case ConsoleKey.PageDown:
                    return Command.Last;
                case ConsoleKey.PageUp:
                    return Command.First;
                case ConsoleKey.UpArrow:
                    return Command.Decrease10;
                case ConsoleKey.DownArrow:
                    return Command.Increase10;
                case ConsoleKey.F1:
                    return Command.Help;
                default:
                    return Command.None;
            }
        }
    }
}
