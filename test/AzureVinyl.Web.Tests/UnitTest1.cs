namespace AzureVinyl.Web.Tests;

public class UnitTest1
{
    [Fact]
    public void NewFailingTest()
    {
        // Arrange
        int result;

        // Act
        result = 1 + 1;

        // Assert
        Assert.Equal(3, result);
    }
}
