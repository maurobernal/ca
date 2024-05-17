using ca.Application.CQRS.Skills.Queries.GetSkillQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ca.Web.Endpoints;

public class Test : EndpointGroupBase
{

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetHostName);
    }

    [AllowAnonymous]
    public IResult GetHostName()
     => Results.Ok(System.Net.Dns.GetHostName());




}
