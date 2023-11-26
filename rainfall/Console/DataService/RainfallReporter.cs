
using DataService.Models;
namespace DataService;

public class RainfallReporter : IReporter<RainfallData, DeviceData, RainfallReport>
{
    IRainfallProcessor<RainfallData, DeviceData> p;
    public RainfallReporter(IRainfallProcessor<RainfallData, DeviceData> p)
    {
        this.p = p;
    }

    public void SetData(IEnumerable<RainfallData> rainfallData, IEnumerable<DeviceData> deviceData)
    {
        p.SetData(rainfallData, deviceData);
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