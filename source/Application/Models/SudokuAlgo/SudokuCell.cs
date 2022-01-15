using System;
using System.Collections.Generic;
using System.Linq;
using Application.MiscTodo.AlgoOnValueSetCandidatesSetters;

namespace Application.Models.SudokuAlgo
{
    //todo make it work for more or less than 9 digits
    public class SudokuCell
    {
        static SudokuCell()
        {
            OnValueSet+= new RowCandidateSetter().Perform;
            OnValueSet+= new ColumnCandidateSetter().Perform;
            OnValueSet+= new BlockCandidateSetter().Perform;
        }

        private const int MaxDigits = 9;

        public int Value { get; private set; } = -1;
        public List<bool> Candidates { get; private set; }

        private int _remainingCandidates = 0;

        public bool HasValue => Value > 0;
        public bool ReadyToBeSet => !HasValue && _remainingCandidates == 1;

        private static event Action<int, Context> OnValueSet;

        public SudokuCell()
        {
            Candidates = Enumerable.Repeat(true, 9).ToList(); //todo 9
            _remainingCandidates = 9;
        }

        private SudokuCell(int value, List<bool> candidates, int remainingCandidates)
        {
            Candidates = new List<bool>(candidates);
            Value = value;
            _remainingCandidates = remainingCandidates;
        }

        public void RemoveCandidate(int digit, Context context)
        {
            ValidateDigit(digit);
            var index = digit - 1;
            if (Candidates[index] == false)
                return;
            Candidates[index] = false;
            //todo
            //context.History.AddSetValueEntry(digit, context, reason);

            _remainingCandidates--;
        }

        public void SetValue(int digit, Context context, bool needsHistoryEntry, string reason = null)
        {
            ValidateDigit(digit);
            Candidates = Enumerable.Repeat(false, 9).ToList();
            _remainingCandidates = 0;
            Value = digit;
            OnValueSet(digit, context);
            if (needsHistoryEntry)
                context.History.AddSetValueEntry(digit, context, reason);
        }

        public bool TrySetValueByCandidates(Context context)
        {
            if (!ReadyToBeSet)
                return false;
            var digitToSet = Candidates.IndexOf(true) + 1;
            SetValue(digitToSet, context, true);
            return true;
        }

        public SudokuCell Clone()
        {
            return new SudokuCell(Value, Candidates, _remainingCandidates);
        }

        public override string ToString()
        {
            return HasValue ? Value.ToString() : ".";
        }

        private bool IsValidDigit(int digit) => digit > 0 && digit <= MaxDigits;
        private void ValidateDigit(int digit)
        {
            if (!IsValidDigit(digit))
                throw new Exception($"Digit {digit} cannot be set as cell value"); //todo custom exception type
        }
    }
}