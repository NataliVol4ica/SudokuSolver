using System.Collections.Generic;
using Application.MiscTodo;
using Application.MiscTodo.AlgoHiddenPairsRules;
using Application.MiscTodo.AlgoHiddenSinglesRules;
using Application.MiscTodo.AlgoNakedPairsRules;
using Application.MiscTodo.AlgoNakedSingleRules;
using Application.MiscTodo.AlgoPointingDigitsRules;
using Application.MiscTodo.AlgoXWingRules;
using Application.Models;

namespace Application.Services
{
    public class Solver
    {
        private readonly Context _context;

        private readonly List<IRule> _rules = new()
        {
            new NakedSingleRule(),
            new HiddenSinglesRule(),
            new PointingDigitsRule(),
            new NakedPairsRule(),
            new HiddenPairsRule(),
            new XWingRule(),
        };

        public Solver(Context context)
        {
            _context = context;
        }

        public void Solve()
        {
            bool isAnyCellUpdated;

            do
            {
                isAnyCellUpdated = ApplyRules();
            } while (isAnyCellUpdated);
        }

        private bool ApplyRules()
        {
            foreach (var rule in _rules)
            {
                if (rule.Apply(_context) > 0)
                    return true; //so that we apply rules according to their hierarchy //todo do the same inside rules? is it worth?
            };
            return false;
        }
    }
}
