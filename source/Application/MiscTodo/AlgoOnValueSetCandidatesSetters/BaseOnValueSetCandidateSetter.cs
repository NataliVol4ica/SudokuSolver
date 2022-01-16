using Application.Models;

namespace Application.MiscTodo.AlgoOnValueSetCandidatesSetters
{
    //todo when Perform is called, use list instead of creating items each time
    public abstract class BaseOnValueSetCandidateSetter
    {
        public abstract void Perform(int newDigit, Context context);
    }
}
