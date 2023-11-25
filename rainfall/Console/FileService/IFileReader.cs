
public interface IFileReader
{
    public IEnumerable<T> LoadFileData<T>(string path);
    public IEnumerable<T> LoadFolderData<T>(string path);
}