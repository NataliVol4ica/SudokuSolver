using System.Collections.Generic;
using Application.Models;

namespace Application.MiscTodo.AlgoNakedPairsRules
{
    public class NakedPairsRule : IRule
    {
        private readonly List<BaseNakedPairRule> _rules = new()
        {
            new RowNakedPairRule(),
            new ColumnNakedPairRule(),
            new BlockNakedPairRule(),
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
