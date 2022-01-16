using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Models.SudokuAlgo.History.SolutionHistoryNavigation
{
    public class SolutionHistoryNavigationEntry
    {
        public int StepId { get; set; }
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }

        public string Message { get; set; }
        public Sudoku SudokuSnapshot { get; set; }
        public ValuePosition CellValueSet { get; set; }
        public List<ValuePosition> CandidateValueRemoved { get; set; }
        public DateTime TimeStamp { get; }

        public SolutionHistoryNavigationEntry(ValueSetSolutionHistoryEntry source)
        {
            TimeStamp = source.TimeStamp;
            Message = source.Reason;
            SudokuSnapshot = source.SudokuSnapshot;
            CellValueSet = ToViewEntry(source);
        }

        public SolutionHistoryNavigationEntry(List<CandidateRemovedSolutionHistoryEntry> source)
        {
            //todo message
            //todo validate null or empty list
            Message = "Removing candidates from cells. TODO REASON";
            TimeStamp = source.First().TimeStamp;
            //todo
            SudokuSnapshot = null;
            CandidateValueRemoved = ToViewEntries(source);
        }

        public SolutionHistoryNavigationEntry(ValueSetSolutionHistoryEntry valueSetSource, List<CandidateRemovedSolutionHistoryEntry> candidateRmSource)
        {
            TimeStamp = valueSetSource.TimeStamp;
            Message = valueSetSource.Reason;
            SudokuSnapshot = valueSetSource.SudokuSnapshot;
            CellValueSet = ToViewEntry(valueSetSource);
            CandidateValueRemoved = ToViewEntries(candidateRmSource);
        }

        private ValuePosition ToViewEntry(ValueSetSolutionHistoryEntry source)
        {
            return new ValuePosition { Value = source.Digit, Position = source.Position };
        }

        private List<ValuePosition> ToViewEntries(List<CandidateRemovedSolutionHistoryEntry> source)
        {
            return source
                .Select(e => new ValuePosition
                {
                    Position = e.Position,
                    Value = e.CandidateId
                })
                .ToList();
        }

    }
}