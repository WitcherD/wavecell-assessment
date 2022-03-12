namespace Wavecell.Filters;

public interface IRulesEngine
{
    SearchRule FindRule(IFilters filters);
}