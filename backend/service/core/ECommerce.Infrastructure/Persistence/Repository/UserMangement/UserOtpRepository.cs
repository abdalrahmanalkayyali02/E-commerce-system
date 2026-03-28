using Common.Enum;
using Common.Specfication;
using ECommerce.Domain.modules.UserMangement.Entity;
using ECommerce.Domain.modules.UserMangement.Repositories;
using ECommerce.Infrastructure.Persistence.Mapper.UserMangement;
using ECommerce.Infrastructure.Persistence.Model.UserMangement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Repository.UserMangement
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


        //need implement 
        public async Task<UserOTPEntity?> GetLastOtpOfType(Guid userId, OtpType type, CancellationToken cancellation = default)
        {
            var model = await _context.UserOtps
                    .AsNoTracking() // <--- Add this! Tells EF "Don't track this, I'm just reading it"
                    .Where(u => u.userID == userId && u.OTPtype == type && !u.IsUsed && !u.IsVerified)
                    .OrderByDescending(u => u.GeneratedAt)
                    .FirstOrDefaultAsync(cancellation);

            if (model == null) return null;

            return UserOTPMapper.FromPersistenceToDomain(model);
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
