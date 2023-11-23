
using CsvHelper.Configuration.Attributes;

namespace CoreService.Models;

public class RainfallData : IId
{
    [Index(0)]
    public string Id { get; set; }
    [Index(1)]

    public DateTime Time { get; set; }
    [Index(2)]

    public int Rainfall { get; set; }

    public override string ToString()
    {

        return string.Format("Id: {0} Time: {1} Rainfall: {2}", this.Id, this.Time, this.Rainfall);
    }

}