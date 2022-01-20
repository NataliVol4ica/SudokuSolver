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
            string result = $"Step {stepId + 1}.";
            if (IsFirst)
                return "FIRST. " + result;
            if (IsLast)
                return "FINAL. " + result;
            return result;
        }

        public Sudoku SudokuSnapshot { get; set; }
        public ValuePosition CellValueSet { get; set; }
        public List<ValuePosition> RemovedCandidates { get; set; }
        public List<ValuePosition> HighlightedCandidates { get; set; }
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
            RemovedCandidates = ToViewEntries(source);
            _message = source.FirstOrDefault()?.Message;
        }

        public SolutionHistoryNavigationEntry(List<CandidateHighlightedSolutionHistoryEntry> source)
        {
            //todo validate null or empty list
            TimeStamp = source.First().TimeStamp;
            //todo
            SudokuSnapshot = source[^1].SudokuSnapshot;
            HighlightedCandidates = ToViewEntries(source);
            _message = source.FirstOrDefault()?.Message;
        }

        private static List<ValuePosition> ToViewEntries<T>(List<T> source) where T: BaseCandidateHistoryEntry
        {
            return source
                .Select(e => new ValuePosition
                {
                    Position = e.Position,
                    Value = e.CandidateId
                })
                .ToList();
        }

        private ValuePosition ToViewEntry(ValueSetSolutionHistoryEntry source)
        {
            return new ValuePosition { Value = source.Digit, Position = source.Position };
        }

        //removed candidates snapshot has high prio
        public void AddRemovedCandidates(List<CandidateRemovedSolutionHistoryEntry> source)
        {
            SudokuSnapshot = source[^1].SudokuSnapshot ?? SudokuSnapshot;
            RemovedCandidates = ToViewEntries(source);
        }

        //highlighted candidate snapshot has low prio
        public void AddHighlightedCandidates(List<CandidateHighlightedSolutionHistoryEntry> source)
        {
            SudokuSnapshot ??= source[^1].SudokuSnapshot;
            HighlightedCandidates = ToViewEntries(source);
        }
    }
}