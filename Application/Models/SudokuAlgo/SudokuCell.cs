using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Models.SudokuAlgo
{
    //todo make it work for more or less than 9 digits
    public class SudokuCell
    {
        private const int MaxDigits = 9;
        public bool HasValue => Value.HasValue;
        public int? Value { get; private set; }
        public List<bool> Assumptions { get; private set; }
        public SudokuCell(int value)
        {
            if (IsValidDigit(value))
                Value = value;
            else if (value == 0)
                Assumptions = Enumerable.Repeat(true, 9).ToList();
            else
                throw new ArgumentException($"Cell with value '{value}' cannot exist.");
        }

        public bool IsPossible(int digit)
        {
            if (IsValidDigit(digit))
                return Assumptions[digit];
            return false;
        }

        public override string ToString()
        {
            return Value?.ToString() ?? ".";
        }

        private bool IsValidDigit(int digit) => digit > 0 && digit <= MaxDigits;
    }
}