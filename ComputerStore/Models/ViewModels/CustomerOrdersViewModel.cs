namespace ComputerStore.Models.ViewModels
{
    public class CustomerOrdersViewModel
    {
        public List<SaleEntity> InProcessSales { get; set; } = new List<SaleEntity>();
        public List<SaleEntity> OrderedSales { get; set; } = new List<SaleEntity>();
        public List<SaleEntity> CompletedSales { get; set; } = new List<SaleEntity>();
        public List<SaleEntity> CancelledSales { get; set; } = new List<SaleEntity>();
    }
}
