using NoteRepository.Common.Utility.StringEnumeration;

namespace NoteRepository.Common.Utility.MeasureUnit
{
    /// <summary>
    /// Enumeration for weight unit
    /// </summary>
    public enum WeightUnit
    {
        /// <summary>
        /// The pounds unit
        /// </summary>
        [StringValue("lb")]
        Pound,

        /// <summary>
        /// The Kilogram unit
        /// </summary>
        [StringValue("kg")]
        Kilogram,

        /// <summary>
        /// The Gram unit
        /// </summary>
        [StringValue("g")]
        Gram
    }
}