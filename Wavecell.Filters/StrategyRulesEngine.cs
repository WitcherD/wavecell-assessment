using Microsoft.Extensions.Caching.Memory;

namespace Wavecell.Filters
{
    public class StrategyRulesEngine : RulesEngine
    {
        private readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions { SizeLimit = 10000000 }); // around 10mb

        public SearchRule FindRule(string filter1, string filter2, string filter3, string filter4)
        {
            var filters = new StrategyFilters
            {
                Filter1 = string.Intern(filter1),
                Filter2 = string.Intern(filter2),
                Filter3 = string.Intern(filter3),
                Filter4 = string.Intern(filter4)
            };
            return _cache.GetOrCreate(filters, entry =>
            {
                entry.Size = 1000;
                return FindRule(filters);
            });
        }
    }
}
