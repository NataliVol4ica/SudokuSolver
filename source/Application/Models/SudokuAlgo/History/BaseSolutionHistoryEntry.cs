using System;

namespace Application.Models.SudokuAlgo.History
{
    public abstract class BaseSolutionHistoryEntry
    {
        public Guid ContextId { get; set; }
        //public abstract HistoryEntryLevel Level { get; }
        public DateTime TimeStamp { get; } = DateTime.Now;
    }
}
