// See https://aka.ms/new-console-template for more information

using CoreService;
using CoreService.Models;

if (args.Length < 2)
{
    Console.WriteLine("Please supply the correct number of arguments");
    Console.WriteLine("rainfall <path_to_device_data> <path_to_folder_of_rainfall_data>");
    throw new Exception("Not enough args");
}

Console.WriteLine("Testing that building still works");


var device_path = args[0];
var data_folder = args[1];

List<RainfallData> rainfall_data = new();
List<DeviceData> device_data = new();

FileService.DataReader.LoadFolderDataToList(data_folder, rainfall_data);
FileService.DataReader.LoadFileDataToList(device_path, device_data);

var sorted_data = HelperService.Helper.SortIntoDictionary(device_data, rainfall_data);

// Printer.PrintDictionary(sorted_data);
var processor = new Processor(sorted_data);
Printer.Print(processor.outputData);


