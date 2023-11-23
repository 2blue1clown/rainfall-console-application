// See https://aka.ms/new-console-template for more information

using CoreService;
using CoreService.Models;

if (args.Length < 2)
{
    Console.WriteLine("Please supply the correct number of arguments");
    Console.WriteLine("dotnet run <path_to_device_data> <path_to_folder_of_rainfall_data>");
    Environment.Exit(1);
}

var device_path = args[0];
var data_folder = args[1];

List<RainfallData> rainfall_data = new();
List<DeviceData> device_data = new();

FileService.DataReader.LoadFolderDataToList(data_folder, rainfall_data);
FileService.DataReader.LoadFileDataToList(device_path, device_data);

if (device_data.Count == 0)
{
    Console.WriteLine("ERROR: No devices provided in the devices data file");
    Environment.Exit(1);
}

if (rainfall_data.Count == 0)
{
    Console.WriteLine("ERROR: No valid rainfall data provided in folder");
    Environment.Exit(1);
}

try
{
    var sorted_data = HelperService.Helper.SortIntoDictionary(device_data, rainfall_data);
    // Printer.PrintDictionary(sorted_data);
    var processor = new Processor(sorted_data);
    Printer.Print(processor.outputData);
}
catch (KeyNotFoundException e)
{
    Console.WriteLine("ERROR: Found rainfall data with id not in devices file.\nPlease update devices file to include device '{0}'", e.Message);
    Environment.Exit(1);
}


