using System;

namespace Application.Models.SudokuAlgo
{
    public class SolutionStep
    {
        public string Description { get; set; }
        public string Rule { get; set; }
        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
