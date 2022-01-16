using Application.Models;

namespace Application.MiscTodo.AlgoOnValueSetCandidatesSetters
{
    public class ColumnOnValueSetCandidateSetter : BaseOnValueSetCandidateSetter
    {
        public override void Perform(int newDigit, Context context)
        {
            var column = context.SudokuUnderSolution.Column(context.CellUnderAction);
            foreach (var cell in column)
            {
                if (cell.HasValue)
                    continue;
                cell.RemoveCandidate(newDigit, context);
            }
        }
    }
}
