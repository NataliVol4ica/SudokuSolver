using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Application.Models;
using Application.Tools;
using Application.Tools.Enums;

namespace Application.MiscTodo.AlgoXWingRules
{
    //todo decompose
    public class ColumnXWingRule : BaseXWingRule
    {
        private List<DigitStatistics> GetColumnStatistics(int columnId, Context context)
        {
            var column = context.SudokuUnderSolution.Column(columnId);
            var statistics = InitializeStatistics(9); //todo 9
            for (int rowId = 0; rowId < 9; rowId++)
            {
                var cell = column[rowId];
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

        private int ProcessXWingRows(Context context, int digitToRemove, List<int> rowIds, List<int> columnIdsToRemain, string message)
        {
            var numOfChanges = 0;

            for (int rowListId = 0; rowListId < rowIds.Count; rowListId++)
            {
                var rowId = rowIds[rowListId];
                for (int columnId = 0; columnId < 9; columnId++) //todo 9
                {
                    if (columnIdsToRemain.Contains(columnId))
                        continue;
                    if (context.SudokuUnderSolution[rowId, columnId].RemoveCandidate(digitToRemove + 1, context, new Point(rowId, columnId), message))
                        numOfChanges++;
                }
            }
            return numOfChanges;
        }

        protected bool AreInSquare(List<Point> pointSet1, List<Point> pointSet2)
        {
            return pointSet1.Select(s => s.X).SequenceEqual(pointSet2.Select(s => s.X));
        }

        public override int ApplyToAll(Context context)
        {
            context.HistoryEntryLevel = HistoryEntryLevel.CandidateSet;

            var numOfChanges = 0;
            var allStatistics = new List<List<DigitStatistics>>();
            for (int columnId = 0; columnId < 9; columnId++) //todo 9
            {
                allStatistics.Add(GetColumnStatistics(columnId, context));
            }

            for (int digit = 0; digit < 9; digit++)
            {
                for (int columnId1 = 0; columnId1 < 9; columnId1++) //todo 9
                {
                    if (allStatistics[columnId1][digit] is null)
                        continue;
                    var column1 = allStatistics[columnId1][digit];
                    for (int columnId2 = columnId1 + 1; columnId2 < 9; columnId2++) //todo 9
                    {
                        if (allStatistics[columnId2][digit] is null)
                            continue;
                        var column2 = allStatistics[columnId2][digit];
                        if (!AreInSquare(column1.Positions, column2.Positions))
                            continue;
                        context.InitNewContextId();
                        var message = $"Found Vertical XWing. Digit {digit + 1}. " +
                                          $"Positions {column1.Positions[0].ToSudokuCoords()} and {column1.Positions[1].ToSudokuCoords()}, " +
                                          $"Positions {column2.Positions[0].ToSudokuCoords()} and {column2.Positions[1].ToSudokuCoords()}";
                        var numOfChangedIterationCells = ProcessXWingRows(
                            context,
                            digit,
                            new List<int> { column1.Positions[0].X, column1.Positions[1].X },
                            new List<int> { columnId1, columnId2 },
                            message);
                        if (numOfChangedIterationCells > 0)
                        {
                            context.History.AddHighlightCandidateEntry(digit + 1, context, column1.Positions[0], message);
                            context.History.AddHighlightCandidateEntry(digit + 1, context, column1.Positions[1], message);
                            context.History.AddHighlightCandidateEntry(digit + 1, context, column2.Positions[0], message);
                            context.History.AddHighlightCandidateEntry(digit + 1, context, column2.Positions[1], message);
                        }
                        numOfChanges += numOfChangedIterationCells;
                    }
                }
            }

            return numOfChanges;
        }
    }
}
