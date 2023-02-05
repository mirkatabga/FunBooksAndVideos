using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Infrastructure.Persistence.Repositories;

namespace FunBooksAndVideos.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FunBooksAndVideosContext _context;
        private IOrderRepository? _orderRepository;
        private CustomerRepository? _customerRepository;
        private ProductRepository? _productRepository;
        private MembershipRepository? _membershipRepository;
        private ShippingSlipRepository? _shippingSlipRepository;

        public UnitOfWork(FunBooksAndVideosContext context)
        {
            _context = context;
        }

        public IOrderRepository Orders =>
            _orderRepository ??= new OrderRepository(_context);

        public ICustomerRepository Customers =>
            _customerRepository ??= new CustomerRepository(_context);

        public IProductRepository Products =>
            _productRepository ??= new ProductRepository(_context);

        public IMembershipRepository Memberships =>
            _membershipRepository ??= new MembershipRepository(_context);

        public IShippingSlipRepository ShippingSlips =>
            _shippingSlipRepository ??= new ShippingSlipRepository(_context);

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}