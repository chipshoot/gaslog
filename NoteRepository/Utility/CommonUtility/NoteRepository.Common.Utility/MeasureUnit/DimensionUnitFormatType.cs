using NoteRepository.Common.Utility.StringEnumeration;

namespace NoteRepository.Common.Utility.MeasureUnit
{
    public enum DimensionUnitFormatType
    {
        [StringValue("mm")]
        Millimetre,

        [StringValue("cm")]
        Centimetre,

        [StringValue("m")]
        Metre,

        [StringValue("in")]
        Inch,

        [StringValue("ft")]
        Feet,

        [StringValue("Feet")]
        MillimetreAndInch,
    }
}