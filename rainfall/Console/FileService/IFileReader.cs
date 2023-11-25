
public interface IFileReader
{
    public IEnumerable<T> LoadFileData<T>(string path) => throw new NotImplementedException();
    public IEnumerable<T> LoadFolderData<T>(string path) => throw new NotImplementedException();
}