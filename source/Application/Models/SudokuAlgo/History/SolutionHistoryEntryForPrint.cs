using System.Drawing;
using Application.Tools.Enums;

namespace Application.Models.SudokuAlgo.History
{
    //todo wrap instead of inheriting
    public class SolutionHistoryEntryForPrint : ValueSetSolutionHistoryEntry
    {
        //todo use
        //public int Id { get; set; }
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }

        public SolutionHistoryEntryForPrint(Point pos, int digit, string reason, int id, Sudoku snapshot, HistoryEntryLevel level) : base(pos, digit, reason, id, snapshot, level) {}

        public SolutionHistoryEntryForPrint(ValueSetSolutionHistoryEntry entry)
            : base(entry.Position, entry.Digit, entry.Reason, entry.Id, entry.SudokuSnapshot, entry.Level) { }
    }
}
