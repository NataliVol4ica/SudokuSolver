using System.Collections.Generic;
using Application.Models;

namespace Application.MiscTodo.AlgoHiddenSinglesRules
{
    public class HiddenSinglesRule : IRule
    {
        private readonly List<BaseHiddenSingleRule> _rules = new()
        {
            new HiddenSingleInARowRule(),
            new HiddenSingleInAColumnRule(),
            new HiddenSingleInABlockRule(),
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
