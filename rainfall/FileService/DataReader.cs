using System.Globalization;
using CsvHelper;


namespace FileService;

public class DataReader
{
    public static IEnumerable<T> LoadFileData<T>(string path)
    {
        using (
                var reader = new StreamReader(path)
                )
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // read device data and prepare for sorting of data
            var data = csv.GetRecords<T>();
            if (data != null) return data;

        }
        return Enumerable.Empty<T>();
    }

    public static IEnumerable<T> LoadFolderData<T>(string path)
    {
        var collection = Enumerable.Empty<T>();
        foreach (string file in Directory.EnumerateFiles(path))
        {
            collection.Concat(LoadFileData<T>(file));
        }
        return collection;
    }


    /// <summary>
    /// Puts data from file into given list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="lst"></param>
    /// <returns></returns>
    public static void LoadFileDataToList<T>(string path, List<T> lst)
    {
        try
        {
            using (
                var reader = new StreamReader(path)
                )
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
                    // Some csv in the data folder might have the wrong data type. Thats ok just move on.
                    // Console.WriteLine("{0} has the wrong data type", path);
                }
            }
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("Error: File '{0}' not found", path);
            Environment.Exit(1);
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
                //Some files will have the wrong type of data and thats ok, just move on.
                // Console.WriteLine("{0} did not have the correct type of data", file);
            }
        }
    }

}
