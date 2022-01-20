using System.Collections.Generic;
using Application.Models;

namespace Application.MiscTodo.AlgoXWingRules
{
    public class XWingRule : IRule
    {
        private readonly List<BaseXWingRule> _rules = new()
        {
            new RowXWingRule(),
            new ColumnXWingRule(),
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
