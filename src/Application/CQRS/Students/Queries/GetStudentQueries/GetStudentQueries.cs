
using ca.Application.Common.Interfaces;
using ca.Application.Common.Exceptions;

namespace ca.Application.CQRS.Students.Queries.GetStudentQueries;
public class GetStudentQueries : IRequest<GetStudentDto>
{
    public int id { get; set; }

}


public class GetStudentQueriesHandler(IApplicationDbContext _context, IMapper _mapper, IVault _vault) : IRequestHandler<GetStudentQueries, GetStudentDto>
{
    public async Task<GetStudentDto> Handle(GetStudentQueries request, CancellationToken cancellationToken)
    {

        var res = _vault.GetKey("Motor1");
        var entity = await _context.Students
            .AsNoTracking()
            .Include(c => c.Courses)
            .ProjectTo<GetStudentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(r => r.Id == request.id);
        
        if (entity == null) throw new ApiNotFoundException($"el registro no existe:{request.id}");

        return entity;

    }
}
