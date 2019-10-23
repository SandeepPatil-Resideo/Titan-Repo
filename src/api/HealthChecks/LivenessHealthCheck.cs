using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
using Titan.Common.Diagnostics.State;

namespace Titan.Ufc.Addresses.API.HealthChecks
{
    public class LivenessHealthCheck : IHealthCheck
    {
        protected IStateObserver Observer { get; }

        public LivenessHealthCheck(IStateObserver observer)
        {
            Observer = observer;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Observer.IsHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy());
            }

            return Task.FromResult(HealthCheckResult.Unhealthy(
                $"Service is now unhealthy: {Observer.LastException.Message}",
                Observer.LastException));
        }
    }
}
