using Application.Models;

namespace Application.MiscTodo.AlgoOnValueSetCandidatesSetters
{
    public class BlockOnValueSetCandidateSetter : BaseOnValueSetCandidateSetter
    {
        public override void Perform(int newDigit, Context context)
        {
            var block = context.SudokuUnderSolution.Block(context.CellUnderAction);
            foreach (var cell in block)
            {
                if (cell.HasValue)
                    continue;
                cell.RemoveCandidate(newDigit, context);
            }
        }
    }
}
