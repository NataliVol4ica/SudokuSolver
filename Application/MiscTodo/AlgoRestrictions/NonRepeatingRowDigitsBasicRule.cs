using System.Drawing;
using Application.Models.SudokuAlgo;

namespace Application.MiscTodo.AlgoRestrictions
{
    public class NonRepeatingRowDigitsBasicRule
    {
        public void ApplyRule(Sudoku sudoku, Point position) //todo history
        {
            var cell = sudoku[position];

            if (!cell.HasValue)
                return;

        }
    }
}
