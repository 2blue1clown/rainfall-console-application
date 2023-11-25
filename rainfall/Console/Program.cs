// See https://aka.ms/new-console-template for more information



using DataService;
using Models;

if (args.Length < 2)
{
    Console.WriteLine("Please supply the correct number of arguments");
    Console.WriteLine("dotnet run <path_to_device_data> <path_to_folder_of_rainfall_data>");
    Environment.Exit(1);
}

var device_path = args[0];
var data_folder = args[1];

var rainfall_data = FileService.FileService.LoadFolderData<RainfallData>(data_folder);
var device_data = FileService.FileService.LoadFileData<DeviceData>(device_path);

if (!device_data.Any())
{
    Console.WriteLine("ERROR: No devices provided in the devices data file");
    Environment.Exit(1);
}

if (!rainfall_data.Any())
{
    Console.WriteLine("ERROR: No valid rainfall data provided in folder");
    Environment.Exit(1);
}

Console.WriteLine();
IProcessor p = new Processor(rainfall_data, device_data);
var reporter = new Reporter(p);
foreach (var report in reporter.Reports)
{
    Console.WriteLine(report);
}



