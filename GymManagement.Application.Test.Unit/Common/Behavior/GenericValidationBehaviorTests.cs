using ErrorOr;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GymManagement.Application.Common.Behavior;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Domains.Gyms;
using MediatR;
using NSubstitute;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Gyms;
using TestCommon.Gyms.commands;

namespace GymManagement.Application.Test.Unit.Common.Behavior
{
    public class GenericValidationBehaviorTests
    {

        private readonly IValidator<CreateGymCommand> _mockValidator;
        private readonly RequestHandlerDelegate<ErrorOr<Gym>> _mockNextBehavior;
        private readonly GenericValidationBehavior<CreateGymCommand, ErrorOr<Gym>> _sut;


        public GenericValidationBehaviorTests()
        {

            // Create next behavior (mock)

            _mockNextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<Gym>>>();

            // Create validator (mock)

            _mockValidator = Substitute.For<IValidator<CreateGymCommand>>();

            // Create validation behavior (sut)
            _sut = new GenericValidationBehavior<CreateGymCommand,ErrorOr<Gym>>(_mockValidator);
        }


        [Fact]
        public async Task InvokeBehavior_ShouldInvokeNextBehavior_WhenValidationResultIsValid()
        {

            //Arrange

            //Create request command
            var createGymRequest = GymCommandFactory.CreateCreateGymCommand();

            _mockValidator.ValidateAsync(createGymRequest).Returns(new ValidationResult());


            var gym = GymFactory.CreateGym();

            _mockNextBehavior.Invoke().Returns(gym);


            //Act

            var result = await _sut.Handle(createGymRequest, _mockNextBehavior, Arg.Any<CancellationToken>());

            //Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo(gym);

        }


        [Fact]
        public async Task InvokeBehavior_ShouldReturnsListOfErrors_WhenValidationResultIsNotValid()
        {

            //Arrange

            //Create request command
            var createGymRequest = GymCommandFactory.CreateCreateGymCommand();

            List<ValidationFailure> validationFailures = [new(propertyName: "Name", errorMessage: "Must has less than 200 charecters")];


            _mockValidator.ValidateAsync(createGymRequest).Returns(new ValidationResult(validationFailures));


            //Act

            var result = await _sut.Handle(createGymRequest, _mockNextBehavior, Arg.Any<CancellationToken>());

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be("Name");
            result.FirstError.Description.Should().Be("Must has less than 200 charecters");

        }

    }
}
