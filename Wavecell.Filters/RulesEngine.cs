using System;
using System.Collections.Generic;

namespace Wavecell.Filters;

public class RulesEngine : IRulesEngine
{
    private readonly Dictionary<Type, List<SearchRule>> _rules = new();

    public RulesEngine()
    {
    }
    public RulesEngine(IEnumerable<SearchRule> rules) => AddRules(rules);
    
    public void AddRules(IEnumerable<SearchRule> rules)
    {
        foreach (var rule in rules)
        {
            AddRule(rule);
        }
    }

    public void AddRule(SearchRule rule)
    {
        var key = rule.Filters.GetType();
        if (_rules.ContainsKey(key))
        {
            _rules[key].Add(rule);
        }
        else
        {
            _rules.Add(key, new List<SearchRule>(new[] { rule }));
        }
    }

    public SearchRule FindRule(IFilters filters)
    {
        var rules = _rules[filters.GetType()];
        SearchRule result = null;
        foreach (var rule in rules)
        {
            if (rule.Priority >= (result?.Priority ?? int.MinValue) && rule.Filters.Match(filters))
            {
                result = rule;
            }
        }
        return result;
    }
}