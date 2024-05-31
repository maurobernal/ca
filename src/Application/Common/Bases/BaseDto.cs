namespace ca.Application.Common.Bases;
public class BaseDto
{
    public int Id { get; set; }
    public void AssignId(int id) => Id = id;
}
