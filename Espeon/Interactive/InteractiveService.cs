using Espeon.Interactive.Callbacks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Espeon.Interactive.Paginator;
using Espeon.Modules;
using Wumpus.Bot;
using Wumpus.Entities;
using Wumpus.Events;

namespace Espeon.Interactive
{
    public class InteractiveService : IDisposable
    {
        private WumpusBotClient Client { get; }

        private readonly Dictionary<ulong, IReactionCallback> _callbacks;
        private readonly TimeSpan _defaultTimeout;

        public InteractiveService(WumpusBotClient client, TimeSpan? defaultTimeout = null)
        {
            Client = client;
            Client.Gateway.MessageReactionAdd += HandleReaction;

            _callbacks = new Dictionary<ulong, IReactionCallback>();
            _defaultTimeout = defaultTimeout ?? TimeSpan.FromSeconds(15);
        }

        public void AddReactionCallback(Message message, IReactionCallback callback)
            => _callbacks[message.Id] = callback;

        public void RemoveReactionCallback(Message message)
            => RemoveReactionCallback(message.Id);

        private void RemoveReactionCallback(ulong id)
            => _callbacks.Remove(id);

        public void ClearReactionCallbacks()
            => _callbacks.Clear();

        public async Task<Message> SendPaginatedMessageAsync(EspeonContext context, PaginatedMessage pager)
        {
            var callback = new PaginatedMessageCallback(this, context, pager);
            await callback.DisplayAsync().ConfigureAwait(false);
            return callback.Message;
        }

        private void HandleReaction(MessageReactionAddEvent reaction)
        {
            _ = Task.Run(async () =>
            {
                var currentUser = await Client.Rest.GetCurrentUserAsync();
                if (reaction.UserId == currentUser.Id) return;
                if (!(_callbacks.TryGetValue(reaction.MessageId.RawValue, out var callback))) return;
                if (!(await callback.Criterion.JudgeAsync(callback.Context, reaction.Emoji)
                    .ConfigureAwait(false))) return;
                
                if (await callback.HandleCallbackAsync(reaction.Emoji).ConfigureAwait(false))
                    RemoveReactionCallback(reaction.MessageId);
            });
        }

        public void Dispose()
        {
            Client.Gateway.MessageReactionAdd -= HandleReaction;
        }
    }
}
