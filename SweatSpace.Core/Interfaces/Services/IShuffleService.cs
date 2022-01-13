﻿using System.Collections.Generic;

namespace SweatSpace.Core.Interfaces.Services
{
    public interface IShuffleService
    {
        /// <summary>
        /// Returns a list with the items in random order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        IEnumerable<T> ShuffleListAsync<T>(IEnumerable<T> items);
    }
}