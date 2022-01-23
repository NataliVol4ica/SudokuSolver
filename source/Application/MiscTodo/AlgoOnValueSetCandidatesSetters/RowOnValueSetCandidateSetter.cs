using System.Drawing;
using Application.Models;

namespace Application.MiscTodo.AlgoOnValueSetCandidatesSetters
{
    public class RowOnValueSetCandidateSetter : BaseOnValueSetCandidateSetter
    {
        public override void Perform(int newDigit, Context context)
        {
            var candidateId = newDigit - 1;
            var row = context.SudokuUnderSolution.Row(context.CellUnderAction);
            for (int colId = 0; colId < 9; colId++) //todo 9
            {
                var cell = row[colId];
                if (cell.HasValue)
                    continue;
                cell.RemoveCandidate(candidateId,
                    context,
                    new Point(context.CellUnderAction.X, colId)
                    );
            }
        }
    }
}
