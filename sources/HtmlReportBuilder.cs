using System.Text;
using SecurityUtils;
namespace DatabaseComparer;
public class HtmlReportBuilder
{
    public string GenerateReport(long ticks, AppSettings settings, Dictionary<string, TableComparisonResult> results)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"<html><head><title>Report {ticks}</title></head><body>");
        sb.AppendLine("<h1>Table comparer result</h1>");
        sb.AppendLine($"<p><b>Database A:</b> {ConnectionStringMasker.Mask(settings.DatabaseA.ConnectionString)}</p>");
        sb.AppendLine($"<p><b>Database B:</b> {ConnectionStringMasker.Mask(settings.DatabaseB.ConnectionString)}</p>");
        foreach (var (table, result) in results)
        {
            sb.AppendLine($"<h2>Table: {table}</h2>");

            if (result.Differences.Any())
            {
                sb.AppendLine("<h3>Modified Rows</h3><table border='1'>");
                sb.AppendLine("<tr><th>Key</th><th>Field</th><th>DB A</th><th>DB B</th></tr>");
                foreach (var diff in result.Differences)
                {
                    sb.AppendLine($"<tr style='background-color:yellow'><td>{diff.Key}</td><td>{diff.Column}</td><td>{diff.ValueA}</td><td>{diff.ValueB}</td></tr>");
                }
                sb.AppendLine("</table><br/>");
            }

            if (result.AddedKeys.Any())
            {
                sb.AppendLine("<h3>Added Rows (DB B)</h3><ul>");
                foreach (var key in result.AddedKeys)
                {
                    sb.AppendLine($"<li style='color:green'>Key: {key}</li>");
                }
                sb.AppendLine("</ul><br/>");
            }

            if (result.DeletedKeys.Any())
            {
                sb.AppendLine("<h3>Deleted Rows (DB A)</h3><ul>");
                foreach (var key in result.DeletedKeys)
                {
                    sb.AppendLine($"<li style='color:red'>Key: {key}</li>");
                }
                sb.AppendLine("</ul><br/>");
            }

            sb.AppendLine("<hr/><br/>");

        }

        sb.AppendLine("</body></html>");
        return sb.ToString();
    }
}
