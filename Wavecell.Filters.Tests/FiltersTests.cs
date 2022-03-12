using NUnit.Framework;

namespace Wavecell.Filters.Tests
{
    public class FiltersTests
    {
        private readonly RulesEngine _rulesEngine;

        public FiltersTests()
        {
            _rulesEngine = new RulesEngine();
            
            _rulesEngine.AddRule(new SearchRule
            {
                RuleId = 1,
                Priority = 1,
                OutputValue = 1,
                Filters = new StrategyFilters
                {
                    Filter1 = "DDD",
                    Filter2 = null,
                    Filter3 = null,
                    Filter4 = null,
                }
            });

            _rulesEngine.AddRule(new SearchRule
            {
                RuleId = 2,
                Priority = 2,
                OutputValue = 2,
                Filters = new StrategyFilters
                {
                    Filter1 = "AAA",
                    Filter2 = "BBB",
                    Filter3 = "CCC",
                    Filter4 = null,
                }
            });

            _rulesEngine.AddRule(new SearchRule
            {
                RuleId = 3,
                Priority = 1,
                OutputValue = 3,
                Filters = new StrategyFilters
                {
                    Filter1 = "AAA",
                    Filter2 = "BBB",
                    Filter3 = "CCC",
                    Filter4 = "DDD",
                }
            });

            _rulesEngine.AddRule(new SearchRule
            {
                RuleId = 4,
                Priority = 4,
                OutputValue = 4,
                Filters = new StrategyFilters
                {
                    Filter1 = "AAA",
                    Filter2 = "BBB",
                    Filter3 = "CCC",
                    Filter4 = "AAA",
                }
            });

            _rulesEngine.AddRule(new SearchRule
            {
                RuleId = 5,
                Priority = 0,
                OutputValue = 5,
                Filters = new StrategyFilters
                {
                    Filter1 = null,
                    Filter2 = null,
                    Filter3 = null,
                    Filter4 = null,
                }
            });
        }

        [Test]
        [TestCase("AAA", "BBB", "CCC", "DDD", 2)]
        [TestCase("AAA", "BBB", "CCC", "AAA", 4)]
        [TestCase("DDD", "BBB", "CCC", "AAA", 1)]
        [TestCase("XXX", "DDD", "XXX", "XXX", 5)]
        [TestCase("BBB", "BBB", "CCC", "AAA", null)]
        public void ReturnsCorrectRule(string filter1, string filter2, string filter3, string filter4, int? expectedRuleId)
        {
            var result = _rulesEngine.FindRule(new StrategyFilters
            {
                Filter1 = filter1,
                Filter2 = filter2,
                Filter3 = filter3,
                Filter4 = filter4,
            });
            Assert.AreEqual(expectedRuleId, result?.RuleId);
        }

        [Test]
        [TestCase("AAA", "BBB", "CCC", "DDD", 2)]
        [TestCase("AAA", "BBB", "CCC", "AAA", 4)]
        [TestCase("DDD", "BBB", "CCC", "AAA", 1)]
        [TestCase("DDD", "DDD", "CCC", "AAA", 1)]
        [TestCase("BBB", "BBB", "CCC", "AAA", null)]
        public void ReturnsCorrectOutputValue(string filter1, string filter2, string filter3, string filter4, int? expectedOutputValue)
        {
            var result = _rulesEngine.FindRule(new StrategyFilters
            {
                Filter1 = filter1,
                Filter2 = filter2,
                Filter3 = filter3,
                Filter4 = filter4,
            });
            Assert.AreEqual(expectedOutputValue, result?.OutputValue);
        }
    }
}