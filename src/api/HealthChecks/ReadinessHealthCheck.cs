﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace TitanTemplate.titanaddressapi.HealthChecks
{
    public class ReadinessHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
