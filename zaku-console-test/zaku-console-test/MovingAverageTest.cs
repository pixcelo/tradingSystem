using Zaku;
namespace zaku_console_test;

public class MovingAverageTest
{
    [Fact]
    public void MovingAverage_Equal()
    {
        // Arrange
        //var movingAverage = new MovingAverage();

        // Act
        var actual = MovingAverage.ComputeAverage(1, 20);

        // Assert
        Assert.Equal(1, actual);
    }
}
