using System;
using Application.Models.SudokuAlgo.History.SolutionHistoryNavigation;

namespace ConsoleUserInterface
{
    public static class SolutionHistoryNavigationEntryExtensions
    {
        public static void Print(this SolutionHistoryNavigationEntry entry, bool isDetailed)
        {
            if (entry is null)
                return;
            if (!string.IsNullOrEmpty(entry.Message))
            {
                Console.WriteLine(entry.Message);
            }

            if (entry.SudokuSnapshot != null)
            {
                if (isDetailed)
                    ExtendedSudokuPrinter.Print(entry.SudokuSnapshot, entry.RemovedCandidates,
                        entry.HighlightedCandidates,
                        entry.CellValueSet?.Position);
                else
                {
                    BasicSudokuPrinter.Print(entry.SudokuSnapshot, entry.CellValueSet?.Position);
                }
            }
        }
    }
}
