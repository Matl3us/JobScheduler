using JobScheduler.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobScheduler.Api.Endpoints;

public static class ExecutionEndpoints
{
    public static void MapExecutionsWithJobEndpoints(this IEndpointRouteBuilder routeBuilder,
        IJobExecutionManagementService executionService)
    {
        routeBuilder.MapGet("/{jobId}/executions",
            async ([FromRoute] Guid jobId,
                [FromQuery] int pageSize,
                [FromQuery] int page
            ) =>
            {
                var executions = await executionService.GetJobExecutionsAsync(jobId, pageSize, page);
                return Results.Json(new { data = executions });
            });
    }

    public static void MapExecutionsEndpoints(this IEndpointRouteBuilder routeBuilder,
        IJobExecutionManagementService executionService)
    {
        routeBuilder.MapGet("/{executionId}",
            async ([FromRoute] Guid executionId) =>
            {
                var execution = await executionService.GetExecutionAsync(executionId);
                return Results.Json(new { data = execution });
            });
    }
}