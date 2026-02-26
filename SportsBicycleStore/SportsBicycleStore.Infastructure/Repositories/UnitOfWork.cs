using Microsoft.EntityFrameworkCore;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository UserRepository { get; }

        public IMProductRepository MProductRepository { get; }

        public IMInspectionReportRepository MInspectionReportRepository { get; }

        public IListingRepository MlistingRepository { get; }

        public IMOrderRepository MOrderRepository { get; }

        public IPaymentRepository PaymentRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
            MProductRepository = new MProductRepository(_context);
            MInspectionReportRepository = new MInspectionReportRepository(_context);
            MlistingRepository = new MlistingRepository(_context);
            MOrderRepository = new MOrderRepository(_context);
            PaymentRepository = new PaymentRepository(_context);
            MCategoryRepository = new MCategoryRepository(_context);
            MBrandRepository = new MBrandRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public IMCategoryRepository MCategoryRepository { get; }

        public IMBrandRepository MBrandRepository { get; }
    }
}
