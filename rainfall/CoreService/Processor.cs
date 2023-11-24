
using System.Diagnostics;
using CoreService.Models;
using CoreService;

namespace CoreService;

public class Processor
{
    IEnumerable<RainfallData> rainfallData;
    IEnumerable<DeviceData> deviceData;
    public DateTime currentTime;
    DateTime cutoff;

    public Dictionary<string, ReportData> reportData = [];

    public Processor() { }

    public Processor(IEnumerable<RainfallData> rainfallData, IEnumerable<DeviceData> deviceData)
    {
        this.rainfallData = rainfallData;
        this.deviceData = deviceData;
        SetCurrentTime();
    }


    private void SetCurrentTime()
    {
        currentTime = rainfallData.Max(row => row.Time);
    }

    public double RecentRainfallAvg(List<RainfallData> lst)
    {
        var data = OnlyDataAfter(lst, cutoff).Select(row => row.Rainfall).ToList();
        return data.Average();
    }

    private void DetermineRecentRainfallAvgs()
    {
        // foreach (var key in dict.Keys)
        // {
        //     ReportData[key].Avg = RecentRainfallAvg(dict[key]);
        // }
    }

    private void DetermineClassifications()
    {
        // foreach (var key in dict.Keys)
        // {
        //     ReportData[key].Classification = Classify(dict[key]);
        // }
    }

    private Classification Classify(List<RainfallData> data)
    {
        return Classify(OnlyDataAfter(data, cutoff).Select(row => row.Rainfall).ToList());
    }

    public Classification Classify(List<double> lst)
    {
        var avg = lst.Average();
        if (lst.FindIndex(n => n > 30) >= 0 || avg >= 15)
        {
            return Classification.RED;
        }
        else if (avg >= 10) return Classification.AMBER;
        else return Classification.GREEN;
    }


    private List<RainfallData> OnlyDataAfter(List<RainfallData> lst, DateTime cutoff)
    {
        return lst.Where(row => DateTime.Compare(cutoff, row.Time) < 0).ToList();
    }

    private void DetermineTrends()
    {
        // foreach (var key in dict.Keys)
        // {
        //     ReportData[key].Trend = DetermineTrend(dict[key].Select(row => (double)row.Rainfall).ToList());
        // }
    }
    public Trend DetermineTrend(List<double> lst)
    {
        var xVals = new List<double>();
        for (int i = 0; i < lst.Count; i++)
        {
            xVals.Add(i);
        }
        var slope = Slope(xVals, lst);

        if (slope > 0) return Trend.INCREASING;
        else if (slope == 0) return Trend.FLAT;
        else return Trend.DECREASING;


    }

    /// <summary>
    /// Found method from the internet because I do not know how to calculate a linear regression
    /// </summary>
    /// <param name="xVals"></param>
    /// <param name="yVals"></param>
    /// <returns></returns>
    private static double Slope(List<double> xVals, List<double> yVals)

    {

        Debug.Assert(xVals.Count == yVals.Count);

        double sumOfX = 0;
        double sumOfY = 0;
        double sumOfXSq = 0;
        double sumOfYSq = 0;
        double ssX = 0;
        double ssY = 0;
        double sumCodeviates = 0;
        double sCo = 0;

        for (int i = 0; i < yVals.Count; i++)
        {
            double x = xVals[i];
            double y = yVals[i];
            sumCodeviates += x * y;
            sumOfX += x;
            sumOfY += y;
            sumOfXSq += x * x;
            sumOfYSq += y * y;
        }
        ssX = sumOfXSq - ((sumOfX * sumOfX) / yVals.Count);
        ssY = sumOfYSq - ((sumOfY * sumOfY) / yVals.Count);

        sCo = sumCodeviates - ((sumOfX * sumOfY) / yVals.Count);


        return sCo / ssX;
    }

}
