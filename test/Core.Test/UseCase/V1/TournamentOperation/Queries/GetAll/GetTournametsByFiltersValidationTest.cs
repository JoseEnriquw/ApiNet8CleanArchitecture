using Core.Domain.Enums;
using Core.UseCase.V1.TournamentOperations.Queries.GetAll;
using FluentValidation.TestHelper;

namespace Core.Test.UseCase.V1.TournamentOperation.Queries.GetAll
{
    public class GetTournametsByFiltersValidationTest
    {
        private readonly GetTournametsByFiltersValidation _validationRules;

        public GetTournametsByFiltersValidationTest()
        {
            _validationRules = new GetTournametsByFiltersValidation();
        }

        [Fact]
        public void Gender_Should_BeValidEnum_WhenDefined()
        {
            // Arrange
            var request = new GetTournametsByFilters
            {
                Gender = (int)EGender.Male
            };

            // Act
            var result = _validationRules.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Gender);
        }

        [Fact]
        public void Gender_Should_ReturnError_WhenNotAValidEnumValue()
        {
            // Arrange
            var request = new GetTournametsByFilters
            {
                Gender = 99
            };

            // Act
            var result = _validationRules.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Gender)
                  .WithErrorCode("PredicateValidator");
        }

        [Fact]
        public void Gender_Should_NotReturnError_WhenNull()
        {
            // Arrange
            var request = new GetTournametsByFilters
            {
                Gender = null // Gender is null
            };

            // Act
            var result = _validationRules.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Gender);
        }
    }

}
