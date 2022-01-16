using System;
using System.Collections.Generic;
using System.Linq;
using Application.MiscTodo.UserInput;
using Application.Tools;

namespace Application.Models.SudokuAlgo.History.Viewer
{
    public class SolutionHistoryViewEntry
    {
        public int StepId { get; set; }
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }

        public string Message { get; set; }
        public Sudoku SudokuSnapshot { get; set; }
        public ValuePositionsViewEntry CellValueSet { get; set; }
        public List<ValuePositionsViewEntry> CandidateValueRemoved { get; set; }
        public DateTime TimeStamp { get; }

        public SolutionHistoryViewEntry(ValueSetSolutionHistoryEntry source)
        {
            TimeStamp = source.TimeStamp;
            Message = source.Reason;
            SudokuSnapshot = source.SudokuSnapshot;
            CellValueSet = ToViewEntry(source);
        }

        public SolutionHistoryViewEntry(List<CandidateRemovedSolutionHistoryEntry> source)
        {
            //todo message
            //todo validate null or empty list
            Message = "Removing candidates from cells. TODO REASON";
            TimeStamp = source.First().TimeStamp;
            //todo
            SudokuSnapshot = null;
            CandidateValueRemoved = ToViewEntries(source);
        }

        public SolutionHistoryViewEntry(ValueSetSolutionHistoryEntry valueSetSource, List<CandidateRemovedSolutionHistoryEntry> candidateRmSource)
        {
            TimeStamp = valueSetSource.TimeStamp;
            Message = valueSetSource.Reason;
            SudokuSnapshot = valueSetSource.SudokuSnapshot;
            CellValueSet = ToViewEntry(valueSetSource);
            CandidateValueRemoved = ToViewEntries(candidateRmSource);
        }

        private ValuePositionsViewEntry ToViewEntry(ValueSetSolutionHistoryEntry source)
        {
            return new ValuePositionsViewEntry { Value = source.Digit, Position = source.Position };
        }

        private List<ValuePositionsViewEntry> ToViewEntries(List<CandidateRemovedSolutionHistoryEntry> source)
        {
            return source
                .Select(e => new ValuePositionsViewEntry
                {
                    Position = e.Position,
                    Value = e.CandidateId
                })
                .ToList();
        }

        public void Print(bool isDetailed)
        {
            if (CellValueSet != null)
            {
                if (IsFirst)
                    Console.Write("FIRST. ");
                else if (IsLast)
                    Console.Write("LAST. ");
                Console.WriteLine($"Step {StepId}. A digit '{CellValueSet.Value}' has been placed at {CellValueSet.Position.ToSudokuCoords()} because {Message}. " +
                                  $"It has triggered {CandidateValueRemoved.Count} candidates removal");
                if (SudokuSnapshot != null)
                {
                    if (isDetailed)
                        ExtendedSudokuPrinter.Print(SudokuSnapshot, CandidateValueRemoved, CellValueSet.Position);
                    else
                    {
                        BasicSudokuPrinter.Print(SudokuSnapshot, CellValueSet.Position);
                    }
                }
            }
            //TODO handle other cases print
        }
    }
}