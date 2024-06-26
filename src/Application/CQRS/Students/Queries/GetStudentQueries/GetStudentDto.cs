﻿using ca.Application.CQRS.TodoItems.Queries.GetTodoItemsWithPagination;
using ca.Domain.Entities;

namespace ca.Application.CQRS.Students.Queries.GetStudentQueries;
public class GetStudentDto
{
    public string fullName { get; set; } = string.Empty;
    
    public int Id { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }

    public int Day { get; set; }

    public List<GetCoursesDto> courses {get;set;} = new();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Student, GetStudentDto>()
                .ForMember(dest => dest.fullName, orig => orig.MapFrom(m => $"{m.FirstName} {m.LastName}"))
                .ForMember(dest => dest.Year, orig => orig.MapFrom(m => m.Birthdate.Year))
                .ForMember(dest => dest.Month, orig => orig.MapFrom(m => m.Birthdate.Month))
                .ForMember(dest => dest.Day, orig => orig.MapFrom(m => m.Birthdate.Day))
                ;   
        }
    }
}

