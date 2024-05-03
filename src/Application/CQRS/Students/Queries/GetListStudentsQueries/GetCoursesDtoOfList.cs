
using ca.Domain.Entities;

namespace ca.Application.CQRS.Students.Queries.GetStudentsQueries;
public class GetCoursesDtoOfList
{
    public string Title { get; set; } = string.Empty;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Course, GetCoursesDtoOfList>();
        }
    }
}

