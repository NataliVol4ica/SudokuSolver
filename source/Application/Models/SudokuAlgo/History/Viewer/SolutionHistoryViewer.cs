﻿using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace Application.Models.SudokuAlgo.History.Viewer
{
    public class SolutionHistoryViewer
    {
        private readonly SolutionHistory _history;

        private List<SolutionHistoryViewEntry> _viewEntries;

        private int LastEntryId => _viewEntries.Count - 1;
        private int FirstEntryId => 0;
        private int PreviouslyViewedEntryId { get; set; } = -1;

        private SolutionHistoryViewer(SolutionHistory history)
        {
            _history = history;
        }

        public static SolutionHistoryViewer Create(SolutionHistory history)
        {
            var result = new SolutionHistoryViewer(history);
            result.Initialize();
            return result;
        }

        public void Initialize()
        {
            var candidateEntries = _history.RemoveCandidateEntries.GroupBy(e => e.ContextId);

            _viewEntries =
                _history.SetValueEntries.FullJoin(candidateEntries,
                    valueSet => valueSet.ContextId,
                    candidateRm => candidateRm.Key,
                    valueSet => new SolutionHistoryViewEntry(valueSet),
                    candidateRm => new SolutionHistoryViewEntry(candidateRm.ToList()),
                    (valueSet, candidateRm) => new SolutionHistoryViewEntry(valueSet, candidateRm.ToList()))
                    .OrderBy(e=>e.TimeStamp) //todo verify if its needed
                    .ToList();
            _viewEntries[0].IsFirst = true;
            _viewEntries[^1].IsLast = true;
            for (int i = 0; i < _viewEntries.Count; i++)
            {
                _viewEntries[i].StepId = i;
            }
        }

        public SolutionHistoryViewEntry GetPreviousEntry()
        {
            if (PreviouslyViewedEntryId <= FirstEntryId)
                return null;
            var entry = _viewEntries[--PreviouslyViewedEntryId];
            return entry;
        }

        public SolutionHistoryViewEntry GetNextEntry()
        {
            if (PreviouslyViewedEntryId >= LastEntryId)
                return null;
            var entry = _viewEntries[++PreviouslyViewedEntryId];
            return entry;
        }

        public SolutionHistoryViewEntry GetFirstEntry()
        {
            PreviouslyViewedEntryId = FirstEntryId - 1;
            return GetNextEntry();
        }

        public SolutionHistoryViewEntry GetLastEntry()
        {
            PreviouslyViewedEntryId = LastEntryId + 1;
            return GetPreviousEntry();
        }

        public SolutionHistoryViewEntry JumpToEntry(int delta)
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
