using DataService;

public class App<T, V, R>
{
    string[] args;
    IEnumerable<T> data;
    IEnumerable<V> deviceData;
    IFileReader fileReader;

    public App(string[] args, IFileReader fileReader, IReporter<T, V, R> reporter)
    {
        this.fileReader = fileReader;
        this.args = args;
        CheckArgs();
        LoadData(args[0], args[1]);

        // completed null check here to ensure editor new data is not null.
        if (data == null || deviceData == null)
        {
            Console.WriteLine("ERROR: No data to process.");
            Environment.Exit(1);
        }

        reporter.SetData(data, deviceData);

        foreach (var report in reporter.Reports)
        {
            Console.WriteLine(report);
        }
    }

    private void CheckArgs()
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Please supply the correct number of arguments");
            Console.WriteLine("dotnet run <path_to_device_data> <path_to_folder_of_rainfall_data>");
            Environment.Exit(1);
        }
    }

    private void LoadData(string devicePath, string dataFolderPath)
    {
        data = fileReader.LoadFolderData<T>(dataFolderPath);
        deviceData = fileReader.LoadFileData<V>(devicePath);
    }
}