using System.Drawing;
using Application.Models;

namespace Application.MiscTodo.AlgoOnValueSetCandidatesSetters
{
    public class ColumnOnValueSetCandidateSetter : BaseOnValueSetCandidateSetter
    {
        public override void Perform(int newDigit, Context context)
        {
            var candidateId = newDigit - 1;
            var column = context.SudokuUnderSolution.Column(context.CellUnderAction);
            for (int rowId = 0; rowId < 9; rowId++) //todo 9
            {
                var cell = column[rowId];
                if (cell.HasValue)
                    continue;
                cell.RemoveCandidate(candidateId,
                    context,
                    new Point(rowId, context.CellUnderAction.Y));
            }
        }
    }
}
