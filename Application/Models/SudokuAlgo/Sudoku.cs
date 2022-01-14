using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Application.Extensions;

namespace Application.Models.SudokuAlgo
{
    public class Sudoku
    {
        public Point Size { get;  } = new Point(9, 9);

        //comes from outside
        private readonly SudokuCell[,] _cells;

        //pre-generated constants based on cells
        private readonly List<SudokuCell[]> _rows;
        private readonly List<SudokuCell[]> _columns;
        private readonly List<SudokuCell[,]> _blocks;

        public Sudoku(List<List<int>> source)
        {
            _cells = source.Select(r => r.Select(d=> new SudokuCell(d)).ToArray()).ToArray().ToMultidimensional();
            _rows = Enumerable.Range(0, 9).Select(v => CreateRow(_cells, v)).ToList();
            _columns = Enumerable.Range(0, 9).Select(v => CreateColumn(_cells, v)).ToList();
            _blocks = Enumerable.Range(0, 9).Select(v => CreateBlock(_cells, v / 3, v % 3)).ToList();
        }
        private Sudoku(SudokuCell[,] source)
        {
            _cells = source;

            _rows = Enumerable.Range(0, 9).Select(v => CreateRow(_cells, v)).ToList();
            _columns = Enumerable.Range(0, 9).Select(v => CreateColumn(_cells, v)).ToList();
            _blocks = Enumerable.Range(0, 9).Select(v => CreateBlock(_cells, v / 3, v % 3)).ToList();
        }

        #region Rows, Columns and Blocks

        public SudokuCell[] Row(Point position) => _rows[position.X];
        public SudokuCell[] Column(Point position) => _columns[position.Y];
        public SudokuCell[,] Block(Point position) => _blocks[position.X / 3 * 3 + position.Y / 3];

        private static SudokuCell[] CreateRow(SudokuCell[,] cells, int positionX)
        {
            return Enumerable.Range(0, cells.GetLength(1))
                .Select(x => cells[positionX, x])
                .ToArray();
        }
        private static SudokuCell[] CreateColumn(SudokuCell[,] cells, int positionY)
        {
            return Enumerable.Range(0, cells.GetLength(0))
                .Select(x => cells[x, positionY])
                .ToArray();
        }
        private static SudokuCell[,] CreateBlock(SudokuCell[,] cells, int positionX, int positionY)
        {
            var result = new SudokuCell[3, 3];
            var start = new Point(positionX * 3, positionY * 3);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[i, j] = cells[start.X + i, start.Y + j];
                }
            }

            return result;
        }

        #endregion Rows, Columns and Blocks

        public SudokuCell[,] DeepCopyCells()
        {
            var cells = new SudokuCell[9, 9];
            for (int i = 0; i < 9; i++) //todo 9
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j] = _cells[i, j].Clone();
                }
            }

            return cells;
        }

        public Sudoku Clone()
        {
            return new Sudoku(DeepCopyCells());
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (int i = 0; i < 9; i++)
            {
                if (i != 0 && i % 3 == 0)
                    result.Append("------+------+------\n");
                for (int j = 0; j < 9; j++)
                {
                    if (j < 8)
                    {
                        result.Append(_cells[i, j].ToString() + " ");
                        if (j > 0 && j % 3 == 2)
                            result.Append('|');
                    }
                    else
                    {
                        result.Append(_cells[i, j].ToString());
                    }
                }

                result.Append('\n');
            }

            return result.ToString();
        }

        public SudokuCell this[int index1, int index2]
        {
            get => _cells[index1, index2];
        }

        public SudokuCell this[Point position] => this[position.X, position.Y];

        public bool IsSolved()
        {
            for (int i = 0; i < 9; i++) //todo 9
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!_cells[i, j].HasValue)
                        return false;
                }
            }

            return true;
        }
    }
}
