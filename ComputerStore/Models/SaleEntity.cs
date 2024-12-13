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
    }
}

