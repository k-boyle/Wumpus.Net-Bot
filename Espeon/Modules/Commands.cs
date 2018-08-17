using Espeon.Interactive;
using Espeon.Interactive.Paginator;
using Finite.Commands;
using System.Threading.Tasks;

namespace Espeon.Modules
{
    [Alias("group")]
    public class Commands : EspeonBase
    {
        private readonly InteractiveService _interactive;

        public Commands(InteractiveService interactive)
        {
            _interactive = interactive;
        }

        [Command("ping")]
        public Task SendPong()
            => SendMessageAsync("pong");

        [Command]
        public async Task Paginator()
        {
            await _interactive.SendPaginatedMessageAsync(Context, new PaginatedMessage
            {
                Pages = new []{"W", "u", "m", "p", "u", "s"}
            });
        }
    }
}
