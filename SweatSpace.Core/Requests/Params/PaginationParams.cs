namespace SweatSpace.Core.Requests.Params
{
    public class PaginationParams
    {
        private int _itemsPerPage = 5;
        private const int _maxItemsPerPage = 50;

        public int PageNumber { get; init; } = 1;

        public int ItemsPerPage
        {
            get => _itemsPerPage;
            init => _itemsPerPage = value > _maxItemsPerPage ? _maxItemsPerPage : value;
        }
    }
}