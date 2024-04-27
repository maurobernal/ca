namespace ca.Application.CQRS.Skills.Commands.UpdateSkillComand;
public class UpdateSkillValidator : AbstractValidator<UpdateSkillComands>
{
    public UpdateSkillValidator()
    {

        RuleFor(f => f.Title).NotNull().NotEmpty().MaximumLength(100).MinimumLength(5);
    }
}
