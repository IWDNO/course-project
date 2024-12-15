namespace ComputerStore.Models
{
    public class SaleEntity
    {
        public Guid Id { get; set; }
        public DateTime SaleDate { get; set; }

        public List<SaleItemEntity> SaleItems { get; set; } = [];

        public Guid SellerId { get; set; } 
        public WorkerEntity? Seller { get; set; } 

        public Guid CustomerId { get; set; } 
        public CustomerEntity? Customer { get; set; }

        public decimal TotalPrice { get; set; }

        public Guid StatusId { get; set; }
        public SaleStatusEntity? Status { get; set; }
    }

    public class SaleStatusEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;

        public List<SaleEntity> Sales { get; set; } = [];
    }
}

