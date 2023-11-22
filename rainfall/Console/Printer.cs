
public class Printer<T>
{
    public static void Print(Dictionary<string, List<T>> dict)
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