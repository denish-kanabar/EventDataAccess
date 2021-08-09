using System;

namespace EvenDataAccess.WorkerAPI
{
    public static class Extensions
    {
        public static int Random(this int i, int start, int end)
        {
            Random random = new();
            return random.Next(start, end);
        }
        public static DateTime Now(this DateTime dt)
        {
            return DateTime.Now;
        }
    }
}
