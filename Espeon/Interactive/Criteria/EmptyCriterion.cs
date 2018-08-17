using Espeon.Modules;
using System.Threading.Tasks;

namespace Espeon.Interactive.Criteria
{
    public class EmptyCriterion<T> : ICriterion<T>
    {
        public Task<bool> JudgeAsync(EspeonContext sourceContext, T parameter)
            => Task.FromResult(true);
    }
}
