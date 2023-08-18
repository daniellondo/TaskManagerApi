namespace Services.Validators.CommandValidators
{
    using Domain.Dtos;
    using Domain.Entities;
    using FluentValidation;
    using Services.Validators.Shared;

    public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskCommandValidator(ICommonValidators commonValidators)
        {
            RuleFor(payload => payload.Id).NotNull();
            RuleFor(payload => payload.Id)
                .Cascade(CascadeMode.Stop)
                .Must(id => commonValidators
                .IsExistingEntityRow<TaskEntity>(t => t.Id == id))
                .WithMessage("Id must be valid.")
                .When(payload => payload != null);
        }
    }
}
