using Espeon.Interactive.Criteria;
using Espeon.Modules;
using System.Threading.Tasks;
using Wumpus.Entities;

namespace Espeon.Interactive.Paginator
{
    internal class EnsureIsIntegerCriterion : ICriterion<Message>
    {
        public Task<bool> JudgeAsync(EspeonContext sourceContext, Message parameter)
        {
            bool ok = int.TryParse(parameter.Content.ToString(), out _);
            return Task.FromResult(ok);
        }
    }
}
