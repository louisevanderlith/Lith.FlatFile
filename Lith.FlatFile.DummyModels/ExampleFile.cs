using System;

namespace Lith.FlatFile.DummyModels
{
    public class ExampleFile : IFlatObject
    {
        [FlatProperty(1, false)]
        public string ID
        {
            get
            {
                return "E";
            }
        }

        [FlatProperty(1, false, BooleanOptions = "Y|N")]
        public bool HasSomething { get; set; }

        [FlatProperty(2, true, DefaultValue = DumbEnum.OptionA)]
        public DumbEnum ChosenOption { get; set; }

        [FlatProperty(8, false, StringFormat = "yyyyMMdd")]
        public DateTime FileDate { get; set; }

        [FlatProperty(1, false, PossibleValues = new[] { "X", "Y", "Z" })]
        public char FewOptions { get; set; }

        [FlatProperty(25, false)]
        public string Name { get; set; }

        [FlatProperty(15, true, DecimalPlaces = 3)]
        public decimal Amount { get; set; }

        public int TotalLineLength
        {
            get
            {
                return 75;
            }
        }
    }
}
