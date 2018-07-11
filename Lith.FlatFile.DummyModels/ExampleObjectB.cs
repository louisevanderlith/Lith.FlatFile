namespace Lith.FlatFile.DummyModels
{
    public class ExampleObjectB : IFlatObject
    {
        [FlatProperty(1, false)]
        public string ID => "X";

        [FlatProperty(5, false)]
        public string Name { get; set; }

        public int TotalLineLength => 6;
    }
}
