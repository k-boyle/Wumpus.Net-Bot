using System.Threading.Tasks;
using Finite.Commands;
using Voltaic;
using Wumpus.Entities;
using Wumpus.Requests;

namespace Espeon.Modules
{
    public abstract class EspeonBase : ModuleBase<EspeonContext>
    {
        protected async Task<Message> SendMessageAsync(string content, bool isTTS = false, Embed embed = null)
        {
            return await Context.Client.CreateMessageAsync(Context.Message.ChannelId, new CreateMessageParams
            {
                Content = new Utf8String(content),
                Embed = embed,
                IsTextToSpeech = isTTS
            });
        }
    }
}
