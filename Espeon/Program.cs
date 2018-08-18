using Espeon.Interactive;
using Espeon.Modules;
using Finite.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Voltaic;
using Voltaic.Logging;
using Wumpus.Bot;
using Wumpus.Net;

namespace Espeon
{
    internal class Program
    {
        private static void Main()
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            var manager = new LogManager(LogSeverity.Verbose);

            var client = new WumpusBotClient(restRateLimiter: new DefaultRateLimiter(), logManager: manager)
            {
                Authorization = new AuthenticationHeaderValue("Bot", Environment.GetEnvironmentVariable("Espeon")),
            };

            manager.Output += message => Console.WriteLine(message);
            
            var commands = new CommandServiceBuilder<EspeonContext>()
                .AddPipeline((ctx, next) =>
                {
                    if (ctx.Context.Message.StartsWith("!"))
                        ctx.PrefixLength = 1;
                    return next.Invoke();
                })
                .AddCommandParser<DefaultCommandParser>()
                .AddModule<Commands>()
                .BuildCommandService();

            var services = new ServiceCollection()
                .AddSingleton<InteractiveService>()
                .AddSingleton(client)
                .AddSingleton(commands)
                .BuildServiceProvider();

            _ = Task.Run(async () => { await client.RunAsync(); });

            try
            {
                client.Gateway.MessageCreate += async message =>
                {
                    if (message.Author.Bot.IsSpecified) return;
                    if (!message.Content.StartsWith(new Utf8String("!"))) return;

                    var channel = await client.Rest.GetChannelAsync(message.ChannelId);
                    var context = new EspeonContext(client.Rest, message, channel, commands);
                    await commands.ExecuteAsync(context, services);
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await Task.Delay(-1);
        }
    }
}
