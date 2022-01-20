using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Application.Extensions;
using Application.MiscTodo.AlgoOnValueSetCandidatesSetters;
using Application.Tools.Enums;

namespace Application.Models.SudokuAlgo
{
    //todo make it work for more or less than 9 digits
    public class SudokuCell
    {
        static SudokuCell()
        {
            OnValueSet += new RowOnValueSetCandidateSetter().Perform;
            OnValueSet += new ColumnOnValueSetCandidateSetter().Perform;
            OnValueSet += new BlockOnValueSetCandidateSetter().Perform;
        }

        private const int MaxDigits = 9;

        public int Value { get; private set; } = -1;

        private List<bool> _candidates;
        private int _numOfRemainingCandidates = 0;
        public int NumOfRemainingCandidates => _numOfRemainingCandidates;

        public bool HasValue => Value > 0;
        public bool ReadyToBeSet => !HasValue && _numOfRemainingCandidates == 1;

        private static event Action<int, Context> OnValueSet;

        public SudokuCell()
        {
            _candidates = Enumerable.Repeat(true, 9).ToList(); //todo 9
            _numOfRemainingCandidates = 9;
        }

        private SudokuCell(int value, List<bool> candidates, int numOfRemainingCandidates)
        {
            _candidates = new List<bool>(candidates);
            Value = value;
            _numOfRemainingCandidates = numOfRemainingCandidates;
        }

        public bool RemoveCandidate(int digit, Context context, Point position, string message = null)
        {
            ValidateDigit(digit);
            var index = digit - 1;
            if (_candidates[index] == false)
                return false;
            _candidates[index] = false;

            context.History.AddRemoveCandidateEntry(digit, context, position, message);

            _numOfRemainingCandidates--;
            return true;
        }

        public void SetValue(int digit, Context context, string reason = null)
        {
            ValidateDigit(digit);
            if (HasValue)
                throw new Exception($"Cannot set cell value to {digit} because it already has a value {Value}"); //todo custom exception
            if (_candidates[digit - 1] == false)
                throw new Exception($"Cannot set cell value to {digit} because it is not a candidate in this cell"); //todo custom exception
            _candidates = Enumerable.Repeat(false, 9).ToList();
            _numOfRemainingCandidates = 0;
            Value = digit;
            context.InitNewContextId();
            OnValueSet(digit, context);
            context.History.AddSetValueEntry(digit, context, reason);
        }

        public bool TrySetValueByCandidates(Context context)
        {
            if (!ReadyToBeSet)
                return false;
            var digitToSet = _candidates.IndexOf(true) + 1;
            SetValue(digitToSet, context, "there is no other digit that can be put in this cell");
            return true;
        }

        public SudokuCell DeepCopy()
        {
            return new SudokuCell(Value, _candidates.Clone(), _numOfRemainingCandidates);
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

        public bool HasCandidate(int candidateId) => _candidates[candidateId]; //todo validate

        public List<int> GetCandidates()
        {
            if (_cachedCandidates.Count != _numOfRemainingCandidates)
            {
                _cachedCandidates = _candidates.Select((c, i) => c ? (int?)i : null).Where(c => c != null).Select(c => c.Value).ToList();
            }

            return _cachedCandidates;
        }

        private List<int> _cachedCandidates = new List<int>();
    }
}