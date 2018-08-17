using System;
using System.Linq;
using System.Threading.Tasks;
using Espeon.Interactive.Callbacks;
using Espeon.Interactive.Criteria;
using Espeon.Modules;
using Umbreon.Interactive.Paginator;
using Voltaic;
using Wumpus.Entities;
using Wumpus.Requests;

namespace Espeon.Interactive.Paginator
{
    public class PaginatedMessageCallback : IReactionCallback, ICallback
    {
        private readonly PaginatedMessage _pager;
        private readonly int _pages;
        private readonly InteractiveService _interactive;
        private int _page = 1;
        private PaginatedAppearanceOptions Options => _pager.Options;

        public Message Message { get; private set; }
        public EspeonContext Context { get; }
        public ICriterion<Emoji> Criterion { get; }
        public TimeSpan? Timeout => TimeSpan.FromMinutes(2);

        public PaginatedMessageCallback(InteractiveService interactive,
            EspeonContext sourceContext,
            PaginatedMessage pager, ICriterion<Emoji> criterion = null)
        {
            _interactive = interactive;
            Context = sourceContext;
            Criterion = criterion ?? new EmptyCriterion<Emoji>();
            _pager = pager;
            _pages = _pager.Pages.Count();
        }

        public async Task DisplayAsync()
        {
            var embed = BuildEmbed();
            var message = await Context.Client.CreateMessageAsync(Context.Channel.Id, new CreateMessageParams
            {
                Content = new Utf8String("."),
                Embed = embed
            });
            Message = message;
            _interactive.AddReactionCallback(message, this);
            _ = Task.Run(async () =>
            {
                await Context.Client.CreateReactionAsync(Context.Channel.Id, message.Id, Options.First);
                await Context.Client.CreateReactionAsync(Context.Channel.Id, message.Id, Options.Back);
                await Context.Client.CreateReactionAsync(Context.Channel.Id, message.Id, Options.Next);
                await Context.Client.CreateReactionAsync(Context.Channel.Id, message.Id, Options.Last);
                await Context.Client.CreateReactionAsync(Context.Channel.Id, message.Id, Options.Stop);
                await Context.Client.CreateReactionAsync(Context.Channel.Id, message.Id, Options.Info);
            });
            if (Timeout.HasValue)
                _ = Task.Delay(Timeout.Value).ContinueWith(_ =>
                {
                    _interactive.RemoveReactionCallback(message);
                    _ = Context.Client.DeleteMessageAsync(Context.Channel.Id, Message.Id);
                });
        }

        public async Task<bool> HandleCallbackAsync(Emoji emote)
        {
            if (emote.Name == Options.First)
            {
                _page = 1;
            }
            else if (emote.Name.Equals(Options.Next))
            {
                if (_page >= _pages)
                {
                    _page = 1;
                    return false;
                }
                ++_page;
            }
            else if (emote.Name.Equals(Options.Back))
            {
                if (_page <= 1)
                {
                    _page = _pages;
                    return false;
                }
                --_page;
            }
            else if (emote.Name.Equals(Options.Last))
            {
                _page = _pages;
            }
            else if (emote.Name.Equals(Options.Stop))
            {
                await Context.Client.DeleteMessageAsync(Context.Channel.Id, Message.Id).ConfigureAwait(false);
                return true;
            }
            else if (emote.Name.Equals(Options.Info))
            {
                //await _interactive.ReplyAndDeleteAsync(Context, Options.InformationText, timeout: Options.InfoTimeout);
                return false;
            }

            await Context.Client.DeleteReactionAsync(Message.ChannelId, Message.Id, Context.User.Id, emote.Name);
            await RenderAsync().ConfigureAwait(false);
            return false;
        }

        protected Embed BuildEmbed()
        {
            return new Embed
            {
                Description = new Utf8String(_pager.Pages.ElementAt(_page - 1).ToString())
            };
        }

        private async Task RenderAsync()
        {
            var embed = BuildEmbed();
            try
            {
                var m = await Context.Client.ModifyMessageAsync(Context.Channel.Id, Message.Id, new ModifyMessageParams
                {
                    Content = new Utf8String("."),
                    Embed = embed
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}