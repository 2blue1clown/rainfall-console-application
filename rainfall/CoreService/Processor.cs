
using CoreService.Models;

namespace CoreService;

public class Processor
{
    IEnumerable<RainfallData> rainfallData;
    IEnumerable<DeviceData> deviceData;
    public DateTime currentTime;


    public Processor() { }

    public Processor(IEnumerable<RainfallData> rainfallData, IEnumerable<DeviceData> deviceData)
    {
        this.rainfallData = rainfallData;
        this.deviceData = deviceData;
        currentTime = this.rainfallData.Max(row => row.Time);
    }

    private IEnumerable<RainfallData> recentRainfallData
    {
        get
        {
            return from row in rainfallData
                   where row.Time > currentTime.AddHours(-4)
                   select row;
        }
    }

    private IEnumerable<IGrouping<string, RainfallData>>? groupedRecentRainfallData
    {
        get
        {
            return from row in recentRainfallData group row by row.Id into idGroup select idGroup;
        }
    }
    private IEnumerable<IGrouping<string, RainfallData>>? groupedRainfallData
    {
        get
        {
            return from row in rainfallData group row by row.Id into idGroup select idGroup;
        }
    }
    public Dictionary<string, double> Averages
    {
        get
        {
            var averages = new Dictionary<string, double>();
            foreach (var g in groupedRecentRainfallData)
            {
                averages.Add(g.Key, g.Select(row => row.Rainfall).Average());
            }
            return averages;
        }
    }
    public Dictionary<string, Classification> Classifications
    {
        get
        {
            var classifications = new Dictionary<string, Classification>();
            foreach (var g in groupedRecentRainfallData)
            {
                classifications.Add(g.Key, Classify(g.Select(row => row.Rainfall)));
            }
            return classifications;
        }
    }
    public Dictionary<string, Trend> Trends
    {
        get
        {
            var Trends = new Dictionary<string, Trend>();
            foreach (var g in groupedRainfallData)
            {
                Trends.Add(g.Key, DetermineTrend(g.Select(row => row.Rainfall)));
            }
            return Trends;
        }
    }

    public List<ReportData> reportData
    {
        get
        {
            var data = new List<ReportData>();
            foreach (var key in Averages.Keys)
            {
                data.Add(new ReportData()
                {
                    Id = key,
                    Avg = Averages[key],
                    Classification = Classifications[key],
                    Trend = Trends[key]
                });
            }
            return data;
        }
    }

    public Classification Classify(IEnumerable<double> data)
    {
        if (data.Any(n => n > 30) || data.Average() >= 15)
        {
            return Classification.RED;
        }
        else if (data.Average() >= 10) return Classification.AMBER;
        else return Classification.GREEN;
    }

    public Trend DetermineTrend(IEnumerable<double> data)
    {

        var xValues = Enumerable.Range(0, data.Count()).Select(n => (double)n);
        var trendData = xValues.Zip(data).Select(coord => new Coord() { X = coord.First, Y = coord.Second });

        var slope = Slope(trendData);

        if (slope > 0) return Trend.INCREASING;
        else if (slope == 0) return Trend.FLAT;
        else return Trend.DECREASING;
    }
    private struct Coord
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
    /// <summary>
    /// Found method from the internet because I do not know how to calculate a linear regression
    /// </summary>
    /// <param name="xValues"></param>
    /// <param name="yVals"></param>
    /// <returns></returns>
    private static double Slope(IEnumerable<Coord> data)
    {

        double sumOfX = 0;
        double sumOfY = 0;
        double sumOfXSq = 0;
        double sumOfYSq = 0;
        double ssX = 0;
        double ssY = 0;
        double sumCodeviates = 0;
        double sCo = 0;

        foreach (var coord in data)
        {
            double x = coord.X;
            double y = coord.Y;
            sumCodeviates += x * y;
            sumOfX += x;
            sumOfY += y;
            sumOfXSq += x * x;
            sumOfYSq += y * y;
        }
        ssX = sumOfXSq - ((sumOfX * sumOfX) / data.Count());
        ssY = sumOfYSq - ((sumOfY * sumOfY) / data.Count());

        sCo = sumCodeviates - ((sumOfX * sumOfY) / data.Count());
        return sCo / ssX;
    }

}
