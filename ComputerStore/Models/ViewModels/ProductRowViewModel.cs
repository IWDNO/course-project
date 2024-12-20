namespace ComputerStore.Models.ViewModels
{
    public class ProductRowViewModel
    {
        public int Index { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ProductId { get; set; }
        public int Quantity { get; set; }
        public List<CategoryEntity> AvailableCategories { get; set; } = new();
        public List<ProductEntity> AvailableProducts { get; set; } = new();
    }
}
