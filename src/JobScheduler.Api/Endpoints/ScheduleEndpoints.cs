using JobScheduler.Core.DTOs;
using JobScheduler.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobScheduler.Api.Endpoints;

public static class ScheduleEndpoints
{
    public static void MapSchedulesWithJobEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/{jobId}/schedules",
            async ([FromRoute] Guid jobId,
                [FromBody] CreateJobScheduleRequest request,
                IJobScheduleManagementService scheduleService) =>
            {
                var schedule = await scheduleService.CreateScheduleAsync(jobId, request);
                return Results.Created($"/api/schedules/{schedule.Id}", schedule);
            });
        routeBuilder.MapGet("{jobId}/schedules",
            async ([FromRoute] Guid jobId,
                IJobScheduleManagementService scheduleService) =>
            {
                var schedules = await scheduleService.GetJobSchedulesAsync(jobId);
                return Results.Json(new { data = schedules });
            });
    }

    public static void MapScheduleEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/{scheduleId}",
            async ([FromRoute] Guid scheduleId,
                IJobScheduleManagementService scheduleService) =>
            {
                var schedule = await scheduleService.GetScheduleAsync(scheduleId);
                return Results.Json(new { data = schedule });
            });

        routeBuilder.MapPut("/{scheduleId}",
            async ([FromRoute] Guid scheduleId,
                [FromBody] UpdateJobScheduleRequest request,
                IJobScheduleManagementService scheduleService) =>
            {
                await scheduleService.UpdateScheduleAsync(scheduleId, request);
                return Results.Json(new { msg = "Schedule updated" });
            });

        routeBuilder.MapDelete("/{scheduleId}",
            async ([FromRoute] Guid scheduleId,
                IJobScheduleManagementService scheduleService) =>
            {
                await scheduleService.DeleteScheduleAsync(scheduleId);
                return Results.Json(new { msg = "Schedule deleted" });
            });
    }
}