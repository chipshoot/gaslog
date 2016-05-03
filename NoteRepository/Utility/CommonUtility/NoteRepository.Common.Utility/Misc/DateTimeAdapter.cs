using NoteRepository.Common.Utility.Validation;
using System;

namespace NoteRepository.Common.Utility.Misc
{
    public class DateTimeAdapter : IDateTimeAdapter
    {
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }

        public DateTime AddBusinessDays(DateTime startUtcDate, int days)
        {
            Guard.Against<ArgumentOutOfRangeException>(days < 0, "days cannot be negative");

            if (days == 0)
            {
                return startUtcDate;
            }

            if (startUtcDate.DayOfWeek == DayOfWeek.Saturday)
            {
                startUtcDate = startUtcDate.AddDays(2);
                days -= 1;
            }
            else if (startUtcDate.DayOfWeek == DayOfWeek.Sunday)
            {
                startUtcDate = startUtcDate.AddDays(1);
                days -= 1;
            }

            startUtcDate = startUtcDate.AddDays(days / 5 * 7);
            var extraDays = days % 5;

            if ((int)startUtcDate.DayOfWeek + extraDays > 5)
            {
                extraDays += 2;
            }

            return startUtcDate.AddDays(extraDays);
        }

        ///  <summary>
        ///  Finds the next date whose day of the week equals the specified day of the week.
        ///  </summary>
        ///  <param name="startUtcDate">
        /// 		The date to begin the search.
        ///  </param>
        ///  <param name="desiredDay">
        /// 		The desired day of the week whose date will be returned.
        ///  </param>
        /// <param name="numberToSkip">
        ///         The number to control how many week day to skip, e.g. we need find next two
        ///         Friday, then you will get Friday of following week not this week
        /// </param>
        /// <returns>
        /// 		The returned date occurs on the given date's week.
        /// 		If the given day occurs before given date, the date for the
        /// 		following week's desired day is returned.
        ///  </returns>
        public DateTime GetNextDateForDay(DateTime startUtcDate, DayOfWeek desiredDay, int numberToSkip = 0)
        {
            // Given a date and day of week,
            // find the next date whose day of the week equals the specified day of the week.
            Guard.Against<ArgumentOutOfRangeException>(numberToSkip < 0, "Cannot passing numberToSkip which is less than zero");
            return startUtcDate.AddDays(DaysToAdd(startUtcDate.DayOfWeek, desiredDay, numberToSkip));
        }

        /// <summary>
        /// Gets the current week day based on startDate, e.g. say current date is July 10, 2015, then
        /// we can you this method to get current week's Monday is July 8, 2015
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="desiredDay">The desired week day.</param>
        /// <returns>DateTime.</returns>
        public DateTime GetCurrentWeekDateForDay(DateTime startDate, DayOfWeek desiredDay)
        {
            var daysToDesiredDay = (int) desiredDay - (int) startDate.DayOfWeek;
            var result = startDate.AddDays(daysToDesiredDay);

            return result;
        }

        /// <summary>
        /// Calculates the number of days to add to the given day of
        /// the week in order to return the next occurrence of the
        /// desired day of the week.
        /// </summary>
        /// <param name="current">
        ///		The starting day of the week.
        /// </param>
        /// <param name="desired">
        ///		The desired day of the week.
        /// </param>
        /// <param name="numberToSkip">
        ///         The number to control how many week day to skip, e.g. we need find next two
        ///         Friday, then you will get Friday of following week not this week
        /// </param>
        /// <returns>
        ///		The number of days to add to <var>current</var> day of week
        ///		in order to achieve the next <var>desired</var> day of week.
        /// </returns>
        private static int DaysToAdd(DayOfWeek current, DayOfWeek desired, int numberToSkip)
        {
            // f( c, d ) = g( c, d ) mod 7, g( c, d ) > 7
            //           = g( c, d ), g( c, d ) < = 7
            //   where 0 <= c < 7 and 0 <= d < 7

            var c = (int)current;
            var d = (int)desired;
            var n = (7 - c + d);

            return ((n > 7) ? n % 7 : n) + (7 * numberToSkip);
        }
    }
}