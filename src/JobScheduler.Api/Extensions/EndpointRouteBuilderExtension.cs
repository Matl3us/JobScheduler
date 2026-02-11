using JobScheduler.Api.Endpoints;

namespace JobScheduler.Api.Extensions;

public static class EndpointRouteBuilderExtension
{
    public static void MapEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGroup("/jobs")
            .MapJobEndpoints()
            .MapSchedulesWithJobEndpoints();

        routeBuilder.MapGroup("/schedules")
            .MapScheduleEndpoints();
    }
}