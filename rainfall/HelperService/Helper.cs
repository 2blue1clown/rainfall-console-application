using System.ComponentModel;
using CoreService.Models;

namespace HelperService;



public class Helper
{
    public static Dictionary<string, List<V>> SortIntoDictionary<T, V>(List<T> lstT, List<V> lstV)
    where V : IId
    where T : IId
    {
        var dict = new Dictionary<string, List<V>>();
        lstT.ForEach(row =>
        {
            if (!dict.ContainsKey(row.Id) && row.Id.Length > 0)
            {
                dict.Add(row.Id, new List<V>());
            }
        });
        lstV.ForEach(row =>
        {
            if (dict.ContainsKey(row.Id)) dict[row.Id].Add(row);
            else
            {
                throw new KeyNotFoundException(string.Format("{0}", row.Id));
            }
        });
        return dict;
    }
}
