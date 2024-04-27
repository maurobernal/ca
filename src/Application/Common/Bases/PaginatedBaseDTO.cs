namespace ca.Application.Common.Bases;
public abstract class PaginatedBaseDTO
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 3;
}
