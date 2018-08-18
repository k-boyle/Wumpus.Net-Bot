using System;
using System.Threading;
using System.Threading.Tasks;
using Wumpus.Net;

namespace Espeon
{
    public class CustomRateLimiter : IRateLimiter
    {
        public Task EnterLockAsync(string bucketId, CancellationToken cancelToken)
        {
            throw new Exception("I don't know what the fuck I'm doing :-)");
        }

        public void UpdateLimit(string bucketId, RateLimitInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
