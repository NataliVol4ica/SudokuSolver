using Application.Models;

namespace Application.MiscTodo
{
    public interface IRule
    {
        int Apply(Context context);
    }
}
