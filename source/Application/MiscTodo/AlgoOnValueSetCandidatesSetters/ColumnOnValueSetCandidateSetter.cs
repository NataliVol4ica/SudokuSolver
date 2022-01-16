using System.Drawing;
using Application.Models;

namespace Application.MiscTodo.AlgoOnValueSetCandidatesSetters
{
    public class ColumnOnValueSetCandidateSetter : BaseOnValueSetCandidateSetter
    {
        public override void Perform(int newDigit, Context context)
        {
            var column = context.SudokuUnderSolution.Column(context.CellUnderAction);
            for (int columnId = 0; columnId < 9; columnId++) //todo 9
            {
                var cell = column[columnId];
                if (cell.HasValue)
                    continue;
                cell.RemoveCandidate(newDigit,
                    context,
                    new Point(context.CellUnderAction.X, columnId));
            }
        }
    }
}
