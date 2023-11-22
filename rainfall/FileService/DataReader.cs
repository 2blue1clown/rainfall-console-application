using System.Globalization;
using System.Reflection;
using CsvHelper;

namespace FileService;

public class DataReader<T, V>
{
    public static Dictionary<string, List<V>> LoadDataToDictionaryKeys(string path, string keyProp, Dictionary<string, List<V>> dict)
    {
        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // read device data and prepare for sorting of data
            var data = csv.GetRecords<T>();

            foreach (var row in data)
            {
                PropertyInfo propInfo = row.GetType().GetProperty(keyProp);
                string key = (string)propInfo.GetValue(row, null);

                if (!dict.ContainsKey(key))
                {
                    dict.Add(key, new List<V>());
                }
            }
        }
        return dict;
    }

    public static Dictionary<string, List<V>> LoadDataToDictionaryValues(string path, string keyProp, Dictionary<string, List<V>> dict)
    {

        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // read device data and prepare for sorting of data
            var data = csv.GetRecords<V>();

            foreach (var row in data)
            {
                PropertyInfo propInfo = row.GetType().GetProperty(keyProp);
                string key = (string)propInfo.GetValue(row, null);

                dict[key].Add(row);

            }
        }

        return dict;
    }


}
