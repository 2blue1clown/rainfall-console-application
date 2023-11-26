

namespace DataService;

public interface IReporter<T, V, R>
{
    public void SetData(IEnumerable<T> data1, IEnumerable<V> data2);
    public IEnumerable<R> Reports { get; }
}