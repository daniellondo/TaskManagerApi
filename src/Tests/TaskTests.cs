namespace Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoFixture;
    using Domain.Dtos;
    using Domain.Entities;
    using FluentValidation.TestHelper;
    using NSubstitute;
    using Services.Validators.CommandValidators;
    using Services.Validators.Shared;
    using Xunit;

    public class TaskTests
    {
        public static readonly Fixture _fixture = new();
        private readonly AddTaskCommandValidator _addTaskCommandValidator;
        private readonly ICommonValidators _commonValidator;
        public TaskTests()
        {
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _commonValidator = Substitute.For<ICommonValidators>();
            _addTaskCommandValidator = new AddTaskCommandValidator(_commonValidator);
        }

        [Fact]
        public async Task AddTaskCommandValidator_Adding_Same_Id()
        {
            // Arrange
            var fixture = _fixture.Build<TaskEntity>()
                    .With(p => p.Id, 1)
                    .Without(p => p.Description)
                    .Without(p => p.CreationDate)
                    .Without(p => p.IsCompleted)
                    .Create();
            _commonValidator.ConfigureTestData(new List<TaskEntity> { fixture });

            var _request = new AddTaskCommand
            {
                Id = 1
            };

            // Act
            var result = await _addTaskCommandValidator.TestValidateAsync(_request);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Id);
            result.ShouldHaveValidationErrorFor(r => r.Description);
            result.ShouldHaveValidationErrorFor(r => r.CreationDate);
            result.ShouldHaveValidationErrorFor(r => r.IsCompleted);
        }

    }
}
