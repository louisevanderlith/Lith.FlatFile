using System;

namespace Lith.FlatFile.Models
{
    public class FlatChild
    {
        public string ParentID { get; set; }

        public Type Type { get; set; }

        public IFlatObject Value { get; set; }
    }
}
