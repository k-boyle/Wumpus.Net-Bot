using Espeon.Modules;
using Finite.Commands;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Wumpus.Bot;

namespace Espeon
{
    internal class Program
    {
        private static void Main()
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            var client = new WumpusBotClient()
            {
                Authorization = new AuthenticationHeaderValue("Bot", Environment.GetEnvironmentVariable("Espeon")),
            };

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

           _ = Task.Run(async () => { await client.RunAsync(); });

            Console.WriteLine("Connected");

            try
            {
                client.Gateway.MessageCreate += async message =>
                {
                    Console.WriteLine("Message received");

                    var context = new EspeonContext(client.Rest, message, commands);
                    await commands.ExecuteAsync(context, null);
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
