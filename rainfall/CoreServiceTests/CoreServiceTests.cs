using CoreService;

namespace CoreServiceTests;

public class CoreService_Should
{
    [Theory]
    [InlineData(2, new[] { 1.0, 2, 3 })]
    [InlineData(1.5, new[] { 1.0, 2 })]
    [InlineData(double.NaN, new double[0])]
    public void CalculateAvg(double result, double[] input)
    {
        var p = new Processor();
        Assert.Equal(result, p.Avg(input.ToList()));
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