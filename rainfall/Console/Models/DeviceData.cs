using CsvHelper.Configuration.Attributes;

namespace DataService.Models;
public record DeviceData : IBaseModel
{
    [Index(0)]
    public string Id { get; set; }
    [Index(1)]
    public string Name { get; set; }
    [Index(2)]
    public string Location { get; set; }

}