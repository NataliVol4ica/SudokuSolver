using System;
using System.Collections.Generic;
using System.Linq;
using Application.Infrastructure;
using Application.MiscTodo;

namespace Application.Models.SudokuAlgo
{
    //todo make it work for more or less than 9 digits
    public class SudokuCell
    {
        private const int MaxDigits = 9;
        public bool HasValue => Value > 0;
        public int Value { get; private set; } = -1;
        public List<bool> Candidates { get; private set; }
        public int RemainingCandidates { get; private set; } = 0;

        public SudokuCell(int value)
        {
            if (IsValidDigit(value))
                Value = value;
            else if (value == 0)
            {
                Candidates = Enumerable.Repeat(true, 9).ToList(); //todo 9
                RemainingCandidates = 9;
            }
            else
                throw new ArgumentException($"Cell with value '{value}' cannot exist.");
        }

        public bool IsPossible(int digit)
        {
            if (IsValidDigit(digit))
                return Candidates[digit];
            return false;
        }

        public override string ToString()
        {
            return HasValue ? Value.ToString() : ".";
        }

        private bool IsValidDigit(int digit) => digit > 0 && digit <= MaxDigits;

        //todo protect index
        public CandidateRemovalResult RemoveCandidate(int digit)
        {
            var index = digit - 1;
            if (Candidates[index] == false)
                return CandidateRemovalResult.NotRemoved;
            Candidates[index] = false;
            RemainingCandidates--;

            if (RemainingCandidates == 1)
            {
                Value = ValueOfSingleCandidate();
                return CandidateRemovalResult.RemovedAndHasSingleValue;
            }
            return CandidateRemovalResult.Removed;
        }

        //todo protect digit
        public void SetValue(int digit)
        {
            Value = digit;
            RemainingCandidates = 1;
            for (int i = 0; i < 9; i++)
            {
                if (i + 1 == digit)
                    Candidates[i] = true;
                else
                    Candidates[i] = false;
            }
        }

        private int ValueOfSingleCandidate()
        {
            return Candidates.IndexOf(true) + 1;
        }
    }
}