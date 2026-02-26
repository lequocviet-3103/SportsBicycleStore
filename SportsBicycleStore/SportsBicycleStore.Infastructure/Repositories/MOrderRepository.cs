using Microsoft.AspNetCore.Http.HttpResults;
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

        private static readonly Dictionary<OrderStatus, List<OrderStatus>> AllowedTransitions =
        new()
        {
            { OrderStatus.Pending, new() { OrderStatus.Paid, OrderStatus.Cancelled } },
            //{ OrderStatus.Confirmed, new() { OrderStatus.Paid, OrderStatus.Cancelled } },
            { OrderStatus.Paid, new() { OrderStatus.Completed, OrderStatus.Refunded } },
            { OrderStatus.Completed, new() { OrderStatus.Refunded } },
            { OrderStatus.Cancelled, new() },
            { OrderStatus.Refunded, new() }
        };

        private static readonly Dictionary<OrderPaymentStatus, List<OrderPaymentStatus>> AllowedOrderPaymentStatusTransitions =
        new()
        {
            { OrderPaymentStatus.Unpaid, new() { OrderPaymentStatus.Paid,} },
            { OrderPaymentStatus.Paid, new() { OrderPaymentStatus.Refunded } },
            { OrderPaymentStatus.Refunded, new() { } },
        };

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
                    DeliveryMethod = (int)OrderDeliveryMethod.Delivery,
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

                
                return order;
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Morder?> GetOrderByIdAsync(string orderId)
        {
            var order = await _context.Morders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            return order;
        }

        public Morder UpdateOrderStatus(string orderId, OrderStatus newStatus)
        {
            var order =  _context.Morders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                throw new Exception("Order not found");

            // Convert int to OrderStatus enum
            var currentStatus = (OrderStatus)order.OrderStatus;

            if (currentStatus == newStatus)
                return order;

            if (!AllowedTransitions[currentStatus].Contains(newStatus))
            {
                throw new InvalidOperationException(
                    $"Cannot change status from {currentStatus} to {newStatus}");
            }

            order.OrderStatus = (int)newStatus;
            order.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return order;
        }

        public Morder UpdateOrderPaymentStatus(string orderId, OrderPaymentStatus newStatus)
        {
            var order = _context.Morders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                throw new Exception("Order not found");

            // Convert int to OrderStatus enum
            var currentStatus = (OrderPaymentStatus)order.PaymentStatus;

            if (currentStatus == newStatus)
                return order;

            if (!AllowedOrderPaymentStatusTransitions[currentStatus].Contains(newStatus))
            {
                throw new InvalidOperationException(
                    $"Cannot change status from {currentStatus} to {newStatus}");
            }

            order.PaymentStatus = (int)newStatus;
            order.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return order;
        }

        public async Task<List<GetAllOrderDto>> GetAllOrderDtos()
        {
            var order = await _context.Morders.AsNoTracking()
                .Join(_context.Morderdetails.AsNoTracking(),
                o => o.OrderId,
                od => od.OrderId,
                (o, od) => new {o, od})
                .Join(_context.Mproducts.AsNoTracking(),
                x => x.od.ProductId,
                p => p.ProductId,
                (x, p) => new {x.o, x.od, p})
                .Join(_context.Musers.AsNoTracking(),
                x => x.o.BuyerId,
                u => u.UserId,
                (x, u) => new {x.o, x.od, x.p, u})
                .Select( x => new GetAllOrderDto
                {
                    OrderId = x.o.OrderId,
                    BuyerId = x.o.BuyerId,
                    SellerId = x.o.SellerId,
                    TotalAmount = x.o.TotalAmount,
                    ShippingAddress = x.o.ShippingAddress,
                    ReceiverName = x.o.ReceiverName,
                    ReceiverPhone = x.o.ReceiverPhone,
                    DeliveryMethod = x.o.DeliveryMethod,
                    OrderStatus = x.o.OrderStatus,
                    PaymentStatus = x.o.PaymentStatus,
                    Note = x.o.Note,
                    CreatedAt = x.o.CreatedAt,

                    // ===== ORDER DETAIL =====
                    OrderDetailId = x.od.OrderDetailId,
                    Quantity = x.od.Quantity,
                    UnitPrice = x.od.UnitPrice,
                    Subtotal = x.od.Subtotal,

                    // ===== PRODUCT =====
                    ProductId = x.p.ProductId,
                    ProductName = x.p.ProductName,
                    Price = x.p.Price,

                    // ===== USER (BUYER) =====
                    UserId = x.u.UserId,
                    UserName = x.u.UserName,
                    FullName = x.u.FullName
                }
            ).ToListAsync();
            return order;
        }

        public async Task<GetAllOrderDto?> GetOrderById(string orderId)
        {
            var order = await _context.Morders.AsNoTracking()
                .Join(_context.Morderdetails.AsNoTracking(),
                o => o.OrderId,
                od => od.OrderId,
                (o, od) => new { o, od })
                .Join(_context.Mproducts.AsNoTracking(),
                x => x.od.ProductId,
                p => p.ProductId,
                (x, p) => new { x.o, x.od, p })
                .Join(_context.Musers.AsNoTracking(),
                x => x.o.BuyerId,
                u => u.UserId,
                (x, u) => new { x.o, x.od, x.p, u })
                .Where(x => x.o.OrderId == orderId)
                .Select(x => new GetAllOrderDto
                {
                    OrderId = x.o.OrderId,
                    BuyerId = x.o.BuyerId,
                    SellerId = x.o.SellerId,
                    TotalAmount = x.o.TotalAmount,
                    ShippingAddress = x.o.ShippingAddress,
                    ReceiverName = x.o.ReceiverName,
                    ReceiverPhone = x.o.ReceiverPhone,
                    DeliveryMethod = x.o.DeliveryMethod,
                    OrderStatus = x.o.OrderStatus,
                    PaymentStatus = x.o.PaymentStatus,
                    Note = x.o.Note,
                    CreatedAt = x.o.CreatedAt,

                    // ===== ORDER DETAIL =====
                    OrderDetailId = x.od.OrderDetailId,
                    Quantity = x.od.Quantity,
                    UnitPrice = x.od.UnitPrice,
                    Subtotal = x.od.Subtotal,

                    // ===== PRODUCT =====
                    ProductId = x.p.ProductId,
                    ProductName = x.p.ProductName,
                    Price = x.p.Price,

                    // ===== USER (BUYER) =====
                    UserId = x.u.UserId,
                    UserName = x.u.UserName,
                    FullName = x.u.FullName
                }
            ).FirstOrDefaultAsync();
            return order;
        }

        public async Task<GetAllOrderDto?> GetOrderByUserId(string userId)
        {
            var order = await _context.Morders.AsNoTracking()
                .Join(_context.Morderdetails.AsNoTracking(),
                o => o.OrderId,
                od => od.OrderId,
                (o, od) => new { o, od })
                .Join(_context.Mproducts.AsNoTracking(),
                x => x.od.ProductId,
                p => p.ProductId,
                (x, p) => new { x.o, x.od, p })
                .Join(_context.Musers.AsNoTracking(),
                x => x.o.BuyerId,
                u => u.UserId,
                (x, u) => new { x.o, x.od, x.p, u })
                .Where(x => x.u.UserId == userId)
                .Select(x => new GetAllOrderDto
                {
                    OrderId = x.o.OrderId,
                    BuyerId = x.o.BuyerId,
                    SellerId = x.o.SellerId,
                    TotalAmount = x.o.TotalAmount,
                    ShippingAddress = x.o.ShippingAddress,
                    ReceiverName = x.o.ReceiverName,
                    ReceiverPhone = x.o.ReceiverPhone,
                    DeliveryMethod = x.o.DeliveryMethod,
                    OrderStatus = x.o.OrderStatus,
                    PaymentStatus = x.o.PaymentStatus,
                    Note = x.o.Note,
                    CreatedAt = x.o.CreatedAt,

                    // ===== ORDER DETAIL =====
                    OrderDetailId = x.od.OrderDetailId,
                    Quantity = x.od.Quantity,
                    UnitPrice = x.od.UnitPrice,
                    Subtotal = x.od.Subtotal,

                    // ===== PRODUCT =====
                    ProductId = x.p.ProductId,
                    ProductName = x.p.ProductName,
                    Price = x.p.Price,

                    // ===== USER (BUYER) =====
                    UserId = x.u.UserId,
                    UserName = x.u.UserName,
                    FullName = x.u.FullName
                }
            ).FirstOrDefaultAsync();
            return order;
        }
    }
}
