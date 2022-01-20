using System.Linq;
using Application.Models;
using Application.Models.SudokuAlgo;

namespace Application.MiscTodo.FileAccess
{
    //todo move elsewhere (own project?)
    public class SudokuReader
    {
        private readonly Context _context;

        public SudokuReader(Context context)
        {
            _context = context;
        }
        public void ReadFrom(string path)
        {
            //todo validate file existence and data consistency
            string[] lines = System.IO.File.ReadAllLines(path);

            var digits = lines.Select(line => line.Select(d => d - '0').ToList()).ToList();
            Sudoku.Create(digits, _context);
        }
    }
}
