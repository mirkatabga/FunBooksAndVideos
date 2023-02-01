using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Infrastructure.Persistence.Repositories
{
    public class MembershipRepository : RepositoryBase<Membership>, IMembershipRepository
    {
        public MembershipRepository(FunBooksAndVideosContext dbContext) : base(dbContext)
        {
        }
    }
}