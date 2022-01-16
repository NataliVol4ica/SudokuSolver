using System;
using System.Drawing;

namespace Application.Models.SudokuAlgo.History
{
    public class CandidateRemovedSolutionHistoryEntry : BaseSolutionHistoryEntry
    {
        //public override HistoryEntryLevel Level { get; } //todo
        public int CandidateId { get; }
        public Point Position { get; }

        public CandidateRemovedSolutionHistoryEntry(Point pos, int candidateId, Guid contextId)
        {
            Position = pos;
            CandidateId = candidateId;
            ContextId = contextId;
        }

    }
}
