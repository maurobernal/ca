using ca.Application.Common.Models;
using ca.Application.CQRS.Students.Commands.CreateStudentCommand;
using ca.Application.CQRS.Students.Commands.DeleteStudentCommand;
using ca.Application.CQRS.Students.Commands.PutStudentCommand;
using ca.Application.CQRS.Students.Queries.GetStudentQueries;
using ca.Application.CQRS.Students.Queries.GetStudentsQueries;

namespace ca.Web.Endpoints;
public class Students : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetStudent,"{id}")
            .MapGet(GetListStudent)
            .MapPost(PostStudent)
            .MapPut(PutStudent, "{id}")
            .MapDelete(DeleteStudent, "{id}");
    }

    public Task<GetStudentDto> GetStudent(ISender sender, int id)
    {
        var query = new GetStudentQueries();
        query.id = id;
        return sender.Send(query);
    }

    public async Task<PaginatedList<GetStudentDtoOfList>> GetListStudent(ISender sender, [AsParameters] GetListStudentsQueries query )
    {        
        var res =  await sender.Send(query);
        return res;
    }

    public Task<int> PostStudent(ISender sender, CreateStudentCommand query)
    {
        return sender.Send(query);
    }

    public async Task<IResult> PutStudent(ISender sender, int id, PutStudentCommand command)
    {
        if(id!= command.id ) return Results.BadRequest();
        return Results.Ok(await sender.Send(command));
    }

    public async Task<IResult> DeleteStudent(ISender sender, int id)
    {
        var command = new DeleteStudentCommand();
        command.AssignId(id);
        return Results.Ok(await sender.Send(command));
    }

}
