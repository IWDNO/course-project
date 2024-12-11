using ComputerStore.Application.Implementations;
using ComputerStore.Application.Interfaces;

namespace ComputerStore.Application
{
    public class DataManager
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly ISuppliersRepository _suppliersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly ISalesRepository _salesRepository;
        private readonly ISaleItemsRepository _saleItemsRepository;
        private readonly IUsersRepository _usersRepository;

        public DataManager(
            ICategoriesRepository categoriesRepository,
            ISuppliersRepository suppliersRepository,
            IProductsRepository productsRepository,
            IRolesRepository rolesRepository,
            ISalesRepository salesRepository,
            ISaleItemsRepository saleItemsRepository,
            IUsersRepository usersRepository)
        {
            _categoriesRepository = categoriesRepository;
            _suppliersRepository = suppliersRepository;
            _productsRepository = productsRepository;
            _rolesRepository = rolesRepository;
            _salesRepository = salesRepository;
            _saleItemsRepository = saleItemsRepository;
            _usersRepository = usersRepository;
        }

        public ICategoriesRepository CategoriesRepository => _categoriesRepository;

        public ISuppliersRepository SuppliersRepository => _suppliersRepository;

        public IProductsRepository ProductsRepository => _productsRepository;

        public IRolesRepository RolesRepository => _rolesRepository;

        public ISalesRepository SalesRepository => _salesRepository;

        public ISaleItemsRepository SaleItemsRepository => _saleItemsRepository;

        public IUsersRepository UsersRepository => _usersRepository;
    }
}
