using System;
using Application.Models.SudokuAlgo.History.SolutionHistoryNavigation;
using Application.Tools;

namespace ConsoleUserInterface
{
    public static class SolutionHistoryNavigationEntryExtensions
    {
        public static void Print(this SolutionHistoryNavigationEntry entry, bool isDetailed)
        {
            if (entry is null)
                return;
            if (entry.CellValueSet != null)
            {
                if (entry.IsFirst)
                    Console.Write("FIRST. ");
                else if (entry.IsLast)
                    Console.Write("LAST. ");
                Console.WriteLine($"Step {entry.StepId}. A digit '{entry.CellValueSet.Value}' has been placed at {entry.CellValueSet.Position.ToSudokuCoords()} because {entry.Message}. " +
                                  $"It has triggered {entry.CandidateValueRemoved?.Count ?? 0} candidates removal");
                if (entry.SudokuSnapshot != null)
                {
                    if (isDetailed)
                        ExtendedSudokuPrinter.Print(entry.SudokuSnapshot, entry.CandidateValueRemoved, entry.CellValueSet.Position);
                    else
                    {
                        BasicSudokuPrinter.Print(entry.SudokuSnapshot, entry.CellValueSet.Position);
                    }
                }
            }
            //TODO handle other cases print
        }
    }
}
