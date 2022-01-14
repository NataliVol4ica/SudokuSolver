using System.Drawing;
using Application.Tools;

namespace Application.Models.SudokuAlgo
{
    public class SolutionHistoryEntry
    {
        public int Id { get; }
        public int Digit { get; }
        public Point Position { get; }
        public string Reason { get; }
        public Sudoku SudokuSnapshot { get; }

        public SolutionHistoryEntry(Point pos, int digit, string reason, int id, Sudoku snapshot)
        {
            Digit = digit;
            Position = pos;
            Reason = reason;
            Id = id;
            SudokuSnapshot = snapshot.Clone();
        }

        public override string ToString()
        {
            return $"Step {Id}. A digit '{Digit}' has been placed at {Position.ToSudokuCoords()} because {Reason}";
        }
    }
}
