using ECommerce.Application.Abstraction.Data;
using ECommerce.Domain.modules.Catalog.Repository;
using ECommerce.Domain.modules.UserMangement.Repositories;
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
        public INotificationRepository Notification { get; }

        public IProductRepository productRepository { get; }

        public IProductAttributeRepository ProductAttributeRepository { get; }

        public IProductCatogryRepository ProductCatogryRepository { get; }

        public IProductPhotoRepository ProductPhotoRepository { get; }

        public UnitOfWork
            (
              AppDbContext context,
              IUserRepository users,
              ICustomerRepository customer,
              ISellerRepository seller,
              IUserOTpRepository userOTp,
              IProductRepository productRepository,
              IProductAttributeRepository ProductAttributeRepository,
              IProductCatogryRepository ProductCatogryRepository,
              IProductPhotoRepository ProductPhotoRepository
            ,
              INotificationRepository notification

             )
        {
            _context = context;
            Users = users;
            Customer = customer;
            Seller = seller;
            UserOTp = userOTp;
            this.productRepository = productRepository;
            this.ProductPhotoRepository = ProductPhotoRepository;
            this.ProductCatogryRepository = ProductCatogryRepository;
            this.ProductAttributeRepository = ProductAttributeRepository;
            Notification = notification;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
   
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
               
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