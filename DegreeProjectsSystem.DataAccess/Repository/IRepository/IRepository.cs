﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );

        T GetFirst(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );

        void Add(T entity);

        void Remove(int id);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);

    }
}
