﻿using Model.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepository <T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Deleted(T entity);
        Task<T> FindAsync(string partitionKey, string rowKey);
        Task<IEnumerable<T>> FindAllByPartitionKeyAsync(string partitionKey);
        Task<IEnumerable<T>> FindAllAsync();
    }
}
