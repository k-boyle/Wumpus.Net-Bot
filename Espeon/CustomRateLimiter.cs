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
            throw new Exception("I am a weeb and hate my life");
        }

        public void UpdateLimit(string bucketId, RateLimitInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
