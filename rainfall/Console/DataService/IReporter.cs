

namespace DataService;

public interface IReporter<T, V, R>
{
    public IEnumerable<R> Reports { get; }
}