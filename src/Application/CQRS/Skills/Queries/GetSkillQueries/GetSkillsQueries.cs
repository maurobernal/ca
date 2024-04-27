
using AutoMapper.QueryableExtensions;
using ca.Application.Common.Exceptions;
using ca.Application.Common.Interfaces;

namespace ca.Application.CQRS.Skills.Queries.GetSkillQueries;
public class GetSkillsQueries : IRequest<GetSkillDTO>
{
    internal int Id { get; set; }

    public void AssignId(int id ) => Id = id;
}


public class GetSkillsHandler(IMapper _mapper, IApplicationDbContext _context) : IRequestHandler<GetSkillsQueries, GetSkillDTO>
{
  
    public async Task<GetSkillDTO> Handle(GetSkillsQueries request, CancellationToken cancellationToken)
    {
        var res = await _context.Skills
            .ProjectTo<GetSkillDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(s => s.Id == request.Id);
        
        if (res == null) throw new Common.Exceptions.NotFoundException($"No existe el registro {request.Id}");

        return res;
            
    }
}
