using ComputerStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
public class RolesController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public RolesController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(string roleFilter = "", int page = 1, int pageSize = 10)
    {
        var users = _userManager.Users.AsQueryable();

        if (!string.IsNullOrEmpty(roleFilter))
        {
            if (roleFilter == "Seller")
            {
                var sellerIds = await _userManager.GetUsersInRoleAsync("Seller");
                users = users.Where(u => sellerIds.Contains(u));
            }
            else if (roleFilter == "Customer")
            {
                var customerIds = await _userManager.GetUsersInRoleAsync("Customer");
                users = users.Where(u => customerIds.Contains(u));
            }
        }

        var totalUsers = await users.CountAsync();
        var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);

        var paginatedUsers = await users
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var viewModel = new UserListViewModel
        {
            Users = paginatedUsers,
            RoleFilter = roleFilter,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages
        };

        return View(viewModel);
    }

    public async Task<IActionResult> AssignSellerRole(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.AddToRoleAsync(user, "Seller");
            }
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> RevokeSellerRole(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Seller");
            }
        }

        return RedirectToAction("Index");
    }
}