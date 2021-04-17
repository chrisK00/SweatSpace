using System;
using System.Collections.Generic;
using System.Linq;
using SweatSpace.Api.Business.Interfaces;

namespace SweatSpace.Api.Business.Services
{
    internal class ShuffleService : IShuffleService
    {
        public static readonly Random _random = new();

        public IEnumerable<T> ShuffleList<T>(IEnumerable<T> items)
        {
            //make a random value for every item. Orderby that value. Select and return the actuall item
            return items.Select(x => new { value = x, order = _random.Next() })
                .OrderBy(x => x.order)
                .Select(x => x.value);
        }
    }
}