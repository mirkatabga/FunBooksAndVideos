using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Infrastructure.Persistence.Repositories
{
    public class ShippingSlipRepository : RepositoryBase<ShippingSlip>, IShippingSlipRepository
    {
        public ShippingSlipRepository(FunBooksAndVideosContext dbContext) : base(dbContext)
        {
        }
    }
}