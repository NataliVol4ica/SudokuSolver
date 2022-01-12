using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Application.Extensions;

namespace Application.Models.SudokuAlgo
{
    public class Sudoku
    {
        public Point Size { get;  } = new Point(3, 3);
        private SudokuCell[,] _cells;

        public Sudoku(List<List<int>> source)
        {
            _cells = source.Select(r => r.Select(d=> new SudokuCell(d)).ToArray()).ToArray().ToMultidimensional();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
