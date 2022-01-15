using System;
using System.Drawing;
using Application.MiscTodo.UserInput;
using Application.Tools;
using Application.Tools.Enums;

namespace Application.Models.SudokuAlgo.History
{
    public class ValueSetSolutionHistoryEntry : BaseSolutionHistoryEntry
    {
        public int Id { get; } //todo rm
        public override HistoryEntryLevel Level { get; } //todo
        public int Digit { get; }
        public Point Position { get; }
        public string Reason { get; }
        public Sudoku SudokuSnapshot { get; }

        public ValueSetSolutionHistoryEntry(Point pos, int digit, string reason, int id, Sudoku snapshot, HistoryEntryLevel level)
        {
            Digit = digit;
            Position = pos;
            Reason = reason;
            Id = id;
            Level = level;
            SudokuSnapshot = snapshot.Clone();
        }

        public override string ToString()
        {
            return $"Step {Id}. A digit '{Digit}' has been placed at {Position.ToSudokuCoords()} because {Reason}";
        }

        public override void Print(bool detailedMode = false)
        {
            Console.WriteLine(this);
            if (detailedMode)
                ExtendedSudokuPrinter.Print(SudokuSnapshot, Position);
            else
            {
                BasicSudokuPrinter.Print(SudokuSnapshot, Position);
            }
        }
    }
}
