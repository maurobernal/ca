
using ca.Application.Common.Interfaces;
using ca.Application.Common.Exceptions;

namespace ca.Application.CQRS.Students.Queries.GetStudentQueries;
public class GetStudentQueries : IRequest<GetStudentDto>
{
    public int id { get; set; }

}


public class GetStudentQueriesHandler : IRequestHandler<GetStudentQueries, GetStudentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetStudentQueriesHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetStudentDto> Handle(GetStudentQueries request, CancellationToken cancellationToken)
    {
        var student2 = await _context.Students.AsNoTracking()
           .SingleOrDefaultAsync(s => s.Id == request.id);

        int studentId = 0;
        if (student2 != null) studentId = student2.Id;


        return new GetStudentDto();

    }
}
