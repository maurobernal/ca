namespace ca.Application.CQRS.Skills.Commands.CreateSkillCommand;
public class CreateSkillValidator : AbstractValidator<CreateSkillCommands>
{
    public CreateSkillValidator()
    {
        RuleFor(f => f.Title).MaximumLength(100).MinimumLength(5).NotEmpty().NotNull();
    }
}
