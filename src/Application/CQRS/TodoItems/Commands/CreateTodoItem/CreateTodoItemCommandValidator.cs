namespace ca.Application.CQRS.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(v => v.Title)
            .Must( v => 
            {
                if (v == null) return false;
                if (v.Contains("Tit:")) return true;
                return false;
            }   ).WithMessage("No contiene la palabra Tit:")
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.ListId).GreaterThan(10);
    }
}
