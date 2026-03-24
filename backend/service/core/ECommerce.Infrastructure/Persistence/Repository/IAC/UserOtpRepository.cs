using ECommerce.Domain.modules.IAC.Entity;
using ECommerce.Domain.modules.IAC.Repositories;
using ECommerce.Infrastructure.Persistence.Mapper.IAC;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.IAC
{
    public class UserOtpRepository : IUserOTpRepository
    {
        private readonly AppDbContext _context;

        public UserOtpRepository(AppDbContext context)
        {
            _context = context;
        }   

        public async Task AddAsync(UserOTPEntity userOTPEntity, CancellationToken cancellationToken = default)
        {
            var model =  UserOTPMapper.FromDomainToPersistence(userOTPEntity);

            await _context.UserOtps.AddAsync(model, cancellationToken);
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await _context.UserOtps.FindAsync(id);
            
            if (model != null)
            {
                _context.UserOtps.Remove(model);     
            }
        }

        public  void SoftDelete(UserOTPEntity userOTPEntity, CancellationToken cancellation = default)
        {
            var model = UserOTPMapper.FromDomainToPersistence(userOTPEntity);

            _context.UserOtps.AddAsync(model, cancellation);
        }

        public void Update(UserOTPEntity userOTPEntity, CancellationToken cancellationToken = default)
        {
            var model = UserOTPMapper.FromDomainToPersistence(userOTPEntity);
             _context.UserOtps.Update(model);
        }
    }
}
