using ComputerStore.Application.Implementations;
using ComputerStore.Application.Interfaces;

namespace ComputerStore.Application
{
    public class DataManager
    {
        public ICategoriesRepository CategoriesRepository { get; }
        public ISuppliersRepository SuppliersRepository { get; }
        public IProductsRepository ProductsRepository { get; }
        public IRolesRepository RolesRepository { get; }
        public ISalesRepository SalesRepository { get; }
        public ISaleItemsRepository SaleItemsRepository { get; }
        public IUsersRepository UsersRepository { get; }

        public DataManager(
            ICategoriesRepository categoriesRepository,
            ISuppliersRepository suppliersRepository,
            IProductsRepository productsRepository,
            IRolesRepository rolesRepository,
            ISalesRepository salesRepository,
            ISaleItemsRepository saleItemsRepository,
            IUsersRepository usersRepository)
        {
            CategoriesRepository = categoriesRepository;
            SuppliersRepository = suppliersRepository;
            ProductsRepository = productsRepository;
            RolesRepository = rolesRepository;
            SalesRepository = salesRepository;
            SaleItemsRepository = saleItemsRepository;
            UsersRepository = usersRepository;
        }
    }
}
