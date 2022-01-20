using Application.Models;
using Application.Tools.Enums;

namespace Application.MiscTodo.AlgoXWingRules
{
    public class ColumnXWingRule : BaseXWingRule
    {
        private int ApplyToColumn(int rowId, Context context)
        {
            var numOfChanges = 0;

            return numOfChanges;
        }

        public override int ApplyToAll(Context context)
        {
            context.HistoryEntryLevel = HistoryEntryLevel.CandidateSet;

            var numOfChanges = 0;
            for (int columnId = 0; columnId < 9; columnId++) //todo 9
            {
                numOfChanges += ApplyToColumn(
                    columnId,
                    context);
            }

            return numOfChanges;
        }
    }
}
