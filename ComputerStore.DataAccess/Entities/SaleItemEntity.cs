namespace ComputerStore.DataAccess.Entities
{
    public class SaleItemEntity
    {
        public Guid Id { get; set; }

        public Guid SaleId { get; set; }

        public SaleEntity? Sale {  get; set; }

        public Guid ProductId { get; set; }

        public ProductEntity? Product { get; set; }

        public int Quantity { get; set; } = 0;

        public decimal Price { get; set; } = 0;
    }
}

