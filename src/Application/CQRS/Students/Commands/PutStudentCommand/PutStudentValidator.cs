
using ca.Application.CQRS.Students.Commands.PutStudentCommand;

public class PutStudentValidator : AbstractValidator<PutStudentCommand>
{
    public PutStudentValidator()
    {
        RuleFor(f => f.name).NotEmpty().NotNull().MinimumLength(10).MaximumLength(200);
        RuleFor(f => f.surname).NotEmpty().NotNull().MinimumLength(10).MaximumLength(200);
        RuleFor(f => f.year).GreaterThanOrEqualTo(1980).LessThanOrEqualTo(2099);
    }
}
