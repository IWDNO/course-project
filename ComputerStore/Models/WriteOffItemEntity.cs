namespace ComputerStore.Models
{
    public class WriteOffItemEntity
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
        public ProductEntity? Product { get; set;}

        public Guid WriteOffId { get; set; }
        public WriteOffEntity? WriteOff { get; set; }
    }
}

