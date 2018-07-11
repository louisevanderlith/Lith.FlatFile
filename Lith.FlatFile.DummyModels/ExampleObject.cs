using System;
using System.Collections.Generic;

namespace Lith.FlatFile.DummyModels
{
    /// <summary>
    /// Example of how a file's model has to be setup.
    /// </summary>
    public class ExampleObject : IFlatObject
    {
        public ExampleObject()
        {
            Children = new List<ExampleObjectB>();
        }

        /// <summary>
        /// ID is used to match the Record/Line indicator to the Object
        /// This is required by the IFlatObject
        /// </summary>
        [FlatProperty(1, false)]
        public string ID
        {
            get
            {
                return "E";
            }
        }

        /// <summary>
        /// Example of a boolean property.
        /// 'Y' == true
        /// </summary>
        [FlatProperty(1, false, BooleanOptions = "Y|N")]
        public bool HasSomething { get; set; }

        /// <summary>
        /// Example of a enum value.
        /// Default value is OptionA or 01
        /// </summary>
        [FlatProperty(2, true, DefaultValue = DumbEnum.OptionA)]
        public DumbEnum ChosenOption { get; set; }

        /// <summary>
        /// Example of a data following 'yyyyMMdd' format
        /// </summary>
        [FlatProperty(8, false, StringFormat = "yyyyMMdd")]
        public DateTime FileDate { get; set; }

        /// <summary>
        /// Example of a record which can have a set of options.
        /// Only the PossibleValues specified are valid.
        /// </summary>
        [FlatProperty(1, false, PossibleValues = new[] { "X", "Y", "Z" })]
        public char FewOptions { get; set; }

        /// <summary>
        /// Example of a string property
        /// </summary>
        [FlatProperty(25, false)]
        public string Name { get; set; }

        /// <summary>
        /// Example of a property with decimal places
        /// 12 characters will be used before the decimal place, 3 after
        /// </summary>
        [FlatProperty(15, true, DecimalPlaces = 3)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Example of Children
        /// This will happen when Lines in a file are related to the lines that precede them
        /// The Type parameter in the list must inherit from IFlatObject
        /// Notice that this property doesn't get a [FlatProperty] attribute
        /// </summary>
        public List<ExampleObjectB> Children { get; set; }

        /// <summary>
        /// Indicates the maximum length for this type of Record/Line
        /// This is required by the IFlatObject
        /// </summary>
        public int TotalLineLength
        {
            get
            {
                return 75;
            }
        }
    }
}
