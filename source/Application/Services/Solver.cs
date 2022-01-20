using System;
using System.Collections.Generic;
using Application.MiscTodo;
using Application.MiscTodo.AlgoHiddenPairsRules;
using Application.MiscTodo.AlgoHiddenSinglesRules;
using Application.MiscTodo.AlgoNakedPairsRules;
using Application.MiscTodo.AlgoNakedSingleRules;
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
            var numOfChanges = 0;

            foreach (var rule in _rules)
            {
                numOfChanges += rule.Apply(_context);
            };
            return numOfChanges > 0;
        }
    }
}
