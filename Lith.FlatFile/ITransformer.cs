using Lith.FlatFile.Models;
using System;

namespace Lith.FlatFile
{
    public interface ITransformer
    {
        string Transform(FlatProperty fProp);
    }
}
