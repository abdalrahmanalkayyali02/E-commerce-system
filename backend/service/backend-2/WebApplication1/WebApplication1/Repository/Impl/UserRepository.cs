using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Model;
using WebApplication1.Repository.Interface;
using WebApplication1.Shared.Collection;
using WebApplication1.Shared.Collection.Impl;
using WebApplication1.Shared.Enum;

namespace WebApplication1.Repository.Impl;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserDataModel user, CancellationToken cancellation = default)
    {
        await _context.Users.AddAsync(user, cancellation);
    }

    public void Update(UserDataModel user, CancellationToken cancellation = default)
    {
        _context.Users.Update(user);
    }

    public void SoftDelete(UserDataModel user, CancellationToken cancellation = default)
    {
        user.isDelete = true;
        _context.Users.Update(user);
    }

    public async Task<UserDataModel?> GetUserById(Guid id, CancellationToken ct = default) =>
        await _context.Users.FirstOrDefaultAsync(u => u.id == id, ct);

    public async Task<UserDataModel?> GetUserByEmail(string email, CancellationToken ct = default) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

    public async Task<UserDataModel?> GetUserByPhoneNumber(string phoneNumber, CancellationToken ct = default) =>
        await _context.Users.FirstOrDefaultAsync(u => u.phoneNumber == phoneNumber, ct);

    public async Task<UserDataModel?> GetUserByUserName(string userName, CancellationToken ct = default) =>
        await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName, ct);

    public async Task<UserDataModel?> GetUserByCriteria(Guid? id = null, string? userName = null,
        string? phoneNumber = null, string? email = null, CancellationToken ct = default)
    {
        var query = _context.Users.AsNoTracking();

        if (id.HasValue) return await query.FirstOrDefaultAsync(u => u.id == id, ct);
        if (!string.IsNullOrEmpty(userName)) return await query.FirstOrDefaultAsync(u => u.UserName == userName, ct);
        if (!string.IsNullOrEmpty(phoneNumber)) return await query.FirstOrDefaultAsync(u => u.phoneNumber == phoneNumber, ct);
        if (!string.IsNullOrEmpty(email)) return await query.FirstOrDefaultAsync(u => u.Email == email, ct);

        return null;
    }

    public async Task<PagedList<UserDataModel>> GetUsersByCriteria(
        Guid? id = null,
        string? userName = null,
        string? phoneNumber = null,
        string? email = null,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        var query = _context.Users.AsNoTracking();

        if (id.HasValue) query = query.Where(u => u.id == id);
        if (!string.IsNullOrEmpty(userName)) query = query.Where(u => u.UserName.Contains(userName));
        if (!string.IsNullOrEmpty(phoneNumber)) query = query.Where(u => u.phoneNumber.Contains(phoneNumber));
        if (!string.IsNullOrEmpty(email)) query = query.Where(u => u.Email.Contains(email));

        return await PagedList<UserDataModel>.CreateAsync(query, pageNumber, pageSize, ct);
    }

    public async Task<IEnumerable<UserDataModel>> GetAllUsersAsync(
        Expression<Func<UserDataModel, bool>>? predicate = null, 
        CancellationToken cancellationToken = default)
    {
        var query = _context.Users.AsNoTracking();
        
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IPagedList<UserDataModel>> GetUsersByFilterAsync(
        UserType? type, 
        AccountStatus? accountStatus, 
        bool? isDeleted, 
        int pageNumber, 
        int pageSize, 
        CancellationToken cancellationToken)
    {
        var query = _context.Users.AsNoTracking();

        if (type.HasValue) query = query.Where(u => u.Role == type);
        if (accountStatus.HasValue) query = query.Where(u => u.AccountStatus == accountStatus);
        if (isDeleted.HasValue) query = query.Where(u => u.isDelete == isDeleted);

        // Standard paging logic using a generic PagedList implementation
        return await PagedList<UserDataModel>.CreateAsync(query, pageNumber, pageSize, cancellationToken);
    }
}