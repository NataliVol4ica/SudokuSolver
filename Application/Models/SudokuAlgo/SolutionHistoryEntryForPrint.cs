using System.Drawing;

namespace Application.Models.SudokuAlgo
{
    public class SolutionHistoryEntryForPrint : SolutionHistoryEntry
    {
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }

        public SolutionHistoryEntryForPrint(Point pos, int digit, string reason, int id, Sudoku snapshot) : base(pos, digit, reason, id, snapshot){}

        public SolutionHistoryEntryForPrint(SolutionHistoryEntry entry)
            : base(entry.Position, entry.Digit, entry.Reason, entry.Id, entry.SudokuSnapshot) { }
    }
}
