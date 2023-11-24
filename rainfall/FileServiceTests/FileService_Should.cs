using CoreService.Models;

namespace FileServiceTests;

public class FileService_Should
{
    [Theory]
    [InlineData("C:\\Users\\61421\\Documents\\webdev-2023\\rainfall\\testData\\Empty.csv", 0)]
    [InlineData("C:\\Users\\61421\\Documents\\webdev-2023\\rainfall\\testData\\TestFolder1\\Test1.csv", 3)]
    [InlineData("C:\\Users\\61421\\Documents\\webdev-2023\\rainfall\\testData\\Test.csv", 0)]
    [InlineData("C:\\Users\\61421\\Documents\\webdev-2023\\rainfall\\testData\\WrongShape.csv", 0)]
    public void LoadedFileDataHasCorrectCount(string path, int expected)
    {
        var data = FileService.DataReader.LoadFileData<RainfallData>(path);
        Assert.Equal(data.Count(), expected);
    }

    [Theory]
    [InlineData("C:\\Users\\61421\\Documents\\webdev-2023\\rainfall\\testData\\TestFolder1", 3)]
    [InlineData("C:\\Users\\61421\\Documents\\webdev-2023\\rainfall\\testData\\DoesntExist", 0)]
    [InlineData("C:\\Users\\61421\\Documents\\webdev-2023\\rainfall\\testData\\TestFolderEmpty", 0)]

    public void LoadedFolderDataHasCorrectCount(string path, int expected)
    {
        var data = FileService.DataReader.LoadFolderData<RainfallData>(path);
        Assert.Equal(data.Count(), expected);
    }
}