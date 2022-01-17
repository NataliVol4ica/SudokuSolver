using System;
using System.Drawing;
using Application.Tools.Enums;

namespace Application.Models.SudokuAlgo.History
{
    public class CandidateRemovedSolutionHistoryEntry : BaseSolutionHistoryEntry
    {
        public int CandidateId { get; }
        public string Message { get; }
        public Point Position { get; }

        public Sudoku SudokuSnapshot { get; }

        public CandidateRemovedSolutionHistoryEntry(Point pos, int candidateId, Sudoku snapshot, Guid contextId, HistoryEntryLevel level, string message)
        {
            Position = pos;
            CandidateId = candidateId;
            Message = message;
            ContextId = contextId;
            Level = level;
            SudokuSnapshot = snapshot.Clone();
        }

    }
}
