using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Common.Abstraction.Reposotries
{
    public interface IWriteReposotries<T> where T : class
    {
        public Task AddAsync(T item, CancellationToken cancellation = default);
        public void Update(T item, CancellationToken cancellation = default);
        public void SoftDelete(Guid id, CancellationToken cancellation = default);
        public Task DeleteAsync(Guid id, CancellationToken cancellation = default);
    }


}
