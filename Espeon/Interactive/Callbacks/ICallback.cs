using System.Threading.Tasks;
using Wumpus.Entities;

namespace Espeon.Interactive.Callbacks
{
    public interface ICallback
    {
        Task DisplayAsync();
        Message Message { get; }
    }
}
