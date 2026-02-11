using JobScheduler.Core.DTOs;
using JobScheduler.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobScheduler.Api.Endpoints;

public static class JobEndpoints
{
    public static IEndpointRouteBuilder MapJobEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/",
            async ([FromBody] CreateJobRequest request,
                IJobManagementService jobService) =>
            {
                var job = await jobService.CreateJobAsync(request);
                return Results.Created($"/api/jobs/{job.Id}", job);
            });

        routeBuilder.MapGet("/",
            async (IJobManagementService jobService) =>
            {
                var jobs = await jobService.GetAllJobsAsync();
                return Results.Json(new { data = jobs });
            });

        routeBuilder.MapGet("/{jobId}",
            async ([FromRoute] Guid jobId,
                IJobManagementService jobService) =>
            {
                var job = await jobService.GetJobAsync(jobId);
                return Results.Json(new { data = job });
            });

        routeBuilder.MapPut("/{jobId}",
            async ([FromRoute] Guid jobId,
                [FromBody] UpdateJobRequest request,
                IJobManagementService jobService) =>
            {
                await jobService.UpdateJobAsync(jobId, request);
                return Results.Json(new { msg = "Job updated" });
            });

        routeBuilder.MapDelete("/{jobId}",
            async ([FromRoute] Guid jobId,
                IJobManagementService jobService) =>
            {
                await jobService.DeleteJobAsync(jobId);
                return Results.Json(new { msg = "Job deleted" });
            });

        routeBuilder.MapPost("/{jobId}/activate",
            async ([FromRoute] Guid jobId,
                IJobManagementService jobService) =>
            {
                await jobService.ActivateJobAsync(jobId);
                return Results.Json(new { msg = "Job activated" });
            });

        routeBuilder.MapPost("/{jobId}/deactivate",
            async ([FromRoute] Guid jobId,
                IJobManagementService jobService) =>
            {
                await jobService.DeactivateJobAsync(jobId);
                return Results.Json(new { msg = "Job deactivated" });
            });

        return routeBuilder;
    }
}