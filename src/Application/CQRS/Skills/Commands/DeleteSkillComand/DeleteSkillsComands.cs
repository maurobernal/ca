
using ca.Application.Common.Exceptions;
using ca.Application.Common.Interfaces;

namespace ca.Application.CQRS.Skills.Commands.DeleteSkillComand;
public class DeleteSkillsComands : IRequest<int>
{
    public int Id { get; set; }
}


public class DeleteSkillsHandler(IApplicationDbContext _context, ICacheService _cache) : IRequestHandler<DeleteSkillsComands, int>
{
    public async Task<int> Handle(DeleteSkillsComands request, CancellationToken cancellationToken)
    {
        var entity= await _context.Skills.AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == request.Id);

        if (entity == null) throw new Common.Exceptions.ApiNotFoundException($"La entidad no existe. Id:{request.Id}");

        var res= _context.Skills.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        await _cache.RemoveDataAsync($"skill:{entity.Id}");

        return request.Id;
    }
}
