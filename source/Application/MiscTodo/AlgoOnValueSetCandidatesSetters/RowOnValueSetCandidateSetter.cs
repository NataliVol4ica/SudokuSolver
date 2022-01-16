using System.Drawing;
using Application.Models;

namespace Application.MiscTodo.AlgoOnValueSetCandidatesSetters
{
    public class RowOnValueSetCandidateSetter : BaseOnValueSetCandidateSetter
    {
        public override void Perform(int newDigit, Context context)
        {
            var row = context.SudokuUnderSolution.Row(context.CellUnderAction);
            for (int rowId = 0; rowId < 9; rowId++) //todo 9
            {
                var cell = row[rowId];
                if (cell.HasValue)
                    continue;
                cell.RemoveCandidate(newDigit,
                    context,
                    new Point(rowId, context.CellUnderAction.Y)
                    );
            }
        }
    }
}
