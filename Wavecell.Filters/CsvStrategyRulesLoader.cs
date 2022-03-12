using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Wavecell.Filters;

public class CsvStrategyRulesLoader
{
    public static async Task<List<SearchRule>> LoadRulesAsync(string filePath, CancellationToken cancellationToken)
    {
        var lines = await File.ReadAllLinesAsync(filePath, cancellationToken);
        var rules = new List<SearchRule>();
        foreach (var line in lines.Skip(1))
        {
            rules.Add(ParseRule(line));
        }
        return rules;
    }

    public static SearchRule ParseRule(string line)
    {
        var values = line.Split(',');
        return new SearchRule
        {
            RuleId = int.Parse(values[0]),
            Priority = int.Parse(values[1]),
            OutputValue = int.Parse(values[6]),
            Filters = new StrategyFilters
            {
                Filter1 = ParseStringValue(values[2]),
                Filter2 = ParseStringValue(values[3]),
                Filter3 = ParseStringValue(values[4]),
                Filter4 = ParseStringValue(values[5]),
            }
        };
    }

    private static string ParseStringValue(string input) => input == "<ANY>" ? null : input;
}