﻿using System.Collections.Generic;
using Application.MiscTodo.AlgoRestrictions;
using Application.Models;

namespace Application.Services
{
    public class Solver
    {
        private static readonly List<BasicRule> _basicRules;
        private readonly Context _context;

        //todo
        static Solver()
        {
            _basicRules = new List<BasicRule>
            {
                new NonRepeatingRowDigitsBasicRule(),
                new NonRepeatingColumnDigitsBasicRule(),
                new NonRepeatingBlockDigitsBasicRule(),
                new SinglePossiblePositionInARowRule(),
                new SinglePossiblePositionInAColumnRule(),
            };
        }

        public Solver(Context context)
        {
            _context = context;
        }

        public void Solve()
        {
            
        }
        
        //record history for each CELL SET action. add candidates removal to history.

        //0. PRE
        //enqueue all filled cells.
        //fill candidates for all of them
        //RESULT: will be done during cell value set process

        //1. CANDIDATES FILLING
        //Scan sudoku. For every cell, where there is a single candidate, set the value. Trigger sudoku candidates update algo for single cell.
        //repeat until scan adds 0 new records

        //2. Rule set 1
        //scan all rows, columns and blocks. If in one of them there is a single possible position for some digit , put it.

        //3. Rule set 2 todo. 

        //repeat 1-3 until no action can be performed

        //todo: allow to switch simple/detailed sudoku mode.
        //todo: more detailed sudoku table. Show candidates
        //todo: more detailed history. Show all removed candidates when the cell is filled?
        
    }
}
