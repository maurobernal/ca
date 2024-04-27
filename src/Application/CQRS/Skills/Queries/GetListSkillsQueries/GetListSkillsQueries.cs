
using ca.Application.Common.Bases;
using ca.Application.Common.Interfaces;
using ca.Application.Common.Mappings;
using ca.Application.Common.Models;

namespace ca.Application.CQRS.Skills.Queries.GetListSkillsQueries;
public class GetListSkillsQueries : PaginatedBaseDTO, IRequest<PaginatedList<GetSkillQueriesDTO>>
{
}

public class GetListSkillsHandler( IMapper _mapper, IApplicationDbContext _context) : IRequestHandler<GetListSkillsQueries, PaginatedList<GetSkillQueriesDTO>>
{
    public async Task<PaginatedList<GetSkillQueriesDTO>> Handle(GetListSkillsQueries request, CancellationToken cancellationToken)
    {
        var res = await _context.Skills
            .ProjectTo<GetSkillQueriesDTO>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return res;
    }
}
