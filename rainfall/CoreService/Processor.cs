using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using CoreService.Models.RainfallData;

namespace CoreService;

public class Processor
{
    Dictionary<string, List<RainfallData>> dict;
    DateTime currentTime;

    public Processor(Dictionary<string, List<RainfallData>> dict)
    {
        this.dict = dict;
        this.setCurrentTime();
    }

    private void setCurrentTime()
    {
        var data = dict.Values.ToList();
        this.currentTime = DateTime.MinValue;
        data.ForEach(subData => subData.ForEach(row =>
        {
            if (DateTime.Compare(currentTime, row.Time) < 0)
            {
                this.currentTime = row.Time;
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
        var cutoff = this.currentTime.AddHours(-4);
        foreach (var key in this.dict.Keys)
        {
            var data = this.OnlyDataAfter(this.dict[key], cutoff).Select(row => row.Rainfall).ToList();
            lst.Add(new Tuple<string, double>(key, Avg(data)));
        }
        return lst;
    }

    private List<RainfallData> OnlyDataAfter(List<RainfallData> lst, DateTime cutoff)
    {
        return lst.Where(row => DateTime.Compare(cutoff, row.Time) < 0).ToList();
    }


}
