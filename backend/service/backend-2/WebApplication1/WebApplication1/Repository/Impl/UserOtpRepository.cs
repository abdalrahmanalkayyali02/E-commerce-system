using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Model;
using WebApplication1.Repository.Interface;
using WebApplication1.Shared.Enum;

namespace WebApplication1.Repository.Impl;

public class UserOtpRepository : IUserOTpRepository
{
    private readonly AppDbContext _context;

    public UserOtpRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserOtpDataModel userOtpEntity, CancellationToken cancellationToken = default)
    {
        await _context.UserOtps.AddAsync(userOtpEntity, cancellationToken);
    }

    public void Update(UserOtpDataModel userOtpEntity, CancellationToken cancellationToken = default)
    {
        _context.UserOtps.Update(userOtpEntity);
    }

    public void SoftDelete(UserOtpDataModel userOTPEntity, CancellationToken cancellation = default)
    {
        _context.UserOtps.Update(userOTPEntity);
    }

    public async Task<UserOtpDataModel?> GetLastOtpOfType(
        Guid userId, 
        OtpType type, 
        CancellationToken cancellation = default)
    {
        return await _context.UserOtps
            .AsNoTracking() // مهم جداً لتحسين الأداء في عمليات القراءة
            .Where(x => x.userID == userId && x.OTPtype == type)
            // الترتيب تنازلياً بناءً على وقت التوليد لجلب آخر كود تم إرساله
            .OrderByDescending(x => x.GeneratedAt) 
            .FirstOrDefaultAsync(cancellation);
    }
}