using Finite.Commands;
using System.Threading.Tasks;

namespace Espeon.Modules
{
    //[Alias("group")]
    public class Commands : EspeonBase
    {
        [Command("ping")]
        public Task SendPong()
            => SendMessageAsync("pong");

        [Command("cmd")]
        public Task SendCmd()
            => SendMessageAsync("this works");
    }
}
