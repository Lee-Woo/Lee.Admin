using System;
using System.Collections.Generic;

namespace RMIMS.Client.Core.Utils
{
    public static class CommonExtensions
    {
        public static void ForEach<TItem>(this IEnumerable<TItem> sequence, Action<TItem> action)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            foreach (TItem item in sequence)
            {
                action(item);
            }
        }
    }
}
