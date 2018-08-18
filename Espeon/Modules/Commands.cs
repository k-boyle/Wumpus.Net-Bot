using System;
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
        private readonly CommandService<EspeonContext> _commands;

        public Commands(InteractiveService interactive, CommandService<EspeonContext> commands)
        {
            _interactive = interactive;
            _commands = commands;
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

        [Command("help")]
        public Task HelpCommand()
            => SendMessageAsync("I can't access commands :(");

        [OnBuilding]
        public static void OnBuilding(ModuleBuilder _)
            => Console.WriteLine("building");

        [OnExecuting]
        public void OnExecuting(CommandInfo _)
            => Console.WriteLine("On executing");
    }
}
