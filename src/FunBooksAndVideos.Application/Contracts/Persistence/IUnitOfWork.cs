namespace FunBooksAndVideos.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }

        ICustomerRepository Customers { get; }

        IProductRepository Products { get; }

        IMembershipRepository Memberships { get; }

        IShippingSlipRepository ShippingSlips { get; }

        void SaveChanges();
    }
}