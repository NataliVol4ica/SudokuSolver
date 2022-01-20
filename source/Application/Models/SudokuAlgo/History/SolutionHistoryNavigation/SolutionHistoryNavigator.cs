using System.Collections.Generic;
using System.Linq;
using Application.Tools.Enums;
using MoreLinq;

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
            var candidateEntries = history.RemoveCandidateEntries
                .Where(e => e.Level != HistoryEntryLevel.SudokuInit)
                .GroupBy(e => e.ContextId);

            _viewEntries =
                 history.SetValueEntries.Where(e => e.Level != HistoryEntryLevel.SudokuInit)
                     .FullJoin(candidateEntries,
                    valueSet => valueSet.ContextId,
                    candidateRm => candidateRm.Key,
                    valueSet => new SolutionHistoryNavigationEntry(valueSet),
                    candidateRm => new SolutionHistoryNavigationEntry(candidateRm.ToList()),
                    (valueSet, candidateRm) => new SolutionHistoryNavigationEntry(valueSet, candidateRm.ToList()))
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
            PreviouslyViewedEntryId = FirstEntryId - 1;
            return GetNextEntry();
        }

        public SolutionHistoryNavigationEntry GetLastEntry()
        {
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
