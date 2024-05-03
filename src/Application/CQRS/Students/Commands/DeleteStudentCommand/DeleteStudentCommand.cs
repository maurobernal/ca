using ca.Application.Common.Exceptions;
using ca.Application.Common.Interfaces;

namespace ca.Application.CQRS.Students.Commands.DeleteStudentCommand;
public class DeleteStudentCommand : IRequest<int>
{
    internal int Id { get; set; }
    public int AssignId(int id) => Id = id;
}


public class DeleteStudentHandler(IApplicationDbContext _context) : IRequestHandler<DeleteStudentCommand, int>
{
    public async Task<int> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Students.FindAsync(request.Id);
        if (entity == null) throw new ApiNotFoundException($"el registro no existe:{request.Id}");

        _context.Students.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return request.Id;


    }
}
