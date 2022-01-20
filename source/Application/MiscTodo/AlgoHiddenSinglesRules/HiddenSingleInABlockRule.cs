using System.Drawing;
using Application.MiscTodo.AlgoCandidateScannerRules;
using Application.Models;

namespace Application.MiscTodo.AlgoNakedSinglesRules
{
    public class HiddenSingleInABlockRule : BaseHiddenSingleRule
    {
        public override int ApplyToAll(Context context)
        {
            var numOfNewValueCells = 0;
            for (int blockId = 0; blockId < 9; blockId++) //todo 9
            {
                var blockIdForFunc = blockId;
                var setOfCells = context.SudokuUnderSolution.PlainBlock(blockId);
                numOfNewValueCells += ApplyToSingle(
                    setOfCells,
                    context, 
                    "BLOCK",
                    (entityId) => new Point(blockIdForFunc / 3 * 3 + entityId / 3, blockIdForFunc % 3 * 3 + entityId % 3));
            }

            return numOfNewValueCells;
        }
    }
}
