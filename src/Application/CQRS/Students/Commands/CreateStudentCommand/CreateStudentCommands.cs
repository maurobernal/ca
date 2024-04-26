
using ca.Application.Common.Interfaces;
using ca.Domain.Entities;

namespace ca.Application.CQRS.Students.Commands.CreateStudentCommand;
public class CreateStudentCommand: IRequest<int>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int year { get; set; }
    public int month { get; set; }
    public int day { get; set; }


}


public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateStudentHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Student();

        // Mapeo manual
        entity.Birthdate = new DateOnly(request.year, request.month, request.day);
        entity.FirstName = request.Name ?? "";
        entity.LastName = request.Surname ?? "";

        _context.Students.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);


        return entity.Id;
    }
}


