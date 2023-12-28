using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using ResponseFilter;
using static System.Net.Mime.MediaTypeNames;

namespace ResponseFilterTest;

[TestClass]
public class Test
{
    private class TestData
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    [TestMethod]
    public void TestGetFilteredResponse_ValidDataAndFields_ReturnsExpectedJson()
    {
        // Arrange
        var data = new TestData
        {
            Name = "John Doe",
            Age = 25,
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        var fields = new Fields
        {
            ResponseFields = "Name,Age"
        };

        // Act
        var result = Filter<TestData>.GetFilteredResponse(data, fields);
        var jsonString = result.ToString();

        // Assert
        Assert.IsNotNull(jsonString);
        Assert.IsTrue(jsonString.Contains("\"name\""));
        Assert.IsTrue(jsonString.Contains("\"age\""));
        Assert.IsFalse(jsonString.Contains("\"dateofbirth\""));

    }

    [TestMethod]
    public void GetFilteredResponse_NullData_ThrowsArgumentNullException()
    {
        // Arrange
        TestData data = null;
        var fields = new Fields
        {
            ResponseFields = "Name,Age"
        };

        // Act and Assert
        Assert.ThrowsException<Exception>(() => Filter<TestData>.GetFilteredResponse(data, fields), "Error processing field name: Property name not found in the object.");
    }

    [TestMethod]
    public void GetFilteredResponse_NullFields_ThrowsArgumentNullException()
    {
        // Arrange
        var data = new TestData
        {
            Name = "John Doe",
            Age = 25,
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        Fields fields = null;

        // Act and Assert
        Assert.ThrowsException<ArgumentNullException>(() => Filter<TestData>.GetFilteredResponse(data, fields), "The 'fields' parameter cannot be null.");
    }

    [TestMethod]
    public void GetFilteredResponse_InvalidField_ThrowsInvalidOperationException()
    {
        // Arrange
        var data = new TestData
        {
            Name = "John Doe",
            Age = 25,
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        var fields = new Fields
        {
            ResponseFields = "Address"
        };

        // Act and Assert
        Assert.ThrowsException<Exception>(() => Filter<TestData>.GetFilteredResponse(data, fields), "Error processing field address: Property address not found in the object.");
    }



}
