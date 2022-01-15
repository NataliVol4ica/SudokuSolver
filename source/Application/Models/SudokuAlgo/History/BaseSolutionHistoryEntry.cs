using Application.Tools.Enums;

namespace Application.Models.SudokuAlgo.History
{
    public abstract class BaseSolutionHistoryEntry
    {
        public abstract HistoryEntryLevel Level { get; }
        public abstract void Print(bool detailedMode = false);
    }
}
