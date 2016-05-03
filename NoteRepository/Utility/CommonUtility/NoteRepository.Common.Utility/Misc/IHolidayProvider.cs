using System;
using System.Collections;

namespace NoteRepository.Common.Utility.Misc
{
    public interface IHolidayProvider
    {
        Holiday GetHoliday(string holidayName, string country, DateTime startDate);

        ArrayList OrderedHolidays(string country, DateTime startDate);
    }
}