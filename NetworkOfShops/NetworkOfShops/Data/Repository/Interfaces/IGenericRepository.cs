﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetworkOfShops.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<T> GetBy(Expression<Func<T, bool>> predicate, string includeProperties = "");
        Task Insert(T entity);
        Task Delete(object id);
        void Delete(T entityToDelete);
        void Update(T entityToUpdate);
        Task Save();
    }
}
