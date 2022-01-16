using System;
using System.Drawing;

namespace Application.Models.SudokuAlgo.History
{
    public class ValueSetSolutionHistoryEntry : BaseSolutionHistoryEntry
    {
        //public override HistoryEntryLevel Level { get; } //todo
        public int Digit { get; }
        public Point Position { get; }
        public string Reason { get; }
        public Sudoku SudokuSnapshot { get; }

        public ValueSetSolutionHistoryEntry(Point pos, int digit, string reason, Guid contextId, Sudoku snapshot/*, HistoryEntryLevel level*/)
        {
            Digit = digit;
            Position = pos;
            Reason = reason;
            ContextId = contextId;
            //Level = level;
            SudokuSnapshot = snapshot.Clone();
        }
    }
}
