using System.Drawing;
using Application.Models;

namespace Application.MiscTodo.AlgoCandidateScannerRules
{
    public class SinglePossiblePositionInABlockRule : SinglePossiblePositionInAnEntityRule
    {
        public int ApplyToAll(Context context)
        {
            var numOfNewValueCells = 0;
            for (int blockId = 0; blockId < 9; blockId++) //todo 9
            {
                var blockIdForFunc = blockId;
                numOfNewValueCells += ApplyToSingle(
                    context.SudokuUnderSolution.PlainBlock(blockId),
                    context, 
                    "BLOCK",
                    (entityId) => new Point(blockIdForFunc / 3 + entityId / 3, blockIdForFunc % 3 + entityId % 3));
            }

            return numOfNewValueCells;
        }
    }
}
