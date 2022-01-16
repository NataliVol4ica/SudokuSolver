using System;
using System.Drawing;
using Application.Tools.Enums;

namespace Application.Models.SudokuAlgo.History
{
    public class ValueSetSolutionHistoryEntry : BaseSolutionHistoryEntry
    {
        public int Digit { get; protected set; }
        public Point Position { get; protected set; }
        public string Reason { get; protected set; }
        public Sudoku SudokuSnapshot { get; protected set; }

        public ValueSetSolutionHistoryEntry(Point pos, int digit, string reason, Guid contextId, Sudoku snapshot, HistoryEntryLevel level)
        {
            Digit = digit;
            Position = pos;
            Reason = reason;
            ContextId = contextId;
            Level = level;
            SudokuSnapshot = snapshot.Clone();
        }
    }
}
