using Finite.Commands;

namespace Espeon.Modules
{
    public class SuccessResult : IResult
    {
        public bool IsSuccess { get; } = true;
    }
}
