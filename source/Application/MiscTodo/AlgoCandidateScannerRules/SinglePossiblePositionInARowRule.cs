using System.Drawing;
using Application.Models;

namespace Application.MiscTodo.AlgoCandidateScannerRules
{
    public class SinglePossiblePositionInARowRule : SinglePossiblePositionInAnEntityRule
    {
        public int ApplyToAll(Context context)
        {
            var numOfNewValueCells = 0;
            for (int rowId = 0; rowId < 9; rowId++) //todo 9
            {
                var rowIdForFunc = rowId;
                numOfNewValueCells += ApplyToSingle(
                    context.SudokuUnderSolution.Row(rowId),
                    context, 
                    "ROW", 
                    (entityId) => new Point(rowIdForFunc, entityId));
            }

            return numOfNewValueCells;
        }
    }
}
