using ECommerce.Infrastructure.Persistence;

namespace Api.Middleware
{
    public class isAdmin
    {
        private readonly AppDbContext _context;

        public isAdmin(AppDbContext context)
        {
            _context = context;
        }




    }
}
