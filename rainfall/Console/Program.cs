// See https://aka.ms/new-console-template for more information

using CoreService.Models.DeviceData;
using CoreService.Models.RainfallData;





var dict = new Dictionary<string, List<RainfallData>>();

var device_path = "C:\\Users\\61421\\Documents\\webdev-2023\\rainfall\\data\\Devices.csv";
var data_path = "C:\\Users\\61421\\Documents\\webdev-2023\\rainfall\\data\\Data1.csv";

FileService.DataReader<DeviceData, RainfallData>.LoadDataToDictionaryKeys(device_path, "Id", dict);
FileService.DataReader<DeviceData, RainfallData>.LoadDataToDictionaryValues(data_path, "Id", dict);

Printer<RainfallData>.Print(dict);



