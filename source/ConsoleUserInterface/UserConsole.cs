using System;
using System.Collections.Generic;
using Application.Models;
using Application.Models.SudokuAlgo.History.SolutionHistoryNavigation;

namespace ConsoleUserInterface
{

    //todo: allow to switch simple/detailed sudoku mode.
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

        private bool detailedMode = true;

        public void Start(Context solution)
        {
            //todo split into smaller methods
            Command command;

            Console.WriteLine($"Sudoku is fully solved? {solution.IsSudokuSolved}");

            var historyViewer = SolutionHistoryNavigator.Create(solution.History);

            var firstEntry = historyViewer.GetNextEntry();
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
                        PrintResult(historyViewer.GetNextEntry());
                        break;
                    case Command.Previous:
                        PrintResult(historyViewer.GetPreviousEntry());
                        break;
                    case Command.First:
                        PrintResult(historyViewer.GetFirstEntry());
                        break;
                    case Command.Last:
                        PrintResult(historyViewer.GetLastEntry());
                        break;
                    case Command.Decrease10:
                        PrintResult(historyViewer.JumpToEntry(-10));
                        break;
                    case Command.Increase10:
                        PrintResult(historyViewer.JumpToEntry(10));
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

        private void PrintResult(SolutionHistoryNavigationEntry entry)
        {
            entry.Print(detailedMode);
        }

        private readonly Dictionary<ConsoleKey, Command> _keyToCommands = new Dictionary<ConsoleKey, Command>
        {
            { ConsoleKey.Escape, Command.Exit },
            { ConsoleKey.LeftArrow, Command.Previous },
            { ConsoleKey.RightArrow, Command.Next },
            { ConsoleKey.PageDown, Command.Last },
            { ConsoleKey.PageUp, Command.First },
            { ConsoleKey.UpArrow, Command.Decrease10 },
            { ConsoleKey.DownArrow, Command.Increase10 },
            { ConsoleKey.F1, Command.Help },
        };

        private Command ReadInputKey()
        {
            var button = Console.ReadKey(false).Key;
            if (_keyToCommands.TryGetValue(button, out var result))
                return result;
            return Command.None;
        }
    }
}
