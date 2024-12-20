namespace ComputerStore.Models
{
    public class SaleStatusEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;

        public List<SaleEntity> Sales { get; set; } = [];
    }
}

