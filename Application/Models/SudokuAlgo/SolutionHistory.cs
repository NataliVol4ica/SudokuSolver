using System.Collections.Generic;
using System.Drawing;

namespace Application.Models.SudokuAlgo
{
    public class SolutionHistory
    {
        public List<SolutionHistoryEntry> Entries { get;} = new List<SolutionHistoryEntry>(81); //todo 9

        private int LastEntryId => Entries.Count - 1;
        private int FirstEntryId => 0;
        private int PreviouslyViewedEntryId { get; set; } = -1;

        public void AddEntry(Sudoku snapshot, Point pos, int digit, string reason)
        {
            Entries.Add(new SolutionHistoryEntry(pos, digit, reason, Entries.Count + 1, snapshot));
        }

        public SolutionHistoryEntryForPrint GetPreviousEntry()
        {
            if (PreviouslyViewedEntryId <= FirstEntryId)
                return null;
            var entry = Entries[--PreviouslyViewedEntryId];
            return ApplyFirstLastChecks(entry);
        }

        public SolutionHistoryEntryForPrint GetNextEntry()
        {
            if (PreviouslyViewedEntryId >= LastEntryId)
                return null;
            var entry = Entries[++PreviouslyViewedEntryId];
            return ApplyFirstLastChecks(entry);
        }

        public SolutionHistoryEntryForPrint GetFirstEntry()
        {
            PreviouslyViewedEntryId = FirstEntryId - 1;
            return GetNextEntry();
        }

        public SolutionHistoryEntryForPrint GetLastEntry()
        {
            PreviouslyViewedEntryId = LastEntryId + 1;
            return GetPreviousEntry();
        }

        public SolutionHistoryEntryForPrint JumpToEntry(int delta)
        {
            var targetId = PreviouslyViewedEntryId + delta;
            if (targetId < FirstEntryId)
                targetId = FirstEntryId;
            if (targetId > LastEntryId)
                targetId = LastEntryId;
            if (targetId == PreviouslyViewedEntryId)
                return null;
            var entry = Entries[targetId];
            PreviouslyViewedEntryId = targetId;
            return ApplyFirstLastChecks(entry);
        }

        private SolutionHistoryEntryForPrint ApplyFirstLastChecks(SolutionHistoryEntry entry)
        {
            if (PreviouslyViewedEntryId == LastEntryId)
                return new SolutionHistoryEntryForPrint(entry){IsLast = true};
            if (PreviouslyViewedEntryId == FirstEntryId)
                return new SolutionHistoryEntryForPrint(entry) { IsFirst = true };
            return new SolutionHistoryEntryForPrint(entry);
        }
    }
}
