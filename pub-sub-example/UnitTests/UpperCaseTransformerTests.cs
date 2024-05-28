using DataTransformer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
  [TestClass]
  public class UpperCaseTransformerTests
  {
    private UpperCaseTransformer _transformer;

    [TestInitialize]
    public void Setup()
    {
      _transformer = new UpperCaseTransformer();
    }

    [TestMethod]
    public void Transform_ShouldConvertToUpperCase()
    {
      // Arrange
      var input = "test message";
      var expectedOutput = "TEST MESSAGE";

      // Act
      var result = _transformer.Transform(input);

      // Assert
      Assert.AreEqual(expectedOutput, result);
    }

    [TestMethod]
    public void Transform_ShouldHandleEmptyString()
    {
      // Arrange
      var input = string.Empty;
      var expectedOutput = string.Empty;

      // Act
      var result = _transformer.Transform(input);

      // Assert
      Assert.AreEqual(expectedOutput, result);
    }

    [TestMethod]
    public void Transform_ShouldHandleNull()
    {
      // Arrange
      string input = null;

      // Act
      var result = _transformer.Transform(input);

      // Assert
      Assert.IsNull(result);
    }
  }
}
