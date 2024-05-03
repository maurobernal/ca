
using ca.Application.Common.Exceptions;
using ca.Application.Common.Interfaces;

namespace ca.Application.CQRS.Students.Commands.PutStudentCommand;
public class PutStudentCommand : IRequest<int>
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public string surname { get; set; } = string.Empty;
    public int year { get; set; }
    public int month { get; set; }
    public int day { get; set; }
}

public class PutStudentHandler(IApplicationDbContext _context) : 
    IRequestHandler<PutStudentCommand, int>
{
    public async Task<int> Handle(PutStudentCommand request, CancellationToken cancellationToken)
    {
        // validate day/month/year is date
        DateOnly dateOut;
        if (!DateOnly.TryParse($"{request.year}-{request.month}-{request.day}", out dateOut))
            throw new ApiValidationException($"los campos enviados no corresponden a una fecha: {request.year}-{request.month}-{request.day}");

        var entity = await _context.Students.FindAsync(request.id);
        if (entity == null) throw new ApiNotFoundException($"no existe el registro:{request.id}");

        // Update values
        entity.FirstName = request.name;
        entity.LastName = request.surname;
        entity.Birthdate = dateOut;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;


    }
}
