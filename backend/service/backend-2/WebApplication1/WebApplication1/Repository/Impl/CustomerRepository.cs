using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Model;
using WebApplication1.Repository.Interface;

namespace WebApplication1.Repository.Impl;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CustomerDataModel entity, CancellationToken cancellation = default)
    {
        // تعيين تاريخ الإنشاء بشكل تلقائي إذا لم يكن معيناً
        if (entity.CreateAt == default)
            entity.CreateAt = DateTime.UtcNow;

        await _context.Customers.AddAsync(entity, cancellation);
    }

    public void Update(CustomerDataModel entity, CancellationToken cancellation = default)
    {
        // تحديث تاريخ التعديل
        entity.UpdateAt = DateTime.UtcNow;
        
        _context.Customers.Update(entity);
    }

    public async Task<CustomerDataModel?> GetUserById(Guid id, CancellationToken ct = default)
    {
        // استخدام AsNoTracking لتحسين الأداء بما أنها عملية قراءة فقط
        return await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CustomrID == id, ct);
    }
}