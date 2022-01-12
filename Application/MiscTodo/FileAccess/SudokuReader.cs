using System.Linq;
using Application.Models.SudokuAlgo;

namespace Application.MiscTodo.FileAccess
{
    public class SudokuReader
    {
        public Sudoku ReadFrom(string path)
        {
            //todo validate file existence and data consistency
            string[] lines = System.IO.File.ReadAllLines(path);

            var digits = lines.Select(line => line.Select(d => d - '0').ToList()).ToList();
            return new Sudoku(digits);
        }
    }
}
