using Finite.Commands;
using Wumpus;
using Wumpus.Entities;

namespace Espeon.Modules
{
    public class EspeonContext : ICommandContext<EspeonContext>
    {
        private readonly string _message;

        public Message Message { get; }
        public WumpusRestClient Client { get; }
        public string Author { get; }

        public EspeonContext(WumpusRestClient client, Message message, CommandService<EspeonContext> commands)
        {
            Client = client;
            Message = message;
            _message = Message.Content.ToString();
            Author = message.Author.ToString();
            Commands = commands;
        }

        string ICommandContext.Message => _message;

        public CommandService<EspeonContext> Commands { get; }
    }
}
