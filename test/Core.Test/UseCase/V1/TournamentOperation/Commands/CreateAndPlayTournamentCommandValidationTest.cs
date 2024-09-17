using Core.Domain.Enums;
using Core.UseCase.V1.TournamentOperations.Commands.Create;
using FluentValidation.TestHelper;

namespace Core.Test.UseCase.V1.TournamentOperation.Commands
{
    public class CreateAndPlayTournamentCommandValidationTests
    {
        private readonly CreateAndPlayTournamentCommandValidation _validationRules;

        public CreateAndPlayTournamentCommandValidationTests()
        {
            _validationRules = new CreateAndPlayTournamentCommandValidation();
        }

        [Fact]
        public void PlayersId_Should_NotBeNull()
        {
            // Arrange
            var request = new CreateAndPlayTournamentCommand
            {
                PlayersId = null,
                Gender = 1
            };

            // Act
            var result = _validationRules.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PlayersId)
                  .WithErrorCode("NotNullValidator");
        }

        [Fact]
        public void PlayersId_Should_NotBeEmpty()
        {
            // Arrange
            var request = new CreateAndPlayTournamentCommand
            {
                PlayersId = [],
                Gender = 1
            };

            // Act
            var result = _validationRules.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PlayersId)
                  .WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void PlayersId_Should_BePowerOfTwo()
        {
            // Arrange
            var request = new CreateAndPlayTournamentCommand
            {
                PlayersId = [1, 2, 3],
                Gender = 1
            };

            // Act
            var result = _validationRules.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PlayersId)
                  .WithErrorCode("PredicateValidator");
        }

        [Fact]
        public void PlayersId_Should_ContainPositiveValues()
        {
            // Arrange
            var request = new CreateAndPlayTournamentCommand
            {
                PlayersId = [-1, 2, 4],
                Gender = 1
            };

            // Act
            var result = _validationRules.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor("PlayersId[0]")
                  .WithErrorCode("GreaterThanValidator");
        }

        [Fact]
        public void Gender_Should_BeGreaterThanZero()
        {
            // Arrange
            var request = new CreateAndPlayTournamentCommand
            {
                PlayersId = [1, 2, 4],
                Gender = 0
            };

            // Act
            var result = _validationRules.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Gender)
             .WithErrorCode("GreaterThanValidator");
        }

        [Fact]
        public void Gender_Should_BeValidEnumValue()
        {
            // Arrange
            var request = new CreateAndPlayTournamentCommand
            {
                PlayersId = [1, 2, 4],
                Gender = 99
            };

            // Act
            var result = _validationRules.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Gender)
                  .WithErrorCode("PredicateValidator");
        }

        [Fact]
        public void Validation_Should_Pass_WithValidData()
        {
            // Arrange
            var request = new CreateAndPlayTournamentCommand
            {
                PlayersId = [1, 2, 4, 8], 
                Gender = (int)EGender.Male 
            };

            // Act
            var result = _validationRules.TestValidate(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
