
using AutoMapper.QueryableExtensions;
using ca.Application.Common.Bases;
using ca.Application.Common.Exceptions;
using ca.Application.Common.Interfaces;
using ca.Domain.Entities;

namespace ca.Application.CQRS.Skills.Queries.GetSkillQueries;
public class GetSkillsQueries : BaseDto, IRequest<GetSkillDTO>
{}


public class GetSkillsHandler(IMapper _mapper, IApplicationDbContext _context) : IRequestHandler<GetSkillsQueries, GetSkillDTO>
{
  
    public async Task<GetSkillDTO> Handle(GetSkillsQueries request, CancellationToken cancellationToken)
    {
       // var resCache = await _cache.GetDataAsync<Skill>($"skill:{request.Id}");
        //if(resCache == null) { 

        var res = await _context.Skills
            .ProjectTo<GetSkillDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(s => s.Id == request.Id);
            if (res == null) throw new Common.Exceptions.ApiNotFoundException($"No existe el registro {request.Id}");
        return res;
        //}
        //return _mapper.Map<GetSkillDTO>(resCache);
            
    }
}
