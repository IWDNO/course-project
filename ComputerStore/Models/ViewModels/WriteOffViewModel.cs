namespace ComputerStore.Models.ViewModels
{
    public class WriteOffViewModel
    {
        public string Reason { get; set; } = string.Empty;
        public List<WriteOffItemEntity> WriteOffItems { get; set; } = new List<WriteOffItemEntity>();
        public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}
