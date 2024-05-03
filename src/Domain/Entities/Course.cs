namespace ca.Domain.Entities;
public class Course : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;

    public virtual IList<Student> Students { get; set; } = new List<Student>();

}
