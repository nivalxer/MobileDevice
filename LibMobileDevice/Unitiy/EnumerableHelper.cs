using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibMobileDevice.Unitiy
{
    public static class EnumerableHelper
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
            {
                action(item);
            }
        }
    }
}
