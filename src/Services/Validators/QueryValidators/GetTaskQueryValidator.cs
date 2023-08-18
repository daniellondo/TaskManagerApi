namespace Services.Validators.QueryValidators
{
    using Domain.Dtos;
    using Domain.Entities;
    using FluentValidation;
    using Services.Validators.Shared;

    public class GetTaskQueryValidator : AbstractValidator<GetTaskQuery>
    {
        public GetTaskQueryValidator(ICommonValidators commonValidators)
        {
            RuleFor(payload => payload.Id)
                .Cascade(CascadeMode.Stop)
                .Must(id => commonValidators
                .IsExistingEntityRow<TaskEntity>(t => t.Id == id))
                .WithMessage("Id must be valid.")
                .When(payload => payload != null && payload.Id is not null);
        }
    }
}
