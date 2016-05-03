using System;

namespace NoteRepository.Common.Utility.Misc
{
    public struct ProgressInfo
    {
        public int MaxValue { get; set; }

        public int CurrentValue { get; set; }

        public int Percentage
        {
            get
            {
                if (MaxValue <= 0)
                {
                    return 0;
                }
                var percentage = (int)Math.Round(CurrentValue * 100.0 / MaxValue, 0);

                return percentage;
            }
        }

        public string Text { get; set; }
    }
}