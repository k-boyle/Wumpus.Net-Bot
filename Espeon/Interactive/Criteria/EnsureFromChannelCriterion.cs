using Espeon.Modules;
using System.Threading.Tasks;
using Wumpus.Entities;

namespace Espeon.Interactive.Criteria
{
    public class EnsureFromChannelCriterion : ICriterion<Message>
    {
        private readonly ulong _channelId;

        public EnsureFromChannelCriterion(Channel channel)
            => _channelId = channel.Id;

        public Task<bool> JudgeAsync(EspeonContext sourceContext, Message parameter)
        {
            bool ok = _channelId == parameter.ChannelId;
            return Task.FromResult(ok);
        }
    }
}
