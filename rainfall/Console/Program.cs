// See https://aka.ms/new-console-template for more information

using CoreService.Models.DeviceData;
using CoreService.Models.RainfallData;


var dict = new Dictionary<string, List<RainfallData>>();

var device_path = "C:\\Users\\61421\\Documents\\webdev-2023\\rainfall\\data\\Devices.csv";
var data_folder = "C:\\Users\\61421\\Documents\\webdev-2023\\rainfall\\data";

List<RainfallData> rainfall_data = new();

FileService.DataReader.LoadFolderDataToList(data_folder, rainfall_data);



