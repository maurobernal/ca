using ca.Domain.Entities;

namespace ca.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Student> Students { get; }

    DbSet<Course> Courses {get;}

    DbSet<Skill> Skills{ get; }

    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
