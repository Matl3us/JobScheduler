using Cronos;
using FluentValidation;
using JobScheduler.Core.DTOs;
using JobScheduler.Core.Enums;

namespace JobScheduler.Infrastructure.Validators;

public class CreateJobScheduleRequestValidator : AbstractValidator<CreateJobScheduleRequest>
{
    public CreateJobScheduleRequestValidator()
    {
        RuleFor(c => c.JobId)
            .NotEmpty()
            .WithMessage("Job schedule must be linked to the job by id");

        RuleFor(c => c.Type)
            .NotEmpty()
            .WithMessage("Job schedule type is required")
            .IsInEnum()
            .WithMessage("Incorrect value for job schedule type");

        RuleFor(c => c.CronExpression)
            .NotEmpty()
            .WithMessage("Cron expression is required")
            .When(c => c.Type == JobScheduleType.Cron)
            .Custom((ce, context) =>
            {
                if (ce is not null && !CronExpression.TryParse(ce, out _))
                    context.AddFailure("Invalid format of the cron expression");
            })
            .When(c => c.Type == JobScheduleType.Cron);

        RuleFor(c => c.TimeZone)
            .NotEmpty()
            .WithMessage("Time zone is required")
            .Custom((tz, context) =>
            {
                try
                {
                    TimeZoneInfo.FindSystemTimeZoneById(tz);
                }
                catch (TimeZoneNotFoundException)
                {
                    context.AddFailure("Invalid format of the time zone");
                }
            });
    }
}