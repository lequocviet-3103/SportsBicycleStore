using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Repositories
{
        public interface IUnitOfWork : IDisposable
        {
            Task<int> SaveChangesAsync();
            IUserRepository UserRepository { get; } 
            IMProductRepository MProductRepository { get; }
            IMInspectionReportRepository MInspectionReportRepository { get; }
            IListingRepository MlistingRepository { get; }
            IMOrderRepository MOrderRepository { get; }
            IPaymentRepository PaymentRepository { get; }
            IMessageRepository MessageRepository { get; }
    }
}

