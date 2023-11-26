
using DataService.Models;
using FileService;

namespace Tests;

public class FileReader_Should
{

    private string relativePathPrefix = "..\\..\\..\\..\\";

    [Theory]
    [InlineData("..\\testData\\Empty.csv", 0)]
    [InlineData("..\\testData\\TestFolder1\\Test1.csv", 3)]
    [InlineData("..\\testData\\Test.csv", 0)]
    [InlineData("..\\testData\\WrongShape.csv", 0)]
    public void LoadedFileDataHasCorrectCount(string relativePath, int expected)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), relativePathPrefix, relativePath);
        var fileReader = new FileReader();
        var data = fileReader.LoadFileData<RainfallData>(path);
        Assert.Equal(data.Count(), expected);
    }

    [Theory]
    [InlineData("..\\testData\\TestFolder1", 3)]
    [InlineData("..\\testData\\DoesntExist", 0)]
    [InlineData("..\\testData\\TestFolderEmpty", 0)]

    public void LoadedFolderDataHasCorrectCount(string relativePath, int expected)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), relativePathPrefix, relativePath);
        var fileReader = new FileReader();
        var data = fileReader.LoadFolderData<RainfallData>(path);
        Assert.Equal(data.Count(), expected);
    }
}