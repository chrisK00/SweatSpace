using SweatSpace.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SweatSpace.Infrastructure.Services
{
    internal class ShuffleService : IShuffleService
    {
        public static readonly Random _random = new();

        public IEnumerable<T> ShuffleListAsync<T>(IEnumerable<T> items)
        {
            //make a random value for every item. Orderby that value. Select and return the actuall item
            return items.Select(x => new { value = x, order = _random.Next() })
                .OrderBy(x => x.order)
                .Select(x => x.value);
        }
    }
}