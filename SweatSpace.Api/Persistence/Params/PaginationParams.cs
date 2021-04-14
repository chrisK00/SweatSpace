namespace SweatSpace.Api.Persistence.Params
{
    public class PaginationParams
    {
        private int _itemsPerPage = 5;
        private const int _maxItemsPerPage = 50;

        public int PageNumber { get; set; } = 1;

        public int ItemsPerPage
        {
            get => _itemsPerPage;
            set => _itemsPerPage = value > _maxItemsPerPage ? _maxItemsPerPage : value;
        }
    }
}