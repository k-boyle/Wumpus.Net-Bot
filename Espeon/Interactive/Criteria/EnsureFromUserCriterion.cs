using Espeon.Modules;
using System.ComponentModel;
using System.Threading.Tasks;
using Wumpus.Entities;

namespace Espeon.Interactive.Criteria
{
    public class EnsureFromUserCriterion : ICriterion<Message>
    {
        private readonly ulong _id;

        public EnsureFromUserCriterion(User user)
            => _id = user.Id;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public EnsureFromUserCriterion(ulong id)
            => _id = id;

        public Task<bool> JudgeAsync(EspeonContext sourceContext, Message parameter)
        {
            bool ok = _id == parameter.Author.Id;
            return Task.FromResult(ok);
        }
    }
}
