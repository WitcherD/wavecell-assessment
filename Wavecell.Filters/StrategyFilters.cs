namespace Wavecell.Filters;

public record StrategyFilters : IFilters
{
    public string Filter1 { get; set; }
    public string Filter2 { get; set; }
    public string Filter3 { get; set; }
    public string Filter4 { get; set; }

    public bool Match(IFilters obj)
    {
        var filters = obj as StrategyFilters;
        if (filters == null) return false;

        return (Filter1 == default || filters.Filter1 == Filter1) &&
               (Filter2 == default || filters.Filter2 == Filter2) &&
               (Filter3 == default || filters.Filter3 == Filter3) &&
               (Filter4 == default || filters.Filter4 == Filter4);
    }
}