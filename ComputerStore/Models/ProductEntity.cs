namespace ComputerStore.Models
{
    public class ProductEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; } = 0;

        public int StockQuantity {  get; set; } = 0;

        public Guid CategoryId { get; set; }

        public CategoryEntity? Category { get; set; }

        public Guid SupplierId { get; set; }

        public SupplierEntity? Supplier { get; set; }

        public List<SaleItemEntity> SaleItems { get; set; } = [];
    }
}

