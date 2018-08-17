using Espeon.Interactive.Criteria;
using Espeon.Modules;
using System.Threading.Tasks;
using Wumpus.Entities;

namespace Espeon.Interactive.Paginator
{
    internal class EnsureReactionFromSourceUserCriterion : ICriterion<Emoji>
    {
        public Task<bool> JudgeAsync(EspeonContext sourceContext, Emoji parameter)
        {
            bool ok = parameter.User.Value.Id == sourceContext.User.Id;
            return Task.FromResult(ok);
        }
    }
}
