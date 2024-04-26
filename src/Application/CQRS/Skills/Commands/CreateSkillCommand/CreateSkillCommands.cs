
using ca.Application.Common.Interfaces;
using ca.Domain.Entities;

namespace ca.Application.CQRS.Skills.Commands.CreateSkillCommand;
public class CreateSkillCommands : IRequest<int>
{
    public string Title { get; set; } = string.Empty;
}










public class CreateSkillHandler : IRequestHandler<CreateSkillCommands, int>
{
    private readonly IApplicationDbContext _context;

    public CreateSkillHandler(IApplicationDbContext context) => _context = context;
        
    public async Task<int> Handle(CreateSkillCommands request, CancellationToken cancellationToken)
    {
        var entity = new Skill();
        entity.Title = request.Title;
        _context.Skills.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
