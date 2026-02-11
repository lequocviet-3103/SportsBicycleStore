using Microsoft.EntityFrameworkCore;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Domain.Enum;
using SportsBicycleStore.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Repositories
{
    public class MOrderRepository : Repository<Morderdetail>, IMOrderRepository
    {
        public MOrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Morder> CreateOrder(OrderDto orderDto)
        {
            try
            {
                decimal totalAmount = 0;
                var order = new Morder
                {
                    OrderId = Guid.NewGuid().ToString(),
                    BuyerId = orderDto.BuyerId!,
                    SellerId = orderDto.SellerId!,
                    ShippingAddress = orderDto.ShippingAddress,
                    ReceiverName = orderDto.ReceiverName,
                    ReceiverPhone = orderDto.ReceiverPhone,
                    DeliveryMethod = (int)OrderDeliveryMethod.Pickup,
                    OrderStatus = (int)OrderStatus.Pending,
                    PaymentStatus = (int)PaymentStatus.Pending,
                    Note = orderDto.Note,
                    CreatedAt = DateTime.Now
                };

                var orderDetail = new List<Morderdetail>();

                foreach (var item in orderDto.Products)
                {
                    var product = await _context.Mproducts
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.ProductId == item.ProductId);
                    if (product == null)
                        throw new Exception($"Product {item.ProductId} not found");

                    if (item.Quantity <= 0)
                        throw new Exception("Quantity must be greater than 0");
                    var subtotal = product.Price * item.Quantity;

                    orderDetail.Add(new Morderdetail
                    {
                        OrderDetailId = Guid.NewGuid().ToString(),
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price,
                        Subtotal = (decimal)subtotal!,
                        Note = item.Note,
                        CreatedAt = DateTime.Now
                    });
                    totalAmount += (decimal)subtotal!;
                }
                order.TotalAmount = totalAmount;
                await _context.Morders.AddAsync(order);
                await _context.Morderdetails.AddRangeAsync(orderDetail);
                await _context.SaveChangesAsync();

                // Return a fresh instance without navigation properties loaded to avoid serialization issues
                return await _context.Morders
                .Include(o => o.Morderdetails)
                .ThenInclude(d => d.Product)
                .Include(o => o.Buyer)
                .Include(o => o.Seller)
                .FirstAsync(o => o.OrderId == order.OrderId);
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
