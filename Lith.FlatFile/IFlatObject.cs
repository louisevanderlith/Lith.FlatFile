namespace Lith.FlatFile
{
    /// <summary>
    /// Used to identify Objects used to create lines.
    /// Please note that properties should be listed in the order they should be used.
    /// </summary>
    public interface IFlatObject
    {
        /// <summary>
        /// Used to identify lines
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Used to validate the combined property values
        /// Any line not meeting the requirement will have a filler appended.
        /// </summary>
        int TotalLineLength { get; }
    }
}
