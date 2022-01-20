using System.Collections.Generic;
using Application.Models;

namespace Application.MiscTodo.AlgoXWingRules
{
    public abstract class BaseXWingRule
    {
        public abstract int ApplyToAll(Context context);

        protected List<DigitStatistics> InitializeStatistics(int size)
        {
            var statistics = new List<DigitStatistics>(size);
            for (int i = 0; i < size; i++)
                statistics.Add(new DigitStatistics());
            return statistics;
        }
    }
}
