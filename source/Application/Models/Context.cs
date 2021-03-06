using System;
using System.Drawing;
using Application.Models.SudokuAlgo;
using Application.Models.SudokuAlgo.History;
using Application.Tools.Enums;

namespace Application.Models
{
    public class Context
    {
        private Sudoku _sudokuUnderSolution;
        public Sudoku SudokuUnderSolution
        {
            get => _sudokuUnderSolution;
            set => _sudokuUnderSolution = SudokuUnderSolution is null ? value : _sudokuUnderSolution;
        }
        public SolutionHistory History { get; }

        public Point CellUnderAction { get; set; }

        public HistoryEntryLevel HistoryEntryLevel { get; set; }

        public Context()
        {
            History = new SolutionHistory();
        }
        public bool IsSudokuSolved => _sudokuUnderSolution.IsSolved();

        public Guid HistoryContextId { get; private set; }

        public Guid InitNewContextId()
        {
            HistoryContextId = Guid.NewGuid();
            return HistoryContextId;
        }
    }
}
