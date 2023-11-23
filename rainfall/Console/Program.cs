// See https://aka.ms/new-console-template for more information

using CoreService;
using CoreService.Models.DeviceData;
using CoreService.Models.RainfallData;

var device_path = "C:\\Users\\61421\\Documents\\webdev-2023\\rainfall\\data\\Devices.csv";
var data_folder = "C:\\Users\\61421\\Documents\\webdev-2023\\rainfall\\data";

List<RainfallData> rainfall_data = new();
List<DeviceData> device_data = new();

FileService.DataReader.LoadFolderDataToList(data_folder, rainfall_data);
FileService.DataReader.LoadFileDataToList(device_path, device_data);

var sorted_data = HelperService.Helper.SortIntoDictionary(device_data, rainfall_data);

// Printer.PrintDictionary(sorted_data);
var processor = new Processor(sorted_data);
var recent_avgs = processor.RecentRainfallAvgs();
recent_avgs.ForEach(tup => Console.WriteLine("{0} has avg reading of {1} in the last 4 hours", tup.Item1, tup.Item2));

