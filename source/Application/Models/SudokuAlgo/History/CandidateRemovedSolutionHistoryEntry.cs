using System;
using System.Drawing;
using Application.Tools.Enums;

namespace Application.Models.SudokuAlgo.History
{
    public class CandidateRemovedSolutionHistoryEntry : BaseSolutionHistoryEntry
    {
        public int CandidateId { get; }
        public Point Position { get; }

        public CandidateRemovedSolutionHistoryEntry(Point pos, int candidateId, Guid contextId, HistoryEntryLevel level)
        {
            Position = pos;
            CandidateId = candidateId;
            ContextId = contextId;
            Level = level;
        }

    }
}
