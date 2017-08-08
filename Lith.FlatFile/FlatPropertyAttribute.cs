using System;

namespace Lith.FlatFile
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FlatPropertyAttribute : Attribute
    {
        private int decimalPlaces = 2;

        public FlatPropertyAttribute(int fieldLength, bool useNumericPadding)
        {
            FieldLength = fieldLength;
            UseNumericPadding = useNumericPadding;
        }

        /// <summary>
        /// This is the maximum field length for a field
        /// </summary>
        public int FieldLength { get; private set; }

        /// <summary>
        /// Numeric values should usually be left padded with zero's
        /// </summary>
        public bool UseNumericPadding { get; private set; }

        /// <summary>
        /// Default value for when the property has not been set.
        /// Empty properties will awlays use empty strings or zeros as padding, unless specified
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Should be set when a property is only allowed to have 
        /// a few possible values.
        /// </summary>
        public string[] PossibleValues { get; set; }

        /// <summary>
        /// This value will be set for converting dates to yyyyMMdd, etc
        /// </summary>
        public string StringFormat { get; set; }

        /// <summary>
        /// Specifies the options for when using bool values.
        /// The first option will always be used as TRUE.
        /// The second option will always be the default when empty.
        /// Example usage "Y|N"
        /// </summary>
        public string BooleanOptions { get; set; }

        /// <summary>
        /// Specifies the amount of to digits use for Decimal values
        /// When this property is left empty, 2 decimal places will be used.
        /// </summary>
        public int DecimalPlaces
        {
            get
            {
                return decimalPlaces;
            }

            set
            {
                decimalPlaces = value;
            }
        }
    }
}
