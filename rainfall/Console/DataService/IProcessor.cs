using Models;

public interface IProcessor
{
    IEnumerable<TrendData> Trends { get; }
    IEnumerable<AverageData> Averages { get; }
    IEnumerable<ClassificationData> Classifications { get; }
    IEnumerable<DeviceData> DeviceInfo { get; }
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
