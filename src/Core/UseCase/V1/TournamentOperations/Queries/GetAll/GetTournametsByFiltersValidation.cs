using Core.Domain.Common;
using Core.Domain.Enums;
using FluentValidation;

namespace Core.UseCase.V1.TournamentOperations.Queries.GetAll
{
    public class GetTournametsByFiltersValidation : AbstractValidator<GetTournametsByFilters>
    {
        public GetTournametsByFiltersValidation()
        {
            RuleFor(x => x.Gender)
                .Must(x => Enum.IsDefined(typeof(EGender), x!))
                .When(x => x.Gender.HasValue)
                .WithMessage(ErrorMessage.MUST_BE_GENDER);
        }
    }
}
