using DataService;
using Models;

public class App
{
    string[] args;
    IEnumerable<RainfallData> rainfallData;
    IEnumerable<DeviceData> deviceData;
    public App(string[] args, IProcessor p, IReporter r)
    {
        this.args = args;
        CheckArgs();
        LoadData(args[0], args[1]);

        if (rainfallData == null || deviceData == null)
        {
            Console.WriteLine("ERROR: No data to process.");
            Environment.Exit(1);
        }

        p.SetData(rainfallData, deviceData);
        foreach (var report in r.Reports)
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
        rainfallData = FileService.FileService.LoadFolderData<RainfallData>(dataFolderPath);
        deviceData = FileService.FileService.LoadFileData<DeviceData>(devicePath);
    }
}