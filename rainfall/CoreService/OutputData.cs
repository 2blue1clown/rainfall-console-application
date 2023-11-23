
namespace CoreService.Models.OutputData;

public enum Classification { GREEN, RED, AMBER }
public enum Trend { INCREASING, DECREASING, FLAT }


public class OutputData : IId
{
    public string Id { get; set; }
    public double Avg { get; set; }
    public Classification Classification { get; set; }

    public Trend Trend { get; set; }

    public override string ToString()
    {
        return string.Format("Device Id: {0}\nClassification: {1}\nAvg Reading in Last 4 Hours: {2}\nTrend: {3}\n\n", Id, Classification.ToString(), Avg, Trend.ToString());
    }

}