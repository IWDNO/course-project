using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models.ViewModels
{
    public class SaleViewModel
    {
        public Guid CustomerId { get; set; }
        public List<CustomerEntity> Customers { get; set; } = new();

        public List<ProductEntity> Products { get; set; } = new();

        public List<SaleItemViewModel> SaleItems { get; set; } = new();
    }

    public class SaleItemViewModel
    {
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
