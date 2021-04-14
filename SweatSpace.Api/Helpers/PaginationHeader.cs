using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweatSpace.Api.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int totalItems, int pageNumber, int totalPages, int itemsPerPage)
        {
            TotalItems = totalItems;
            PageNumber = pageNumber;
            TotalPages = totalPages;
            ItemsPerPage = itemsPerPage;
        }

        public int TotalItems { get; set; }
        public int PageNumber { get; set; } 
        public int TotalPages { get; set; } 
        public int ItemsPerPage { get; set; } 
    }
}
