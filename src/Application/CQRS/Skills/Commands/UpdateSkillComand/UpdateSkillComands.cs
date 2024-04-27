
using ca.Application.Common.Interfaces;

namespace ca.Application.CQRS.Skills.Commands.UpdateSkillComand;
public class UpdateSkillComands: IRequest<int>
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}





public class UpdateSkillHandler : IRequestHandler<UpdateSkillComands, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateSkillHandler(IMapper mapper, IApplicationDbContext context)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<int> Handle(UpdateSkillComands request, CancellationToken cancellationToken)
    {      
        var entity =  await _context.Skills.FirstOrDefaultAsync(s => s.Id == request.Id);
        if (entity == null) throw new Common.Exceptions.NotFoundException($"No existe el registro {request.Id}");

        entity.Title = request.Title;
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id; 
    }
}
