using System;
using System.Collections.Generic;
using System.Linq;
using Application.Tools;

namespace Application.Models.SudokuAlgo.History.SolutionHistoryNavigation
{
    public class SolutionHistoryNavigationEntry
    {
        public int StepId { get; set; }
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }

        private readonly string _message;
        public string Message => $"{ProcessStepId(StepId)} {_message}";

        private string ProcessStepId(int stepId)
        {
            string result = $"Step {stepId}.";
            if (IsFirst)
                return "FIRST. " + result;
            if (IsLast)
                return "LAST. " + result;
            return result;
        }

        public Sudoku SudokuSnapshot { get; set; }
        public ValuePosition CellValueSet { get; set; }
        public List<ValuePosition> CandidateValueRemoved { get; set; }
        public DateTime TimeStamp { get; }

        public SolutionHistoryNavigationEntry(ValueSetSolutionHistoryEntry source)
        {
            TimeStamp = source.TimeStamp;
            SudokuSnapshot = source.SudokuSnapshot;
            CellValueSet = ToViewEntry(source);
            _message = $"A digit '{source.Digit}' has been placed at {source.Position.ToSudokuCoords()} because {source.Reason}";
        }

        public SolutionHistoryNavigationEntry(List<CandidateRemovedSolutionHistoryEntry> source)
        {
            //todo validate null or empty list
            TimeStamp = source.First().TimeStamp;
            //todo
            SudokuSnapshot = source[^1].SudokuSnapshot;
            CandidateValueRemoved = ToViewEntries(source);
            _message = source.FirstOrDefault()?.Message;
        }

        public SolutionHistoryNavigationEntry(ValueSetSolutionHistoryEntry valueSetSource, List<CandidateRemovedSolutionHistoryEntry> candidateRmSource)
        {
            TimeStamp = valueSetSource.TimeStamp;
            SudokuSnapshot = candidateRmSource[^1].SudokuSnapshot ?? valueSetSource.SudokuSnapshot;
            CellValueSet = ToViewEntry(valueSetSource);
            CandidateValueRemoved = ToViewEntries(candidateRmSource);
            _message = $"A digit '{valueSetSource.Digit}' has been placed at {valueSetSource.Position.ToSudokuCoords()} because {valueSetSource.Reason}." +
                       $" It has triggered {candidateRmSource?.Count ?? 0} candidates removal";
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