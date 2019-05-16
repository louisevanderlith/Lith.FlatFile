using Lith.FlatFile.Models;

namespace Lith.FlatFile
{
    public interface ITransformer
    {
        string Transform(FlatProperty fProp);
    }
}
