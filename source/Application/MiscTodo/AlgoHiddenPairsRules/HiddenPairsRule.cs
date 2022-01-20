using System.Collections.Generic;
using Application.Models;

namespace Application.MiscTodo.AlgoHiddenPairsRules
{
    public class HiddenPairsRule : IRule
    {
        private readonly List<BaseHiddenPairRule> _rules = new()
        {
            new BlockHiddenPairRule(),
            new ColumnHiddenPairRule(),
            new RowHiddenPairRule(),

        };

        public int Apply(Context context)
        {
            var numOfChanges = 0;

            foreach (var rule in _rules)
            {
                numOfChanges += rule.ApplyToAll(context);
            }

            return numOfChanges;
        }
    }
}
