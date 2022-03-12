using BenchmarkDotNet.Attributes;

namespace Wavecell.Filters.Performance
{
    public class CsvSampleBenchmark
    {
        private readonly StrategyRulesEngine _rulesEngine;

        public CsvSampleBenchmark()
        {
            var rules = CsvStrategyRulesLoader.LoadRulesAsync("SampleData.csv", CancellationToken.None).GetAwaiter().GetResult();
            _rulesEngine = new StrategyRulesEngine();
            for (var i = 0; i < 1000; i++)
            {
                _rulesEngine.AddRules(rules);
            }
        }

        [Benchmark]
        public void FindRule_demoX1000_opsX1000()
        {
            for (var i = 0; i < 10000; i++)
            {
                var result1 = _rulesEngine.FindRule("AAA", "BBB", "CCC", "AAA");
                AssertRule(result1, 4, 10);
                
                var result2 = _rulesEngine.FindRule("AAA", "BBB", "CCC", "DDD");
                AssertRule(result2, 4, 10); 
                
                var result3 = _rulesEngine.FindRule("AAA", "AAA", "AAA", "AAA");
                AssertRule(result3, 2, 1);
                
                var result4 = _rulesEngine.FindRule("BBB", "BBB", "BBB", "BBB");
                AssertRule(result4, 6, 0);
                
                var result5 = _rulesEngine.FindRule("BBB", "CCC", "CCC", "CCC");
                AssertRule(result5, 3, 7);
            }
        }

        public void AssertRule(SearchRule rule, int ruleId, int outputValue)
        {
            if (rule.RuleId != ruleId || rule.OutputValue != outputValue)
                throw new InvalidOperationException();
        }
    }
}
