using System;
using System.Collections.Generic;
using System.Linq;
using Application.Tools.Enums;

namespace Application.Models.SudokuAlgo.History.SolutionHistoryNavigation
{
    public class SolutionHistoryNavigator
    {

        private List<SolutionHistoryNavigationEntry> _viewEntries;

        private int LastEntryId => _viewEntries.Count - 1;
        private int FirstEntryId => 0;
        private int PreviouslyViewedEntryId { get; set; } = -1;

        private SolutionHistoryNavigator()
        {
        }

        public static SolutionHistoryNavigator Create(SolutionHistory history)
        {
            var result = new SolutionHistoryNavigator();
            result.Initialize(history);
            return result;
        }

        public void Initialize(SolutionHistory history)
        {
            var groupedEntries = new Dictionary<Guid, SolutionHistoryNavigationEntry>();

            //todo refactor method
            //todo sudokuInitEntries

            var setValueEntries = history.SetValueEntries
                .Where(e => e.Level != HistoryEntryLevel.SudokuInit);
            foreach (var setValueHistoryEntry in setValueEntries)
            {
                groupedEntries.Add(setValueHistoryEntry.ContextId, new SolutionHistoryNavigationEntry(setValueHistoryEntry));
            }

            var candidateRemovedEntries = history.RemoveCandidateEntries
                .Where(e => e.Level != HistoryEntryLevel.SudokuInit)
                .GroupBy(e => e.ContextId)
                .ToList();

            foreach (var candidateRemovedHistoryEntry in candidateRemovedEntries)
            {
                if (groupedEntries.TryGetValue(candidateRemovedHistoryEntry.Key, out var groupedEntry))
                {
                    groupedEntry.AddRemovedCandidates(candidateRemovedHistoryEntry.ToList());
                }
                else
                {
                    groupedEntries.Add(candidateRemovedHistoryEntry.Key, new SolutionHistoryNavigationEntry(candidateRemovedHistoryEntry.ToList()));
                }
            }
            var candidateHighlightedEntries = history.HighlightedCandidateEntries
                .Where(e => e.Level != HistoryEntryLevel.SudokuInit)
                .GroupBy(e => e.ContextId)
                .ToList();

            foreach (var candidateHighlightedHistoryEntry in candidateHighlightedEntries)
            {
                if (groupedEntries.TryGetValue(candidateHighlightedHistoryEntry.Key, out var groupedEntry))
                {
                    groupedEntry.AddHighlightedCandidates(candidateHighlightedHistoryEntry.ToList());
                }
                else
                {
                    groupedEntries.Add(candidateHighlightedHistoryEntry.Key, new SolutionHistoryNavigationEntry(candidateHighlightedHistoryEntry.ToList()));
                }
            }

            _viewEntries = groupedEntries.Keys
                .Select(key => groupedEntries[key])
                .OrderBy(e => e.TimeStamp) //todo verify if its needed
                .ToList();
            _viewEntries[0].IsFirst = true;
            _viewEntries[^1].IsLast = true;
            for (int i = 0; i < _viewEntries.Count; i++)
            {
                _viewEntries[i].StepId = i;
            }
        }

        public SolutionHistoryNavigationEntry GetPreviousEntry()
        {
            if (PreviouslyViewedEntryId <= FirstEntryId)
                return null;
            var entry = _viewEntries[--PreviouslyViewedEntryId];
            return entry;
        }

        public SolutionHistoryNavigationEntry GetNextEntry()
        {
            if (PreviouslyViewedEntryId >= LastEntryId)
                return null;
            var entry = _viewEntries[++PreviouslyViewedEntryId];
            return entry;
        }

        public SolutionHistoryNavigationEntry GetFirstEntry()
        {
            //todo dont blink
            PreviouslyViewedEntryId = FirstEntryId - 1;
            return GetNextEntry();
        }

        public SolutionHistoryNavigationEntry GetLastEntry()
        {
            //todo dont blink
            PreviouslyViewedEntryId = LastEntryId + 1;
            return GetPreviousEntry();
        }

        public SolutionHistoryNavigationEntry JumpToEntry(int delta)
        {
            var targetId = PreviouslyViewedEntryId + delta;
            if (targetId < FirstEntryId)
                targetId = FirstEntryId;
            if (targetId > LastEntryId)
                targetId = LastEntryId;
            if (targetId == PreviouslyViewedEntryId)
                return null;
            var entry = _viewEntries[targetId];
            PreviouslyViewedEntryId = targetId;
            return entry;
        }
    }
}
