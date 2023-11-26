using Models;

namespace DataService;


public interface IDuoProcessor<T, V>
{
    public void SetData(IEnumerable<T> data1, IEnumerable<V> data2);
}

public interface IDeviceInfoProcessor<T>
{
    IEnumerable<T> DeviceInfo { get; }

}
public interface IClassificationsProcessor
{
    IEnumerable<ClassificationData> Classifications { get; }
}


public interface IAveragesProcessor
{
    IEnumerable<AverageData> Averages { get; }
}

public interface ITrendsProcessor
{
    IEnumerable<TrendData> Trends { get; }

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