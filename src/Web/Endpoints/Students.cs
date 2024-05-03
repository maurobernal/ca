using ca.Application.CQRS.Students.Commands.CreateStudentCommand;
using ca.Application.CQRS.Students.Commands.DeleteStudentCommand;
using ca.Application.CQRS.Students.Commands.PutStudentCommand;
using ca.Application.CQRS.Students.Queries.GetStudentQueries;
using ca.Application.CQRS.TodoItems.Commands.CreateTodoItem;
using Microsoft.AspNetCore.Authorization;

namespace ca.Web.Endpoints;

public class Students : EndpointGroupBase
{

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetStudent)
            .MapPost(PostStudent)
            .MapPut(PutStudent, "{id}")
            .MapDelete(DeleteStudent, "{id}");
    }


    [AllowAnonymous]
    public Task<GetStudentDto> GetStudent(ISender sender, [AsParameters] GetStudentQueries query)
    {
        return sender.Send(query);
    }


    [AllowAnonymous]
    public Task<int> PostStudent(ISender sender, CreateStudentCommand query)
    {
        return sender.Send(query);
    }

    [AllowAnonymous]
    public async Task<IResult> PutStudent(ISender sender, int id, PutStudentCommand command)
    {
        if(id!= command.id ) return Results.BadRequest();
        return Results.Ok(await sender.Send(command));
    }

    [AllowAnonymous]
    public async Task<IResult> DeleteStudent(ISender sender, int id)
    {
        var command = new DeleteStudentCommand();
        command.AssignId(id);
        return Results.Ok(await sender.Send(command));
    }


}
