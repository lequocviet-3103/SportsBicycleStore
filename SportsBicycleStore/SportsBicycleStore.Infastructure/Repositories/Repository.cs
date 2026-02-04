using Microsoft.EntityFrameworkCore;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);  
        }

        public void Update(T entity)
        {
            _context.Update(entity);          
        }

        public void Remove(T entity)
        {
            _context.Remove(entity);
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>().AsQueryable(); 
        }
    }

}
