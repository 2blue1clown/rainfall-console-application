using CsvHelper.Configuration.Attributes;

namespace CoreService.Models.OutputData;

public enum Classification { GREEN, RED, AMBER }


public class OutputData : IId
{
    public string Id { get; set; }
    public double Avg { get; set; }
    public Classification Classification { get; set; }
}