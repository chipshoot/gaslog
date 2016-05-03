using System;

namespace NoteRepository.Common.Utility.Misc
{
    public class Holiday : IComparable
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Country { get; set; }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var holiday = obj as Holiday;
            if (holiday == null)
            {
                throw new ArgumentException("Object is not a Holiday");
            }

            var h = holiday;
            return Date.CompareTo(h.Date);
        }

        #endregion IComparable Members
    }
}