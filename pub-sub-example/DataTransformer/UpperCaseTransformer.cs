using Common;

namespace DataTransformer
{
  public class UpperCaseTransformer : IDataTransformer<string, string>
  {
    public string Transform(string input)
    {
      return input.ToUpper();
    }
  }
}
