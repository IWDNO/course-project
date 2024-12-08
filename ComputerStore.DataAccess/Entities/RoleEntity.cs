namespace ComputerStore.DataAccess.Entities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<UserEntity> Users { get; set; } = [];

    }
}
