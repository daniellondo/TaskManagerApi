namespace Services.Validators.CommandValidators
{
    using Domain.Dtos;
    using Domain.Entities;
    using FluentValidation;
    using Services.Validators.Shared;

    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator(ICommonValidators commonValidators)
        {
            RuleFor(payload => payload.Id).NotNull();
            RuleFor(payload => payload.Description).NotNull();
            RuleFor(payload => payload.IsCompleted).NotNull();
            RuleFor(payload => payload.CreationDate).NotNull();
            RuleFor(payload => payload.Id)
                .Cascade(CascadeMode.Stop)
                .Must(id => commonValidators
                .IsExistingEntityRow<TaskEntity>(t => t.Id == id))
                .WithMessage("Id must be valid.")
                .When(payload => payload != null);


        }
    }
}
