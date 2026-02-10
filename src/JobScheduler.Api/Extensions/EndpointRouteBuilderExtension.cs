using JobScheduler.Core.DTOs;
using JobScheduler.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobScheduler.Api.Extensions;

public static class EndpointRouteBuilderExtension
{
    public static void MapEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/jobs",
            async ([FromBody] CreateJobRequest request,
                IJobManagementService jobService) =>
            {
                var job = await jobService.CreateJobAsync(request);
                return Results.Created($"/jobs/{job.Id}", job);
            });

        routeBuilder.MapGet("/jobs",
            async (IJobManagementService jobService) =>
            {
                var jobs = await jobService.GetAllJobsAsync();
                return Results.Json(new { data = jobs }, statusCode: 200);
            });

        routeBuilder.MapGet("/jobs/{jobId}",
            async ([FromRoute] Guid jobId,
                IJobManagementService jobService) =>
            {
                var job = await jobService.GetJobAsync(jobId);
                return Results.Json(new { data = job }, statusCode: 200);
            });

        routeBuilder.MapPut("/jobs/{jobId}",
            async ([FromRoute] Guid jobId,
                [FromBody] UpdateJobRequest request,
                IJobManagementService jobService) =>
            {
                await jobService.UpdateJobAsync(jobId, request);
                return Results.Json(new { msg = "Job updated" }, statusCode: 200);
            });

        routeBuilder.MapDelete("/jobs/{jobId}",
            async ([FromRoute] Guid jobId,
                IJobManagementService jobService) =>
            {
                await jobService.DeleteJobAsync(jobId);
                return Results.Json(new { msg = "Job deleted" }, statusCode: 200);
            });

        routeBuilder.MapPost("/jobs/{jobId}/activate",
            async ([FromRoute] Guid jobId,
                IJobManagementService jobService) =>
            {
                await jobService.ActivateJobAsync(jobId);
                return Results.Json(new { msg = "Job activated" }, statusCode: 200);
            });

        routeBuilder.MapPost("/jobs/{jobId}/deactivate",
            async ([FromRoute] Guid jobId,
                IJobManagementService jobService) =>
            {
                await jobService.DeactivateJobAsync(jobId);
                return Results.Json(new { msg = "Job deactivated" }, statusCode: 200);
            });
    }
}