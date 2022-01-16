using System.Drawing;
using Application.Models;

namespace Application.MiscTodo.AlgoOnValueSetCandidatesSetters
{
    public class ColumnOnValueSetCandidateSetter : BaseOnValueSetCandidateSetter
    {
        public override void Perform(int newDigit, Context context)
        {
            var column = context.SudokuUnderSolution.Column(context.CellUnderAction);
            for (int rowId = 0; rowId < 9; rowId++) //todo 9
            {
                var cell = column[rowId];
                if (cell.HasValue)
                    continue;
                cell.RemoveCandidate(newDigit,
                    context,
                    new Point(rowId, context.CellUnderAction.Y));
            }
        }
    }
}
