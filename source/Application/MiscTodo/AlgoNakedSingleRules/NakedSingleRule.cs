using System.Drawing;
using Application.Models;

namespace Application.MiscTodo.AlgoNakedSingleRules
{
    public class NakedSingleRule : IRule
    {
        public int Apply(Context context)
        {
            var numOfNewValueCells = 0;
            for (int i = 0; i < 9; i++) //todo 9
            {
                for (int j = 0; j < 9; j++) //todo 9
                {
                    context.CellUnderAction = new Point(i, j);
                    context.InitNewContextId();
                    if (context.SudokuUnderSolution[i, j].TrySetValueByCandidates(context))
                        numOfNewValueCells++;
                }
            }

            return numOfNewValueCells;
        }
    }
}
