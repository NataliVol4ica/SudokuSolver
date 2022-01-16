using System.Collections.Generic;
using System.Drawing;

namespace Application.Models.SudokuAlgo.History
{
    public class SolutionHistory
    {
        public List<ValueSetSolutionHistoryEntry> SetValueEntries { get; } = new List<ValueSetSolutionHistoryEntry>(81);

        public List<CandidateRemovedSolutionHistoryEntry> RemoveCandidateEntries { get; } =
            new List<CandidateRemovedSolutionHistoryEntry>();

        public void AddSetValueEntry(int digit, Context context, string reason)
        {
            SetValueEntries.Add(new ValueSetSolutionHistoryEntry(
                context.CellUnderAction,
                digit,
                reason,
                context.HistoryContextId,
                context.SudokuUnderSolution));
        }

        public void AddRemoveCandidateEntry(int digit, Context context, Point position)
        {
            RemoveCandidateEntries.Add(new CandidateRemovedSolutionHistoryEntry(
                position,
                digit,
                context.HistoryContextId));
        }
    }
}
