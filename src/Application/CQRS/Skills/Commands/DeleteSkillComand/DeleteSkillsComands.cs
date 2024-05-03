
using ca.Application.Common.Exceptions;
using ca.Application.Common.Interfaces;

namespace ca.Application.CQRS.Skills.Commands.DeleteSkillComand;
public class DeleteSkillsComands : IRequest<int>
{
    public int Id { get; set; }
}


public class DeleteSkillsHandler : IRequestHandler<DeleteSkillsComands, int>
{
    private readonly IApplicationDbContext _context;

    public DeleteSkillsHandler(IApplicationDbContext context) => _context = context;
    
    public async Task<int> Handle(DeleteSkillsComands request, CancellationToken cancellationToken)
    {
        var ent= await _context.Skills.AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == request.Id);

        if (ent == null) throw new Common.Exceptions.ApiNotFoundException($"La entidad no existe. Id:{request.Id}");

        var res= _context.Skills.Remove(ent);
        await _context.SaveChangesAsync(cancellationToken);
        return request.Id;
    }
}
