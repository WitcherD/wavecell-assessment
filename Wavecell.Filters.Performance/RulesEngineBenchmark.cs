using BenchmarkDotNet.Attributes;

namespace Wavecell.Filters.Performance;

public class RulesEngineBenchmark
{
    private readonly RulesEngine _rulesEngine;
    
    public RulesEngineBenchmark()
    {
        _rulesEngine = new RulesEngine();
        for (var i = 1000; i < 2000; i++)
        {
            _rulesEngine.AddRule(new SearchRule{
                RuleId = i,
                Priority = i, 
                OutputValue = i, 
                Filters = new StrategyFilters
                {
                    Filter1 = $"AAA{i}",
                    Filter2 = $"BBB{i}",
                    Filter3 = $"CCC{i}",
                    Filter4 = $"DDD{i}",
                }});
        }
    }

    [Benchmark]
    public void FindRule_1000rules_100ops()
    {
        for (var i = 0; i < 100; i++)
        {
            _rulesEngine.FindRule(new StrategyFilters
            {
                Filter1 = "AAA",
                Filter2 = "BBB",
                Filter3 = "CCC",
                Filter4 = "DDD",
            });
        }
    }
}