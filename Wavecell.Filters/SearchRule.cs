namespace Wavecell.Filters;

public class SearchRule
{
    public int RuleId { get; set; }
    public int Priority { get; set; }
    public IFilters Filters { get; set; }
    public int? OutputValue { get; set; }
}