using PizzaAPI.Models;
using PizzaData.Models;

namespace PizzaAPI.Helpers
{
    public class PaginationHelper
    {
        /// <summary>
        /// Get Paged Data   
        /// Return object with pagination metadata and data list based on the provided paging parameters.
        /// </summary>
        /// <param name="source">List of items to paginate</param>
        /// <param name="pagingParameter">Pagination information</param>
        public static Object GetPagedData<T>(List<T> source, PagingParameter pagingParameter)
        {
            int count = source.Count();
            int CurrentPage = pagingParameter.pageNumber;
            int PageSize = pagingParameter.pageSize;
            int TotalCount = count;

            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            return new
            {
                paginationMetadata,
                data = items.ToList(),
            };
        }
    }
}
