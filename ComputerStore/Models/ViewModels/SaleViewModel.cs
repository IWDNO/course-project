using ComputerStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class SaleViewModel
{
    public string CustomerId { get; set; }
    public List<IdentityUser> Customers { get; set; } = new();

    public List<CategoryEntity> Categories { get; set; } = new();
    public List<ProductEntity> Products { get; set; } = new();

    public List<SaleItemViewModel> SaleItems { get; set; } = new();
}

public class SaleItemViewModel
{
    public Guid ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }

    public decimal Price { get; set; }
}

public class CreateSaleViewModel
{
    public List<SaleItemEntity> SaleItems { get; set; } = new List<SaleItemEntity>();
    public List<CategoryEntity> AvailableCategories { get; set; } = new List<CategoryEntity>();
    public List<ProductEntity> AvailableProducts { get; set; } = new List<ProductEntity>();
    public List<IdentityUser> AvailableCustomers { get; set; } = new List<IdentityUser>(); // Список покупателей
    public string? CustomerId { get; set; } // ID выбранного покупателя
    public decimal TotalPrice { get; set; } // Общая цена
}