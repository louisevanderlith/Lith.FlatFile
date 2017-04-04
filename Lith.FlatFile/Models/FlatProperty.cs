namespace Lith.FlatFile.Models
{
    public struct FlatProperty
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public FlatPropertyAttribute Attributes { get; set; }
    }
}
