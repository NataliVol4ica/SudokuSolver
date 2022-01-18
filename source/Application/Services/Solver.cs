using System;
using System.Collections.Generic;
using System.Drawing;
using Application.MiscTodo.AlgoCandidateScannerRules;
using Application.MiscTodo.AlgoHiddenPairsRules;
using Application.MiscTodo.AlgoNakedPairsRules;
using Application.Models;

namespace Application.Services
{
    public class Solver
    {
        private readonly Context _context;

        private readonly List<BaseSinglePossiblePositionRule> _singlePossiblePositionRules = new()
        {
            new SinglePossiblePositionInARowRule(),
            new SinglePossiblePositionInAColumnRule(),
            new SinglePossiblePositionInABlockRule(),
        };


        private readonly List<BaseNakedPairRule> _nakedPairRules = new()
        {
            //todo check repetitive code
            new RowNakedPairRule(),
            new ColumnNakedPairRule(),
            new BlockNakedPairRule(),
        };

        private readonly List<BaseHiddenPairRule> _hiddenPairRules = new()
        {
            //todo check repetitive code
            new HiddenPairRule(),
        };

        public Solver(Context context)
        {
            _context = context;
        }

        public void SafeSolve()
        {
            bool isAnyCellUpdated;

            do
            {
                isAnyCellUpdated = ApplyRules();
            } while (isAnyCellUpdated);

        }

        public void Solve()
        {
            try
            {
                bool isAnyCellUpdated;

                do
                {
                    isAnyCellUpdated = ApplyRules();
                } while (isAnyCellUpdated);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Program has crashed. {ex.Message}");
            }
        }

        private bool ApplyRules()
        {
            var numOfNewValueCells = 0;
            numOfNewValueCells += FillAllSingleCandidateCells();
            numOfNewValueCells += FillAllSinglePossiblePositionCells();
            numOfNewValueCells += ApplyNakedPairRules();
            numOfNewValueCells += ApplyHiddenPairRules();
            return numOfNewValueCells > 0;
        }

        private int FillAllSingleCandidateCells()
        {
            var numOfNewValueCells = 0;
            for (int i = 0; i < 9; i++) //todo 9
            {
                for (int j = 0; j < 9; j++) //todo 9
                {
                    _context.CellUnderAction = new Point(i, j);
                    if (_context.SudokuUnderSolution[i, j].TrySetValueByCandidates(_context))
                        numOfNewValueCells++;
                }
            }

            return numOfNewValueCells;
        }

        private int FillAllSinglePossiblePositionCells()
        {
            var numOfChanges = 0;

            foreach (var rule in _singlePossiblePositionRules)
            {
                numOfChanges += rule.ApplyToAll(_context);
            }

            return numOfChanges;
        }

        private int ApplyNakedPairRules()
        {
            var numOfChanges = 0;

            foreach (var rule in _nakedPairRules)
            {
                numOfChanges += rule.ApplyToAll(_context);
            }

            return numOfChanges;
        }

        private int ApplyHiddenPairRules()
        {
            var numOfChanges = 0;

            foreach (var rule in _hiddenPairRules)
            {
                numOfChanges += rule.ApplyToAll(_context);
            }
            return numOfChanges;

        }

        //todo record history for each CELL SET action (done)  +add candidates removal to history with groupping (not done).
        //todo: allow to switch simple/detailed sudoku mode.
    }
}
