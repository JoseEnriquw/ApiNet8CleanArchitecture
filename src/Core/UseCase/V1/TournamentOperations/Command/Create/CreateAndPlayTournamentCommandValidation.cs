using Core.Domain.Common;
using Core.Domain.Enums;
using FluentValidation;

namespace Core.UseCase.V1.TournamentOperations.Command.Create
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
                //.Must(x => EsPotenciaDeDos(x.Count))
                //.WithMessage(ErrorMessage.MUST_BE_POTENCY_NUMBER_OF_TWO)
                ;

            RuleFor(x => x.Gender)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(1)
                .WithMessage(string.Format(ErrorMessage.MUST_BE_A_POSITIVE_NUMBER, "{PropertyName}"))
                .Must(x => Enum.IsDefined(typeof(EGender), x))
                .WithMessage(ErrorMessage.MUST_BE_GENDER);
        }

        //private static bool EsPotenciaDeDos(int n)
        //{
        //    return n > 0 && (n & (n - 1)) == 0;
        //}
    }
}
