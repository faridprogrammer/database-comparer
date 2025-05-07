// Program.cs
using Microsoft.Extensions.Configuration;
using DatabaseComparer;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var settings = configuration.Get<AppSettings>();

var dbA = new DatabaseService(settings.DatabaseA.ConnectionString);
var dbB = new DatabaseService(settings.DatabaseB.ConnectionString);
var comparer = new TableComparer();
var reportBuilder = new HtmlReportBuilder();

var results = new Dictionary<string, TableComparisonResult>();

foreach (var table in settings.TablesToCompare)
{
    var tableA = await dbA.GetTableDataAsync(table);
    var tableB = await dbB.GetTableDataAsync(table);
    var result = comparer.Compare(tableA, tableB);
    results[table] = result;
}

var ticks = DateTime.Now.Ticks;
var html = reportBuilder.GenerateReport(ticks, settings, results);
File.WriteAllText($"report_{ticks}.html", html);