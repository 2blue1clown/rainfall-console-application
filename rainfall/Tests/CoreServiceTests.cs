using CoreService;
using CoreService.Models;

namespace CoreServiceTests;

public class CoreService_Should
{
    private string relativePathPrefix = "..\\..\\..\\..\\";

    [Theory]
    [InlineData(new[] { 2020, 6, 5, 11, 30, 0 }, "..\\testData\\Data1.csv")]
    public void IdentifyCurrentTime(int[] expected, string relativePath)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), relativePathPrefix, relativePath);
        var expectedTime = new DateTime(expected[0], expected[1], expected[2], expected[3], expected[4], expected[5]);
        var rainfallData = FileService.DataReader.LoadFileData<RainfallData>(path);
        var p = new Processor(rainfallData, Enumerable.Empty<DeviceData>());
        Assert.Equal(p.currentTime, expectedTime);
    }

    [Theory]
    [InlineData(Trend.FLAT, new[] { 0.0, 0 })]
    [InlineData(Trend.INCREASING, new[] { 0.0, 1, 2, 3, 1 })]
    [InlineData(Trend.DECREASING, new[] { 0.0, 5, 2, 3, 0 })]
    public void IdentifyTrend(Trend result, double[] input)
    {
        var p = new Processor();
        Assert.Equal(result, p.DetermineTrend([.. input]));
    }

    [Theory]
    [InlineData(Classification.RED, new[] { 35.0, 0, 0, 0 })]
    [InlineData(Classification.RED, new[] { 20, 15, 15, 13.0 })]
    [InlineData(Classification.AMBER, new[] { 10, 15, 10, 13.0 })]
    [InlineData(Classification.GREEN, new[] { 0, 5, 1, 13.0 })]
    public void ClassifyRainfall(Classification result, double[] input)
    {
        var p = new Processor();
        Assert.Equal(result, p.Classify([.. input]));
    }


}