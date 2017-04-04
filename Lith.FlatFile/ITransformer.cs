using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lith.FlatFile
{
    public interface ITransformer
    {
        string Transform(object value, FlatPropertyAttribute attributes);
    }
}
