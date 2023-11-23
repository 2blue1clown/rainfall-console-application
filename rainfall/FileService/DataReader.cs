using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace FileService;

public class DataReader
{

    /// <summary>
    /// Puts data from file into given list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="lst"></param>
    /// <returns></returns>
    public static void LoadFileDataToList<T>(string path, List<T> lst)
    {
        Console.WriteLine("Data loading from {0}", path);
        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // read device data and prepare for sorting of data

            var data = csv.GetRecords<T>();
            try
            {
                foreach (var row in data)
                {
                    lst.Add(row);
                }
            }
            catch
            {
                Console.WriteLine("{0} has the wrong data type", path);
            }
        }
    }


    public static void LoadFolderDataToList<T>(string path, List<T> lst)
    {
        foreach (string file in Directory.EnumerateFiles(path))
        {
            try
            {
                LoadFileDataToList<T>(file, lst);
            }
            catch
            {
                Console.WriteLine("{0} did not have the correct type of data", file);
            }
        }
    }

}
