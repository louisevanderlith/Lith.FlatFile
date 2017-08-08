namespace Lith.FlatFile
{
    public interface ITransformer
    {
        string Transform(object value, FlatPropertyAttribute attributes);
    }
}
