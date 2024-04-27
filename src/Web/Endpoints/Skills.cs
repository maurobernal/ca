using ca.Application.Common.Models;
using ca.Application.CQRS.Skills.Commands.CreateSkillCommand;
using ca.Application.CQRS.Skills.Commands.DeleteSkillComand;
using ca.Application.CQRS.Skills.Commands.UpdateSkillComand;
using ca.Application.CQRS.Skills.Queries.GetListSkillsQueries;
using ca.Application.CQRS.Skills.Queries.GetSkillQueries;
using ca.Application.CQRS.Students.Queries.GetStudentQueries;
using ca.Application.CQRS.TodoItems.Queries.GetTodoItemsWithPagination;
using Microsoft.AspNetCore.Authorization;


namespace ca.Web.Endpoints;

public class Skills : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetSkills,"{id}")
            .MapGet(GetListSkills)
            .MapPost(PostSkill)
            .MapPut(PutSkills, "{id}")
            .MapDelete(DeleteSkills, "{id}");
    }

    [AllowAnonymous]
    public Task<GetSkillDTO> GetSkills(ISender sender, int id)
    {
        var command = new GetSkillsQueries();
        command.AssignId(id);

        return sender.Send(command);
    }

    [AllowAnonymous]
    public async Task<PaginatedList<GetSkillQueriesDTO>> GetListSkills(ISender sender, [AsParameters] GetListSkillsQueries query)
    {
        
        var res = await sender.Send(query);
        return res;
    }

    [AllowAnonymous]
    public Task<int> PostSkill(ISender sender, CreateSkillCommands command)
    {
        return sender.Send(command);
    }

    [AllowAnonymous]
    public async Task<IResult> PutSkills(ISender sender,int id, UpdateSkillComands command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.Ok(command.Id);
    }

    [AllowAnonymous]
    public async Task<IResult> DeleteSkills(ISender sender, int id)
    {
        await sender.Send(new DeleteSkillsComands() { Id=id});
        return Results.Ok(id);
    }

    
}
