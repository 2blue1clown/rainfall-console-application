
using CsvHelper.Configuration.Attributes;

namespace CoreService.Models.RainfallData;

public class RainfallData
{
    [Index(0)]
    public string Id { get; set; }
    [Index(1)]

    public DateTime Time { get; set; }
    [Index(2)]

    public int Rainfall { get; set; }

    public override string ToString()
    {

        return String.Format("Id: {0} Time: {1} Rainfall: {2}", this.Id, this.Time, this.Rainfall);
    }

}