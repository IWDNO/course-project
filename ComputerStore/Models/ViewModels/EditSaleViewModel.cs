namespace ComputerStore.Models.ViewModels
{
    public class EditSaleViewModel
    {
        public SaleEntity Sale { get; set; }
        public List<CategoryEntity> AvailableCategories { get; set; }
        public List<ProductEntity> AvailableProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
