using Microsoft.AspNetCore.Identity;

namespace ComputerStore.Models.ViewModels
{
    public class UserListViewModel
    {
        public List<IdentityUser> Users { get; set; }
        public string RoleFilter { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
