using System.Drawing;
using Application.MiscTodo.AlgoCandidateScannerRules;
using Application.Models;

namespace Application.Services
{
    public class Solver
    {
        private readonly Context _context;

        //todo
        static Solver()
        {
        }

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

        //record history for each CELL SET action. add candidates removal to history.

        //0. PRE
        //enqueue all filled cells.
        //fill candidates for all of them
        //RESULT: will be done during cell value set process

        //1. CANDIDATES FILLING
        //Scan sudoku. For every cell, where there is a single candidate, set the value. Trigger sudoku candidates update algo for single cell.
        //repeat until scan adds 0 new records
        //RESULT: done here

        //2. Rule set 1
        //scan all rows, columns and blocks. If in one of them there is a single possible position for some digit , put it.

        //3. Rule set 2 todo. 

        //repeat 1-3 until no action can be performed

        //todo: allow to switch simple/detailed sudoku mode.
        //todo: more detailed sudoku table. Show candidates
        //todo: more detailed history. Show all removed candidates when the cell is filled?
        
    }
}
