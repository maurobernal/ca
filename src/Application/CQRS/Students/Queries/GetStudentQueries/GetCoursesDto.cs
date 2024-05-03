using ca.Domain.Entities;

namespace ca.Application.CQRS.Students.Queries.GetStudentQueries;
public class GetCoursesDto
{
    public string Title { get; set; } = string.Empty;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Course, GetCoursesDto>();
        }
    }
}
