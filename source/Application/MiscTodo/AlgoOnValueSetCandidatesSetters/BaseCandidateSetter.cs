using Application.Models;

namespace Application.MiscTodo.AlgoOnValueSetCandidatesSetters
{
    public abstract class BaseCandidateSetter
    {
        public abstract void Perform(int newDigit, Context context);
    }
}
