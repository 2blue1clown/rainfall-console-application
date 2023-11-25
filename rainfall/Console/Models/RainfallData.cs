
using CsvHelper.Configuration.Attributes;

namespace Models;

public record RainfallData : IBaseModel
{
    [Index(0)]
    public string Id { get; set; }
    [Index(1)]

    public DateTime Time { get; set; }
    [Index(2)]

    public double Rainfall { get; set; }
}