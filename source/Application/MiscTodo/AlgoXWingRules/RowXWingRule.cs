using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Application.Models;
using Application.Tools;
using Application.Tools.Enums;

namespace Application.MiscTodo.AlgoXWingRules
{
    //todo decompose
    public class RowXWingRule : BaseXWingRule
    {
        private List<DigitStatistics> GetRowStatistics(int rowId, Context context)
        {
            var row = context.SudokuUnderSolution.Row(rowId);
            var statistics = InitializeStatistics(9); //todo 9
            for (int columnId = 0; columnId < 9; columnId++)
            {
                var cell = row[columnId];
                if (cell.HasValue)
                {
                    statistics[cell.Value - 1].NumOfOccurencies = 999;
                    continue;
                }
                for (int candidateId = 0; candidateId < 9; candidateId++) //todo 9
                {
                    if (cell.HasCandidate(candidateId) == false)
                        continue;
                    statistics[candidateId].NumOfOccurencies++;
                    statistics[candidateId].Positions.Add(new Point(rowId, columnId));
                }
            }

            return statistics.Select(s => s.NumOfOccurencies == 2 ? s : null).ToList();
        }

        private int ProcessXWingColumns(Context context, int digitToRemove, List<int> columnIds, List<int> rowIdsToRemain, string message)
        {
            var numOfChanges = 0;

            for (int columnListId = 0; columnListId < columnIds.Count; columnListId++)
            {
                var columnId = columnIds[columnListId];
                for (int rowId = 0; rowId < 9; rowId++) //todo 9
                {
                    if (rowIdsToRemain.Contains(rowId))
                        continue;
                    if (context.SudokuUnderSolution[rowId, columnId].RemoveCandidate(digitToRemove, context, new Point(rowId, columnId), message))
                        numOfChanges++;
                }
            }

            return numOfChanges;
        }

        protected bool AreInSquare(List<Point> pointSet1, List<Point> pointSet2)
        {
            return pointSet1.Select(s => s.Y).SequenceEqual(pointSet2.Select(s => s.Y));
        }

        public override int ApplyToAll(Context context)
        {
            context.HistoryEntryLevel = HistoryEntryLevel.CandidateSet;

            var numOfChanges = 0;
            var allStatistics = new List<List<DigitStatistics>>();
            for (int rowId = 0; rowId < 9; rowId++) //todo 9
            {
                allStatistics.Add(GetRowStatistics(rowId, context));
            }

            for (int candidateId = 0; candidateId < 9; candidateId++)
            {
                for (int rowId1 = 0; rowId1 < 9; rowId1++) //todo 9
                {
                    if (allStatistics[rowId1][candidateId] is null)
                        continue;
                    var row1 = allStatistics[rowId1][candidateId];
                    for (int rowId2 = rowId1 + 1; rowId2 < 9; rowId2++) //todo 9
                    {
                        if (allStatistics[rowId2][candidateId] is null)
                            continue;
                        var row2 = allStatistics[rowId2][candidateId];
                        if (!AreInSquare(row1.Positions, row2.Positions))
                            continue;
                        context.InitNewContextId();
                        var message = $"Found Horizontal XWing. Digit {candidateId + 1}. " +
                                          $"Positions {row1.Positions[0].ToSudokuCoords()} and {row1.Positions[1].ToSudokuCoords()}, " +
                                          $"Positions {row2.Positions[0].ToSudokuCoords()} and {row2.Positions[1].ToSudokuCoords()}";
                        var numOfChangedIterationCells = ProcessXWingColumns(
                            context,
                            candidateId,
                            new List<int> { row1.Positions[0].Y, row1.Positions[1].Y },
                            new List<int> { rowId1, rowId2 },
                            message);
                        if (numOfChangedIterationCells > 0)
                        {
                            context.History.AddHighlightCandidateEntry(candidateId + 1, context, row1.Positions[0], message);
                            context.History.AddHighlightCandidateEntry(candidateId + 1, context, row1.Positions[1], message);
                            context.History.AddHighlightCandidateEntry(candidateId + 1, context, row2.Positions[0], message);
                            context.History.AddHighlightCandidateEntry(candidateId + 1, context, row2.Positions[1], message);
                        }
                        numOfChanges += numOfChangedIterationCells;
                    }
                }
            }

            return numOfChanges;
        }
    }
}
