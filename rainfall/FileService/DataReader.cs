using System.Globalization;
using System.Reflection;
using CsvHelper;

namespace FileService;

public class DataReader<T, K> where T : new()
{
    public Dictionary<K, T> LoadDataToDictionary(string path, string keyProp, Dictionary<K, T> dict)
    {
        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // read device data and prepare for sorting of data
            var data = csv.GetRecords<T>();

            foreach (var row in data)
            {
                PropertyInfo propInfo = row.GetType().GetProperty(keyProp);
                K key = (K)propInfo.GetValue(row, null);

                if (!dict.ContainsKey(key))
                {
                    dict.Add(key, new T());
                }
            }
        }
        return dict;
    }
}
