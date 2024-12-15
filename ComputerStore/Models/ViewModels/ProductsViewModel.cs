namespace ComputerStore.Models.ViewModels
{
    public class ProductsViewModel
    {
        public CategoryEntity? Category { get; set; }
        public List<ProductEntity>? Products { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public SaleEntity? CurrentSale { get; set; }
        public List<SaleItemEntity> SaleItems { get; set; } = new List<SaleItemEntity>();
    }
}
