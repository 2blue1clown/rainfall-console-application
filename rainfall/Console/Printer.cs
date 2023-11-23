
public class Printer
{
    public static void PrintDictionary<T>(Dictionary<string, List<T>> dict)
    {
        foreach (var key in dict.Keys)
        {
            Console.WriteLine("{0}", key);
            foreach (var row in dict[key])
            {
                Console.WriteLine("{0}", row.ToString());
            }
        }
    }

}