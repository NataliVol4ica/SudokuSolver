using System.Collections.Generic;
using System.Drawing;
using Application.MiscTodo.AlgoCandidateScannerRules;
using Application.MiscTodo.AlgoNakedPairsRules;
using Application.Models;

namespace Application.Services
{
    public class Solver
    {
        private readonly Context _context;

        private readonly List<BaseNakedPairRule> _nakedPairRules = new()
        {
            new NakedPairRule()
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
            var numOfNewValueCells = 0;
            numOfNewValueCells += FillAllSingleCandidateCells();
            numOfNewValueCells += FillAllSinglePossiblePositionCells();
            numOfNewValueCells += ApplyNakedPairRules();
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
            var numOfNewValueCells = 0;
            numOfNewValueCells += new SinglePossiblePositionInARowRule().ApplyToAll(_context);
            numOfNewValueCells += new SinglePossiblePositionInAColumnRule().ApplyToAll(_context);
            numOfNewValueCells += new SinglePossiblePositionInABlockRule().ApplyToAll(_context);
            return numOfNewValueCells;
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

        //todo record history for each CELL SET action (done)  +add candidates removal to history with groupping (not done).
        //todo: allow to switch simple/detailed sudoku mode.
    }
}
