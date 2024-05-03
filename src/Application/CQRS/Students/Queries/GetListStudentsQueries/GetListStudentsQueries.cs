using ca.Application.Common.Bases;
using ca.Application.Common.Interfaces;
using ca.Application.Common.Mappings;
using ca.Application.Common.Models;

namespace ca.Application.CQRS.Students.Queries.GetStudentsQueries;
public class GetListStudentsQueries : PaginatedBaseDTO, IRequest<PaginatedList<GetStudentDtoOfList>>
{

}

public class GetListStudentHandler(IApplicationDbContext _context, IMapper _mapper) : IRequestHandler<GetListStudentsQueries, PaginatedList<GetStudentDtoOfList>>
{
    public async Task<PaginatedList<GetStudentDtoOfList>> Handle(GetListStudentsQueries request, CancellationToken cancellationToken)
    {
        var res = await _context.Students
            .ProjectTo<GetStudentDtoOfList>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        return res;
        
    }
}
