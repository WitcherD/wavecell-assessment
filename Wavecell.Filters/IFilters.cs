namespace Wavecell.Filters;

public interface IFilters
{
    public bool Match(IFilters filters);
}