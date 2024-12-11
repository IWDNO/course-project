using ComputerStore.DataAccess.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.MVC.ViewModels
{
    public class SaleViewModel
    {
        public List<SaleItemViewModel> SaleItems { get; set; } = new List<SaleItemViewModel>();

        [Display(Name = "Общая стоимость")]
        [Range(0, double.MaxValue, ErrorMessage = "Общая стоимость должна быть положительным числом")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "Выберите продавца")]
        [Display(Name = "Продавец")]
        public Guid SellerId { get; set; }

        public List<UserEntity> Sellers { get; set; } = new List<UserEntity>();
    }

    public class SaleItemViewModel
    {
        public Guid ProductId { get; set; }

        [Display(Name = "Товар")]
        public string ProductName { get; set; } = string.Empty;

        [Display(Name = "Количество")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть больше 0")]
        public int Quantity { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}