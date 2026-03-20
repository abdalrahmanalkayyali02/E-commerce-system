using ECommerce.Application.Abstraction.Data;
using ECommerce.Domain.modules.IAC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; }
        public ICustomerRepository Customer { get; }
        public ISellerRepository Seller { get; }
        public IUserOTpRepository UserOTp { get; }

        public UnitOfWork(
            AppDbContext context,
            IUserRepository users,
            ICustomerRepository customer,
            ISellerRepository seller,
            IUserOTpRepository userOTp)
        {
            _context = context;
            Users = users;
            Customer = customer;
            Seller = seller;
            UserOTp = userOTp;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //try
            //{
            //   return  await _context.SaveChangesAsync(cancellationToken);

            //}
            //catch (Exception ex)
            //{
            //    throw new Exception();
            //}

            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                // استخدم throw فقط ليرسل الخطأ كاملاً للـ Debugger
                // أو اطبع الـ InnerException
                var innerError = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Database Error: {innerError}");
            }

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}