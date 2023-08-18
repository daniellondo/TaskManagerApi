namespace Services.Validators.CommandValidators
{
    using Domain.Dtos;
    using Domain.Entities;
    using FluentValidation;
    using Services.Validators.Shared;

    public class AddTaskCommandValidator : AbstractValidator<AddTaskCommand>
    {
        public AddTaskCommandValidator(ICommonValidators commonValidators)
        {
            RuleFor(payload => payload.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Must(id => !commonValidators.IsExistingEntityRow<TaskEntity>(x => x.Id == id))
                .WithMessage("You cannot add 2 tasks with the same id");

            RuleFor(payload => payload.Description).NotEmpty();
            RuleFor(payload => payload.IsCompleted).NotEmpty();
            RuleFor(payload => payload.CreationDate).NotEmpty();
        }
    }
}
