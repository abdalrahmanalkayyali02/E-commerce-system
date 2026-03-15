using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Abstraction.Reposotries
{
    public interface IReadReposotries<T> where T : class
    {
        public Task<T> GetById(Guid id,CancellationToken cancellation);
        public Task<IEnumerable<T>> GetAll(CancellationToken cancellation);
    }


}
