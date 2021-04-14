using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SweatSpace.Api.Persistence.Helpers
{
    /// <summary>
    /// Use the generic static method to create a PagedList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int pageNumber, int itemsPerPage, int totalItems)
        {
            PageNumber = pageNumber;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;

            //cast to an integer but math ceiling requires using doubles. Math ceiling since we want the items to fit
            TotalPages = (int)Math.Ceiling(totalItems / (double)itemsPerPage);
            //add the items to the list
            AddRange(items);
        }

        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }

        /// <summary>
        /// Creates a paged list with the items returned from executing the query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>A paged list</returns>
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int pageNumber, int itemsPerPage)
        {
            var count = await query.CountAsync();
            //page 1, page size 2 = Skip 0 and take 2
            var items = await query.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToListAsync();
            return new PagedList<T>(items, pageNumber, itemsPerPage, count);
        }
    }
}