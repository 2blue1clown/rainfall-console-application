using System.Globalization;
using CsvHelper;


namespace FileService;

public class DataReader
{
    public static IEnumerable<T> LoadFileData<T>(string path)
    {
        Console.WriteLine("Attempting to load file data from {0}", path);

        var data = ExecuteWithLogging(() =>
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("en-AU")))
            {
                // read device data and prepare for sorting of data
                var data = csv.GetRecords<T>().ToList();
                Console.WriteLine("Successfully loaded file data");
                return data;
            }
        });
        return data;
    }

    public static IEnumerable<T> LoadFolderData<T>(string path)
    {
        Console.WriteLine("Attempting to load folder data from {0}", path);
        var collection = Enumerable.Empty<T>();
        var files = GetFiles(path);
        foreach (string file in files)
        {
            collection = collection.Concat(LoadFileData<T>(file));
        }
        Console.WriteLine("Successfully loaded folder", path);
        return collection;
    }

    private static IEnumerable<T> ExecuteWithLogging<T>(Func<IEnumerable<T>> action)
    {
        try
        {
            return action();
        }
        catch (BadDataException)
        {
            Console.WriteLine("The file has bad data");
        }
        catch (CsvHelperException)
        {
            Console.WriteLine("The file has bad data or has wrong shape");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("The file or directory cannot be found.");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("The file or directory cannot be found.");
        }
        catch (PathTooLongException)
        {
            Console.WriteLine("'path' exceeds the maximum supported path length.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("You do not have permission to create this file.");
        }
        catch (IOException e)
        {
            Console.WriteLine($"An exception occurred:\nError code: " +
                              $"{e.HResult & 0x0000FFFF}\nMessage: {e.Message}");
        }
        return Enumerable.Empty<T>().ToList();
    }
    private static IEnumerable<string> GetFiles(string path)
    {
        var files = ExecuteWithLogging(() =>
        {
            var files = Directory.EnumerateFiles(path);
            return files;
        });
        return files;
    }

}
