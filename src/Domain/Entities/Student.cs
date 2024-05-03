namespace ca.Domain.Entities;
public class Student: BaseAuditableEntity 
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Identified { get; set; }
    public DateOnly Birthdate { get; set;} = new DateOnly();
    public virtual IList<Skill> Skills{ get; set; } = new List<Skill>();
    public virtual IList<Course> Courses{ get; set; } = new List<Course>();

}
