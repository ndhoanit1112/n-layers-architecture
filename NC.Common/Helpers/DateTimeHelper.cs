using System;

namespace NC.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static bool IsOverlap(DateTime startDate1, DateTime endDate1, DateTime startDate2, DateTime endDate2, bool overlapIfSameStartOrEnd)
        {
            if (overlapIfSameStartOrEnd)
            {
                return !(endDate1 < startDate2 || startDate1 > endDate2);
            }

            return !(endDate1 <= startDate2 || startDate1 >= endDate2);
        }

        public static bool IsIn(DateTime startDateCheck, DateTime endDateCheck, DateTime startDate, DateTime endDate)
        {
            return startDate <= startDateCheck && endDateCheck <= endDate;
        }
    }
}
