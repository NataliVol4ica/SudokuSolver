using System.Collections.Generic;
using System.Drawing;

namespace Application.Models.SudokuAlgo.History
{
    public class SolutionHistory
    {
        public List<ValueSetSolutionHistoryEntry> SetValueEntries { get; } = new(81);
        public List<CandidateRemovedSolutionHistoryEntry> RemoveCandidateEntries { get; } = new();
        public List<CandidateHighlightedSolutionHistoryEntry> HighlightedCandidateEntries { get; } = new();

        public void AddSetValueEntry(int digit, Context context, string reason)
        {
            SetValueEntries.Add(new ValueSetSolutionHistoryEntry(
                context.CellUnderAction,
                digit,
                reason,
                context.SudokuUnderSolution,
                context.HistoryContextId,
                context.HistoryEntryLevel));
        }

        public void AddRemoveCandidateEntry(int digit, Context context, Point position, string message)
        {
            RemoveCandidateEntries.Add(new CandidateRemovedSolutionHistoryEntry(
                position,
                digit,
                context.SudokuUnderSolution,
                context.HistoryContextId,
                context.HistoryEntryLevel,
                message));
        }

        public void AddHighlightCandidateEntry(int digit, Context context, Point position, string message)
        {
            HighlightedCandidateEntries.Add(new CandidateHighlightedSolutionHistoryEntry(
                position,
                digit,
                context.SudokuUnderSolution,
                context.HistoryContextId,
                context.HistoryEntryLevel,
                message));
        }
    }
}
