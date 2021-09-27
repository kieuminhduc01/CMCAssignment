using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationDBContext context)
        {
            this._context = context;
            this.entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public T GetById(object id)
        {
            return entities.Find(id);
        }
        public int Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            entities.Add(entity);
            return _context.SaveChanges();
        }
        public int Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            return _context.SaveChanges();
        }
        public int Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            entities.Remove(entity);
            return _context.SaveChanges();
        }
    }
}
