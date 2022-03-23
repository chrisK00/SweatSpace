using System;
using System.Collections.Generic;
using System.Linq;

namespace SweatSpace.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
        {
            var rng = new Random();
            return items.OrderBy(_ => rng.Next());
        }
    }
}
