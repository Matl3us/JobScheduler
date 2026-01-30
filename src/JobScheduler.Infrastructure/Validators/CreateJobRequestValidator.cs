using FluentValidation;
using JobScheduler.Core.DTOs;

namespace JobScheduler.Infrastructure.Validators;

public class CreateJobRequestValidator : AbstractValidator<CreateJobRequest>
{
    public CreateJobRequestValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Job name is required")
            .MaximumLength(200)
            .WithMessage("Name is too long");

        RuleFor(c => c.Type)
            .NotEmpty()
            .WithMessage("Job type is required")
            .IsInEnum()
            .WithMessage("Incorrect value for job type");

        RuleFor(c => c.Data)
            .NotEmpty()
            .WithMessage("Job data is required")
            .MaximumLength(4000)
            .WithMessage("Job data is too long");

        RuleFor(c => c.CreatedBy)
            .NotEmpty()
            .WithMessage("Job created by field is required")
            .MaximumLength(100)
            .WithMessage("Created by field too long");
    }
}