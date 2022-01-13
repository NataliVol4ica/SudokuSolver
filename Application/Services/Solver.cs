using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Application.Extensions;
using Application.MiscTodo.AlgoRestrictions;
using Application.Models.SudokuAlgo;

namespace Application.Services
{
    public class Solver
    {
        private readonly List<BasicRule> _basicRules;

        public Solver()
        {
            _basicRules = new List<BasicRule>
            {
                new NonRepeatingRowDigitsBasicRule(),
                new NonRepeatingColumnDigitsBasicRule(),
                new NonRepeatingBlockDigitsBasicRule(),
            };
        }

        public SudokuSolution Solve(Sudoku sudoku)
        {
            //queue of cells to check
            var queue = new Queue<Point>();
            for (int i = 0; i < 9; i++) //todo 9
            {
                for (int j = 0; j < 9; j++) //todo 9
                {
                    if (sudoku[i, j].HasValue)
                        queue.Enqueue(new Point(i, j));
                }
            }

            while (queue.TryDequeue(out var cellPosition))
            {
                foreach (var rule in _basicRules)
                {
                    var acquiredValues = rule.ApplyRule(sudoku, cellPosition);
                    if (acquiredValues.Any())
                        queue.EnqueueRange(acquiredValues);
                }
            }

            Console.WriteLine(sudoku);

            throw new NotImplementedException();
        }
    }
}
