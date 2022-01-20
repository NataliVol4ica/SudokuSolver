using System.Drawing;

namespace Application.Models.SudokuAlgo.History
{
    public abstract class BaseCandidateHistoryEntry : BaseSolutionHistoryEntry
    {
        public int CandidateId { get; protected set; }
        public Point Position { get; protected set; }
    }
}
