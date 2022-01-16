using System;
using Application.Tools.Enums;

namespace Application.Models.SudokuAlgo.History
{
    public abstract class BaseSolutionHistoryEntry
    {
        public Guid ContextId { get; protected set; }
        public HistoryEntryLevel Level { get; protected set; }
        public DateTime TimeStamp { get; } = DateTime.Now;
    }
}
