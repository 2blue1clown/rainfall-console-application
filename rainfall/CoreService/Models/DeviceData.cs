using CsvHelper.Configuration.Attributes;

namespace CoreService.Models.DeviceData;
public class DeviceData : IId
{
    [Index(0)]
    public string Id { get; set; }
    [Index(1)]
    public string Name { get; set; }
    [Index(2)]
    public string Location { get; set; }

}