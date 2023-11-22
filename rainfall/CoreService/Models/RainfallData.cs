
using CsvHelper.Configuration.Attributes;

namespace Models.RainfallData;

public class RainfallData
{
    [Index(0)]
    public string Id { get; set; }
    [Index(1)]

    public DateTime Time { get; set; }
    [Index(2)]

    public int Rainfall { get; set; }

}