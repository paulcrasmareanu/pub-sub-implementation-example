namespace Common
{
  public interface IDataTransformer<TOutput, TInput>
  {
    public TOutput Transform(TInput input);
  }
}
