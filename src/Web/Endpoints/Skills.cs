using ca.Application.CQRS.Skills.Commands.CreateSkillCommand;
using Microsoft.AspNetCore.Authorization;

namespace ca.Web.Endpoints;

public class Skills : EndpointGroupBase
{

    [AllowAnonymous]
    public Task<int> PostSkill(ISender sender, CreateSkillCommands command)
    {
        return sender.Send(command);
    }

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(PostSkill);
    }
}
