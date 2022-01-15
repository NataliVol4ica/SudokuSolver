using Application.Models;

namespace Application.MiscTodo.AlgoCandidatesSetters
{
    public class BlockCandidateSetter : BaseCandidateSetter
    {
        public override void Perform(int newDigit, Context context)
        {
            var block = context.SudokuUnderSolution.Block(context.CellUnderAction);
            foreach (var cell in block)
            {
                if (cell.HasValue)
                    continue;
                cell.RemoveCandidate(newDigit);
            }
        }
    }
}
