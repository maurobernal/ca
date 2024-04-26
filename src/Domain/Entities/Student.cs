namespace ca.Domain.Entities;
public class Student: BaseAuditableEntity 
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Identified { get; set; }
    public DateOnly Birthdate { get; set;} = new DateOnly();

    public int CourseId { get; set; }
    public Course? Course { get; set; }
    public virtual IList<Skill> Skills{ get; set; } = new List<Skill>();
    
    
}
