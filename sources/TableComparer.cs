using Microsoft.Extensions.Configuration;
using System.Data;
namespace DatabaseComparer;
public class TableComparer
{
    public TableComparisonResult Compare(DataTable a, DataTable b)
    {
        var result = new TableComparisonResult();
        var keyColumn = a.Columns[0].ColumnName; // assumes first column is key

        var dictA = a.AsEnumerable().ToDictionary(r => r[keyColumn].ToString());
        var dictB = b.AsEnumerable().ToDictionary(r => r[keyColumn].ToString());

        foreach (var key in dictA.Keys)
        {
            if (!dictB.ContainsKey(key))
            {
                result.DeletedKeys.Add(key);
            }
            else
            {
                foreach (DataColumn col in a.Columns)
                {
                    var valA = dictA[key][col.ColumnName].ToString();
                    var valB = dictB[key][col.ColumnName].ToString();
                    if (valA != valB)
                    {
                        result.Differences.Add(new Difference
                        {
                            Key = key,
                            Column = col.ColumnName,
                            ValueA = valA,
                            ValueB = valB
                        });
                    }
                }
            }
        }

        foreach (var key in dictB.Keys.Except(dictA.Keys))
        {
            result.AddedKeys.Add(key);
        }

        return result;
    }
}
