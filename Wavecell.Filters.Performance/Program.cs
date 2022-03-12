using BenchmarkDotNet.Running;
using Wavecell.Filters.Performance;

BenchmarkRunner.Run(typeof(Program).Assembly);
//var a = new CsvSampleBenchmark();
//a.FindRule_demoX1000_opsX1000();