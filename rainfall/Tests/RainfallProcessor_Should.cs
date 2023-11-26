

using System.Data;
using DataService;
using DataService.Models;
using FileService;

namespace Tests;

public class RainfallProcessor_Should
{
    private string relativePathPrefix = "..\\..\\..\\..\\";

    [Theory]
    [InlineData(new[] { 2020, 6, 5, 11, 30, 0 }, "..\\testData\\Data1.csv")]
    public void IdentifyCurrentTime(int[] expected, string relativePath)
    {
        var fileReader = new FileReader();
        var path = Path.Combine(Directory.GetCurrentDirectory(), relativePathPrefix, relativePath);
        var expectedTime = new DateTime(expected[0], expected[1], expected[2], expected[3], expected[4], expected[5]);
        var rainfallData = fileReader.LoadFileData<RainfallData>(path);
        var processor = new RainfallProcessor();

        processor.SetData(rainfallData, Enumerable.Empty<DeviceData>());

        Assert.Equal(processor.currentTime, expectedTime);
    }

    [Theory]
    [InlineData(Trend.FLAT, new[] { 0.0, 0 })]
    [InlineData(Trend.INCREASING, new[] { 0.0, 1, 2, 3, 1 })]
    [InlineData(Trend.DECREASING, new[] { 0.0, 5, 2, 3, 0 })]
    public void IdentifyTrend(Trend result, double[] input)
    {
        var processor = new RainfallProcessor();
        Assert.Equal(result, processor.DetermineTrend([.. input]));
    }

    [Theory]
    [InlineData(Classification.RED, new[] { 35.0, 0, 0, 0 })]
    [InlineData(Classification.RED, new[] { 20, 15, 15, 13.0 })]
    [InlineData(Classification.AMBER, new[] { 10, 15, 10, 13.0 })]
    [InlineData(Classification.GREEN, new[] { 0, 5, 1, 13.0 })]
    public void ClassifyRainfall(Classification result, double[] input)
    {
        var processor = new RainfallProcessor();
        Assert.Equal(result, processor.Classify([.. input]));
    }

    [Fact]
    public void IdentifyMostRecentData()
    {
        var rainfallData = new List<RainfallData> {
            new RainfallData(){Id = "1", Rainfall = 1, Time = new DateTime(2020, 6, 5, 4, 0, 0)},
            new RainfallData(){Id = "2", Rainfall = 30, Time = new DateTime(2020, 6, 5, 3, 30, 0)},
            new RainfallData(){Id = "3", Rainfall = 10, Time = new DateTime(2020, 6, 5, 3, 0, 0)},
            new RainfallData(){Id = "4", Rainfall = 0, Time = new DateTime(2020, 6, 5, 0, 0, 0)},
            new RainfallData(){Id = "5", Rainfall = 0, Time = new DateTime(2020, 6, 4, 23, 30, 0)},
            new RainfallData(){Id = "6", Rainfall = 0, Time = new DateTime(2020, 6, 4, 23, 0, 0)},
         };

        var processor = new RainfallProcessor();

        processor.SetData(rainfallData, Enumerable.Empty<DeviceData>());

        var expectedMostRecentData = new List<RainfallData> {
            new RainfallData(){Id = "1", Rainfall = 1, Time = new DateTime(2020, 6, 5, 4, 0, 0)},
            new RainfallData(){Id = "2", Rainfall = 30, Time = new DateTime(2020, 6, 5, 3, 30, 0)},
            new RainfallData(){Id = "3", Rainfall = 10, Time = new DateTime(2020, 6, 5, 3, 0, 0)},
         };
        Assert.Equal(processor.recentRainfallData.Count(), expectedMostRecentData.Count());
    }

}