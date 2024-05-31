
using ca.Application.Common.Interfaces;

namespace ca.Application.CQRS.Skills.Commands.UpdateSkillComand;
public class UpdateSkillComands: IRequest<int>
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}





public class UpdateSkillHandler( IApplicationDbContext _context, ICacheService _cache) : IRequestHandler<UpdateSkillComands, int>
{
    public async Task<int> Handle(UpdateSkillComands request, CancellationToken cancellationToken)
    {      
        var entity =  await _context.Skills.FirstOrDefaultAsync(s => s.Id == request.Id);
        if (entity == null) throw new Common.Exceptions.ApiNotFoundException($"No existe el registro {request.Id}");

        entity.Title = request.Title;
        await _context.SaveChangesAsync(cancellationToken);
        await _cache.SetDataAsync($"skill:{entity.Id}", entity);

        return entity.Id; 
    }
}
