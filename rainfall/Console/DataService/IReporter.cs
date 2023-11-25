

namespace DataService;

public interface IReporter<R>
{
    public IEnumerable<R> Reports { get; }
}