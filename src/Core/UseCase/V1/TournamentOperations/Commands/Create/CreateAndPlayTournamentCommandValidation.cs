using Core.Domain.Common;
using Core.Domain.Enums;
using FluentValidation;

namespace Core.UseCase.V1.TournamentOperations.Commands.Create
{
    public class CreateAndPlayTournamentCommandValidation : AbstractValidator<CreateAndPlayTournamentCommand>
    {

        public CreateAndPlayTournamentCommandValidation()
        {
            RuleFor(x => x.PlayersId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(string.Format(ErrorMessage.NULL_VALUE, "{PropertyName}"))
                .NotEmpty()
                .WithMessage(string.Format(ErrorMessage.EMPTY_VALUE, "{PropertyName}"))
                .Must(x => EsPotenciaDeDos(x.Count))
                .WithMessage(ErrorMessage.MUST_BE_POTENCY_NUMBER_OF_TWO)
                ;

            RuleForEach(x => x.PlayersId)
                .ChildRules(child => child.RuleFor(x => x)
                .Cascade(CascadeMode.Stop)
                    .GreaterThan(0)
                    .WithMessage(string.Format(ErrorMessage.MUST_BE_A_POSITIVE_NUMBER, "{PropertyName}")));

            RuleFor(x => x.Gender)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithMessage(string.Format(ErrorMessage.MUST_BE_A_POSITIVE_NUMBER, "{PropertyName}"))
                .Must(x => Enum.IsDefined(typeof(EGender), x))
                .WithMessage(ErrorMessage.MUST_BE_GENDER);
        }

        private static bool EsPotenciaDeDos(int n)
        {
            return n > 0 && (n & n - 1) == 0;
        }
    }
}
