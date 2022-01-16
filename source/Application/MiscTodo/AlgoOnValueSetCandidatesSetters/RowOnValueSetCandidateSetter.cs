using Application.Models;

namespace Application.MiscTodo.AlgoOnValueSetCandidatesSetters
{
    public class RowOnValueSetCandidateSetter : BaseOnValueSetCandidateSetter
    {
        public override void Perform(int newDigit, Context context)
        {
            var row = context.SudokuUnderSolution.Row(context.CellUnderAction);
            foreach (var cell in row)
            {
                if (cell.HasValue)
                    continue;
                cell.RemoveCandidate(newDigit, context);
            }
        }
    }
}
