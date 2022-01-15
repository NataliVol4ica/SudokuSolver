using System.Collections.Generic;

namespace Application.Models.SudokuAlgo.History
{
    public class SolutionHistory
    {
        public List<ValueSetSolutionHistoryEntry> Entries { get;} = new List<ValueSetSolutionHistoryEntry>(81); //todo 9

        private int LastEntryId => Entries.Count - 1;
        private int FirstEntryId => 0;
        private int PreviouslyViewedEntryId { get; set; } = -1;

        public void AddSetValueEntry(int digit, Context context, string reason)
        {
            Entries.Add(new ValueSetSolutionHistoryEntry(
                context.CellUnderAction,
                digit,
                reason, 
                Entries.Count + 1, 
                context.SudokuUnderSolution,
                context.HistoryEntryLevel));
        }
        //public void AddCandidateEntry(int digit, Context context)
        //{
        //    Entries.Add(new SolutionHistoryEntry(context.CellUnderAction, digit, reason, Entries.Count + 1, context.SudokuUnderSolution));
        //}

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

        private SolutionHistoryEntryForPrint ApplyFirstLastChecks(ValueSetSolutionHistoryEntry entry)
        {
            if (PreviouslyViewedEntryId == LastEntryId)
                return new SolutionHistoryEntryForPrint(entry){IsLast = true};
            if (PreviouslyViewedEntryId == FirstEntryId)
                return new SolutionHistoryEntryForPrint(entry) { IsFirst = true };
            return new SolutionHistoryEntryForPrint(entry);
        }
    }
}
