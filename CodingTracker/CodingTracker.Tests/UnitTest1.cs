namespace CodingTracker.Tests;
using CodingTracker;

[TestClass]
public class CodingTrackerTests
{    
    [TestMethod]
    public void CanDelete_NonExistent_ReturnsFalse()
    {
        // Arrange - not necessary, static method.
        
        // Act 
        Controller.Delete();

        // Assert
    }
}