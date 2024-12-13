namespace ComputerStore.Models
{
    public class SupplierEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ContactInfo { get; set; } = string.Empty;

        public List<ProductEntity> Products { get; set; } = [];
    }
}

