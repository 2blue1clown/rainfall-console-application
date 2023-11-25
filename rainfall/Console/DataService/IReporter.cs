

namespace DataService;

public interface IReporter
{
    public IEnumerable<Report> Reports { get; }
}