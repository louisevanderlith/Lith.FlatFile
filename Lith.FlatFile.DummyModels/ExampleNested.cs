namespace Lith.FlatFile.DummyModels
{
    public class ExampleNested : IFlatObject
    {
        public string ID => "";

        [FlatProperty(5, false, DefaultValue = default(ExampleNested))]
        public string Name { get; set; }

        public int TotalLineLength => 5;
    }
}
