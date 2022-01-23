using System.Collections.Generic;
using System.Drawing;
using Application.MiscTodo.AlgoSharedTools;
using Application.Models;
using Application.Tools;
using Application.Tools.Enums;

namespace Application.MiscTodo.AlgoPointingDigitsRules
{
    public class PointingDigitsRule : IRule
    {
        //todo decompose
        private int ApplyToBlock(int blockId, Context context)
        {
            var numOfChanges = 0;
            var block = context.SudokuUnderSolution.PlainBlock(blockId);
            var statistics = InitializeStatistics(9); //todo 9

            for (int cellId = 0; cellId < 9; cellId++) //todo 9
            {
                var cell = block[cellId];
                if (cell.HasValue)
                {
                    statistics[cell.Value - 1].NumOfOccurencies = 999;
                    continue;
                }

                var candidates = cell.GetCandidates();
                foreach (var candidate in candidates)
                {
                    statistics[candidate].NumOfOccurencies++;
                    statistics[candidate].Positions.Add(new Point(blockId / 3 * 3 + cellId / 3, blockId % 3 * 3 + cellId % 3));
                }
            }

            for (int candidateId = 0; candidateId < 9; candidateId++) //todo 9
            {
                var stat = statistics[candidateId];
                numOfChanges += ProcessStatistics(stat, candidateId, context);
            }

            return numOfChanges;
        }

        private int ProcessStatistics(DigitStatistics statistics, int candidateId, Context context)
        {
            int numOfChanges = 0;

            string message = string.Empty;

            if (statistics.NumOfOccurencies == 2)
            {
                context.InitNewContextId();
                if (Utilities.IsHorizontalPair(statistics.Positions[0], statistics.Positions[1]))
                {
                    int rowId = statistics.Positions[0].X;
                    message = PairMessage(statistics.Positions[0], statistics.Positions[1], candidateId, "horizontal");
                    for (int columnId = 0; columnId < 9; columnId++) //todo 9
                    {
                        if (columnId == statistics.Positions[0].Y || columnId == statistics.Positions[1].Y)
                            continue;
                        if (context.SudokuUnderSolution[rowId, columnId].RemoveCandidate(candidateId, context,
                                new Point(rowId, columnId), message))
                            numOfChanges++;
                    }
                }
                else if (Utilities.IsVerticalPair(statistics.Positions[0], statistics.Positions[1]))
                {
                    int columnId = statistics.Positions[0].Y;
                    message = PairMessage(statistics.Positions[0], statistics.Positions[1], candidateId, "vertical");
                    for (int rowId = 0; rowId < 9; rowId++) //todo 9
                    {
                        if (rowId == statistics.Positions[0].X || rowId == statistics.Positions[1].X)
                            continue;
                        if (context.SudokuUnderSolution[rowId, columnId].RemoveCandidate(candidateId, context,
                                new Point(rowId, columnId), message))
                            numOfChanges++;
                    }
                }

                if (numOfChanges > 0)
                {
                    context.History.AddHighlightCandidateEntry(candidateId + 1, context, statistics.Positions[0], message);
                    context.History.AddHighlightCandidateEntry(candidateId + 1, context, statistics.Positions[1], message);
                }
            }
            else if (statistics.NumOfOccurencies == 3)
            {
                if (Utilities.IsHorizontalTriple(statistics.Positions[0], statistics.Positions[1], statistics.Positions[2]))
                {
                    int rowId = statistics.Positions[0].X;
                    message = TripleMessage(statistics.Positions[0], statistics.Positions[1], statistics.Positions[2], candidateId, "horizontal");
                    for (int columnId = 0; columnId < 9; columnId++) //todo 9
                    {
                        if (columnId == statistics.Positions[0].Y || columnId == statistics.Positions[1].Y || columnId == statistics.Positions[2].Y)
                            continue;
                        if (context.SudokuUnderSolution[rowId, columnId].RemoveCandidate(candidateId, context,
                                new Point(rowId, columnId), message))
                            numOfChanges++;
                    }
                }
                else if (Utilities.IsVerticalTriple(statistics.Positions[0], statistics.Positions[1], statistics.Positions[2]))
                {
                    int columnId = statistics.Positions[0].Y;
                    message = TripleMessage(statistics.Positions[0], statistics.Positions[1], statistics.Positions[2], candidateId, "vertical");
                    for (int rowId = 0; rowId < 9; rowId++) //todo 9
                    {
                        if (rowId == statistics.Positions[0].X || rowId == statistics.Positions[1].X || rowId == statistics.Positions[2].X)
                            continue;
                        if (context.SudokuUnderSolution[rowId, columnId].RemoveCandidate(candidateId, context,
                                new Point(rowId, columnId), message))
                            numOfChanges++;
                    }
                }

                if (numOfChanges > 0)
                {
                    context.History.AddHighlightCandidateEntry(candidateId + 1, context, statistics.Positions[0], message);
                    context.History.AddHighlightCandidateEntry(candidateId + 1, context, statistics.Positions[1], message);
                    context.History.AddHighlightCandidateEntry(candidateId + 1, context, statistics.Positions[2], message);
                }
            }

            return numOfChanges;
        }

        public int Apply(Context context)
        {
            context.HistoryEntryLevel = HistoryEntryLevel.CandidateSet;
            var numOfChanges = 0;

            for (int blockId = 0; blockId < 9; blockId++) //todo 9
            {
                numOfChanges += ApplyToBlock(blockId, context);
            }

            return numOfChanges;
        }

        private List<DigitStatistics> InitializeStatistics(int size)
        {
            var statistics = new List<DigitStatistics>(size);
            for (int i = 0; i < size; i++)
                statistics.Add(new DigitStatistics());
            return statistics;
        }

        private string PairMessage(Point p1, Point p2, int candidateId, string direction) =>
            $"Found {direction} Pointing Pair {p1.ToSudokuCoords()} {p2.ToSudokuCoords()} with digit {candidateId + 1}. " + DirectionMessage(direction);

        private string TripleMessage(Point p1, Point p2, Point p3, int candidateId, string direction) =>
            $"Found {direction} Pointing Triple {p1.ToSudokuCoords()} {p2.ToSudokuCoords()} {p3.ToSudokuCoords()} with digit {candidateId + 1}. " + DirectionMessage(direction);

        private object DirectionMessage(string direction)
        {
            var rowOrColumn = direction.Equals("vertical") ? "column" : "row";
            return $"Removing other candidates in {rowOrColumn}";
        }

    }
}
