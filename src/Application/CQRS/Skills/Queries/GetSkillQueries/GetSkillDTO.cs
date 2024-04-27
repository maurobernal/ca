using ca.Domain.Entities;

namespace ca.Application.CQRS.Skills.Queries.GetSkillQueries;
public class GetSkillDTO
{
    public int Id{ get; set; }
    public string Title { get; set; } = string.Empty;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Skill, GetSkillDTO>();
        }
    }
}
