using System.Drawing;
using Application.Models;

namespace Application.MiscTodo.AlgoOnValueSetCandidatesSetters
{
    public class BlockOnValueSetCandidateSetter : BaseOnValueSetCandidateSetter
    {
        public override void Perform(int newDigit, Context context)
        {
            var block = context.SudokuUnderSolution.Block(context.CellUnderAction);
            for (int i = 0; i < 3; i++) //todo 9
            {
                for (int j = 0; j < 3; j++)
                {
                    var cell = block[i, j];
                    if (cell.HasValue)
                        continue;
                    cell.RemoveCandidate(newDigit,
                        context,
                        new Point(context.CellUnderAction.X / 3 * 3 + i, context.CellUnderAction.Y / 3 * 3 + j));
                }
            }
        }
    }
}
