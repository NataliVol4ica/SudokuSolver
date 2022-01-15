using Application.Models;

namespace Application.MiscTodo.AlgoCandidatesSetters
{
    public class ColumnCandidateSetter : BaseCandidateSetter
    {
        public override void Perform(int newDigit, Context context)
        {
            var column = context.SudokuUnderSolution.Column(context.CellUnderAction);
            foreach (var cell in column)
            {
                if (cell.HasValue)
                    continue;
                cell.RemoveCandidate(newDigit);
            }
        }
    }
}
