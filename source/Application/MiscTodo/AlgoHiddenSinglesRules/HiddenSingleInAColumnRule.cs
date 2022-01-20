using System.Drawing;
using Application.Models;

namespace Application.MiscTodo.AlgoHiddenSinglesRules
{
    public class HiddenSingleInAColumnRule : BaseHiddenSingleRule
    {
        public override int ApplyToAll(Context context)
        {
            var numOfNewValueCells = 0;
            for (int columnId = 0; columnId < 9; columnId++) //todo 9
            {
                var columnIdForFunc = columnId;
                numOfNewValueCells += ApplyToSingle(
                    context.SudokuUnderSolution.Column(columnId),
                    context,
                    "COLUMN",
                    (entityId) => new Point(entityId, columnIdForFunc));
            }

            return numOfNewValueCells;
        }
    }
}
