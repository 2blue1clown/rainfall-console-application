
using Models;
namespace DataService;
public class RainfallReporter : IReporter<RainfallReport>
{
    IProcessor<RainfallData, DeviceData> p;
    public RainfallReporter(IProcessor<RainfallData, DeviceData> p)
    {
        this.p = p;
    }

    // implement this
    public IEnumerable<RainfallReport> Reports
    {
        get
        {
            return from average in p.Averages
                   join trend in p.Trends on average.Id equals trend.Id
                   join classification in p.Classifications on average.Id equals classification.Id
                   join device in p.DeviceInfo on average.Id equals device.Id
                   select new RainfallReport()
                   {
                       Id = average.Id,
                       Average = average.Average,
                       Trend = trend.Trend,
                       Classification = classification.Classification,
                       Name = device.Name,
                       Location = device.Location
                   };
        }
    }

}