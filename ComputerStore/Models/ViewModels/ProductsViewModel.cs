namespace ComputerStore.Models.ViewModels
{
    public class ProductsViewModel
    {
        public CategoryEntity? Category { get; set; }
        public List<ProductEntity>? Products { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
