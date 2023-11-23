
using CoreService.Models.OutputData;
using CoreService.Models.RainfallData;

namespace CoreService;

public class Processor
{
    Dictionary<string, List<RainfallData>> dict;
    DateTime currentTime;
    DateTime cutoff;

    public Dictionary<string, OutputData> processedData = new();

    public Processor(Dictionary<string, List<RainfallData>> dict)
    {
        this.dict = dict;
        foreach (var key in dict.Keys)
        {
            processedData.Add(key, new OutputData() { Id = key });
        }
        setCurrentTime();
        cutoff = currentTime.AddHours(-4);
        CalculateRecentRainfallAvgs();
        DetermineClassifications();
    }

    private void setCurrentTime()
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
        avgs.ForEach(tup => processedData[tup.Item1].Avg = tup.Item2);
    }

    private void DetermineClassifications()
    {
        foreach (var key in dict.Keys)
        {
            processedData[key].Classification = Classify(dict[key]);
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


}
