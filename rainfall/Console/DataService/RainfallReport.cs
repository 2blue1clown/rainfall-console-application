
using DataService.Models;

namespace DataService;

public record RainfallReport : IBaseModel
{
    public string Id { get; set; }
    public string Location { get; set; }
    public string Name { get; set; }
    public double Average { get; set; }
    public Classification Classification { get; set; }

    public Trend Trend { get; set; }

    public override string ToString()
    {
        return string.Format("Name: {4}\nDevice Id: {0}\nLocation: {5}\nClassification: {1}\nAverage Reading in Last 4 Hours: {2}\nTrend: {3}\n\n", Id, Classification.ToString(), Average, Trend.ToString(), Name, Location);
    }

}