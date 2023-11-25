
using System.Data;
using Models;

namespace DataService;

public class Processor : IProcessor
{
    private IEnumerable<RainfallData> rainfallData;
    private IEnumerable<DeviceData> deviceData;
    public DateTime currentTime;
    public Processor() { }
    public void SetData(IEnumerable<RainfallData> rainfallData, IEnumerable<DeviceData> deviceData)
    {
        this.rainfallData = rainfallData.Where(r => r.Id.Length > 0);
        this.deviceData = deviceData.Where(d => d.Id.Length > 0);
        currentTime = this.rainfallData.Max(row => row.Time);
        CheckData();
    }

    private void CheckData()
    {
        if (!deviceData.Any())
        {
            Console.WriteLine("ERROR: No devices provided in the devices data file");
            Environment.Exit(1);
        }

        if (!rainfallData.Any())
        {
            Console.WriteLine("ERROR: No valid rainfall data provided in folder");
            Environment.Exit(1);
        }

        if (deviceData.DistinctBy(d => d.Id).Count() != deviceData.Count())
        {
            Console.WriteLine("WARNING: There are duplicate ids in devices. Removing duplicates and proceeding\n");
            deviceData = deviceData.DistinctBy(d => d.Id);
        }

        var rainfallIdsNotInDevices = rainfallData.Select(r => r.Id).Except(deviceData.Select(d => d.Id));
        var deviceIdsNotInRainfall = deviceData.Select(d => d.Id).Except(rainfallData.Select(r => r.Id));
        if (rainfallIdsNotInDevices.Any())
        {
            Console.WriteLine("WARNING: Rainfall data includes the following ids not in given devices data. They will be removed.");
            foreach (var id in rainfallIdsNotInDevices)
            {
                Console.WriteLine(id);
                Console.WriteLine();

            }
            rainfallData = rainfallData.Where(r => !rainfallIdsNotInDevices.Contains(r.Id));
        }
        if (deviceIdsNotInRainfall.Any())
        {
            Console.WriteLine("WARNING: Devices data includes the following devices than in rainfall data");
            foreach (var id in deviceIdsNotInRainfall)
            {
                Console.WriteLine(id);
                Console.WriteLine();
            }
        }

    }

    private IEnumerable<IGrouping<string, RainfallData>>? groupedRainfallData
    {
        get
        {
            return from row in rainfallData group row by row.Id into idGroup select idGroup;
        }
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

    public IEnumerable<DeviceData> DeviceInfo { get { return deviceData; } }

    public IEnumerable<AverageData> Averages
    {
        get
        {
            return from g in groupedRecentRainfallData
                   select new AverageData()
                   {
                       Id = g.Key,
                       Average = g.Select(row => row.Rainfall).Average()
                   };
        }
    }
    public IEnumerable<ClassificationData> Classifications
    {
        get
        {
            return from g in groupedRecentRainfallData
                   select new ClassificationData()
                   {
                       Id = g.Key,
                       Classification = Classify(g.Select(row => row.Rainfall))
                   };
        }
    }
    public IEnumerable<TrendData> Trends
    {
        get
        {
            return from g in groupedRainfallData
                   select new TrendData()
                   {
                       Id = g.Key,
                       Trend = DetermineTrend(g.Select(row => row.Rainfall))
                   };
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
