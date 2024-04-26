namespace ca.Application.CQRS.Students.Commands.CreateStudentCommand;
public class CreateStudentValidators : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentValidators()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(200)
            .MinimumLength(10)
            .WithMessage("Name length between 10 and 200");

        RuleFor(p => p.Surname)
           .NotEmpty()
           .MaximumLength(200)
           .MinimumLength(10)
           .WithMessage("Name length between 10 and 200");
        RuleFor(p => p.year).Must(y => y >= 1980 && y <= 2099).WithMessage("Year must between 1980 and 2099");
        RuleFor(p => p.month).Must(y => y >= 1 && y <= 12).WithMessage("Year must between 1980 and 2099");
        RuleFor(p => p.day).Must(y => y >= 1 && y <= 31).WithMessage("Year must between 1980 and 2099");


    }

}
