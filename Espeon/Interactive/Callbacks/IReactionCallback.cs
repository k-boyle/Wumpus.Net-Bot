using Espeon.Interactive.Criteria;
using Espeon.Modules;
using System;
using System.Threading.Tasks;
using Wumpus.Entities;

namespace Espeon.Interactive.Callbacks
{
    public interface IReactionCallback
    {
        ICriterion<Emoji> Criterion { get; }
        TimeSpan? Timeout { get; }
        EspeonContext Context { get; }

        Task<bool> HandleCallbackAsync(Emoji reaction);
    }
}
