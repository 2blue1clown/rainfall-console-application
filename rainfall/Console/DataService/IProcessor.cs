using Models;


/// <summary>
/// Responsible for finding averages, trends and classifications from data.
/// 
/// T is the type of individual data points
/// V is the type of data able the device that collected the individual data points
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="V"></typeparam>
public interface IProcessor<T, V>
{
    public void SetData(IEnumerable<T> data, IEnumerable<V> deviceData);
    IEnumerable<TrendData> Trends { get; }
    IEnumerable<AverageData> Averages { get; }
    IEnumerable<ClassificationData> Classifications { get; }
    IEnumerable<V> DeviceInfo { get; }
}

public enum Classification { GREEN, RED, AMBER }
public enum Trend { INCREASING, DECREASING, FLAT }

public record TrendData : IBaseModel
{
    public string Id { get; set; }
    public Trend Trend { get; set; }
}

public record ClassificationData : IBaseModel
{
    public string Id { get; set; }
    public Classification Classification { get; set; }
}


public record AverageData : IBaseModel
{
    public string Id { get; set; }
    public double Average { get; set; }

}
