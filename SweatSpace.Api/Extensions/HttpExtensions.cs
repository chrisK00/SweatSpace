using System.Text.Json;
using Microsoft.AspNetCore.Http;
using SweatSpace.Api.Helpers;

namespace SweatSpace.Api.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int totalItems, int itemsPerPage, int pageNumber,
            int totalPages)
        {
            var paginationHeader = new PaginationHeader(totalItems, pageNumber, totalPages, itemsPerPage);

            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
    }
}