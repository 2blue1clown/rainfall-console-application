
using System.Diagnostics;
using CoreService.Models;
using CoreService;

namespace CoreService;

public class Processor
{
    Dictionary<string, List<RainfallData>> dict;
    DateTime currentTime;
    DateTime cutoff;

    public Dictionary<string, OutputData> outputData = [];

    public Processor(Dictionary<string, List<RainfallData>> dict)
    {
        this.dict = dict;
        foreach (var key in dict.Keys)
        {
            outputData.Add(key, new OutputData() { Id = key });
        }
        SetCurrentTime();
        cutoff = currentTime.AddHours(-4);
        CalculateRecentRainfallAvgs();
        DetermineClassifications();
        DetermineTrends();
    }

    private void SetCurrentTime()
    {
        var data = dict.Values.ToList();
        this.currentTime = DateTime.MinValue;
        data.ForEach(subData => subData.ForEach(row =>
        {
            if (DateTime.Compare(currentTime, row.Time) < 0)
            {
                currentTime = row.Time;
            }
        }));
    }

    private static double Avg(List<int> data)
    {
        double total = 0;
        data.ForEach(num => total += num);
        return total / data.Count;
    }

    public List<Tuple<string, double>> RecentRainfallAvgs()
    {
        var lst = new List<Tuple<string, double>>();
        foreach (var key in dict.Keys)
        {
            var data = OnlyDataAfter(this.dict[key], cutoff).Select(row => row.Rainfall).ToList();
            lst.Add(new Tuple<string, double>(key, Avg(data)));
        }
        return lst;
    }

    private void CalculateRecentRainfallAvgs()
    {
        var avgs = RecentRainfallAvgs();
        avgs.ForEach(tup => outputData[tup.Item1].Avg = tup.Item2);
    }

    private void DetermineClassifications()
    {
        foreach (var key in dict.Keys)
        {
            outputData[key].Classification = Classify(dict[key]);
        }
    }

    private Classification Classify(List<RainfallData> data)
    {
        return Classify(OnlyDataAfter(data, cutoff).Select(row => row.Rainfall).ToList());
    }

    private Classification Classify(List<int> lst)
    {
        var avg = Avg(lst);
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
        foreach (var key in dict.Keys)
        {
            outputData[key].Trend = DetermineTrend(dict[key]);
        }
    }
    private Trend DetermineTrend(List<RainfallData> lst)
    {
        var xVals = new List<double>();
        for (int i = 0; i < lst.Count; i++)
        {
            xVals.Add(i);
        }
        var slope = Slope(xVals, lst.Select(row => (double)row.Rainfall).ToList());

        if (slope > 0) return Trend.INCREASING;
        else if (slope == 0) return Trend.FLAT;
        else return Trend.DECREASING;


    }

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
