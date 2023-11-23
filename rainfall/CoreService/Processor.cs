using CoreService.Models.RainfallData;

namespace CoreService;

public class Processor
{
    private static double RainfallAvg(List<RainfallData> data)
    {
        double total = 0;
        data.ForEach(row => total += row.Rainfall);
        return total / data.Count;
    }

    public static List<Tuple<string, double>> RainfallAvgs(Dictionary<string, List<RainfallData>> dict)
    {
        var lst = new List<Tuple<string, double>>();
        foreach (var key in dict.Keys)
        {
            lst.Add(new Tuple<string, double>(key, RainfallAvg(dict[key])));
        }
        return lst;
    }


}
