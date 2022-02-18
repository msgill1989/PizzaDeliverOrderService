using Microsoft.Extensions.Logging;
using PizzaDeliverOrder2.EntityModels;
using PizzaDeliverOrder2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IPizzaDeliveryDBContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(ILogger<OrderRepository> logger, IPizzaDeliveryDBContext context)
        {
            _context = context;
            _logger = logger;
        }
        public decimal CalculateCost(PlaceOrderModel placeOrderRequest)
        {
            decimal totalCost = 0;
            try
            {
                foreach (var pizza in placeOrderRequest.Pizzas)
                {
                    var pizzaPrice = (from o in _context.PizzaCost where o.PizzaName == pizza.PizzaName select o.PizzaPrice).FirstOrDefault();
                    totalCost += pizzaPrice;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some error happened while calculating the cost.");
                throw;
            }
            return totalCost;
        }
        public int InsertFinalOrder(PlaceOrderModel placeOrderRequest, decimal finalCost)
        {
            int orderId;
            try
            {
                var newOrder = new FinalOrders()
                {
                    CustomerId = placeOrderRequest.CustomerId,
                    CustomerName = placeOrderRequest.CustomerName,
                    OrderCost = finalCost,
                    OrderDetails = string.Join(',', placeOrderRequest.Pizzas)
                };
                _context.FinalOrders.Add(newOrder);
                _context.SaveChanges();

                orderId = newOrder.OrderId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some internal error happened while Inserting the order to database.");
                throw;
            }
            return orderId;
        }

        public bool validateOrderExistence(int orderId)
        {
            try
            {
                if (_context.FinalOrders.Any(u => u.OrderId == orderId))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Some error occured while validating the order with order Id" + orderId.ToString());
                throw;
            }
        }

        public int DeleteOrder(int orderId)
        {
            int response;
            try
            {
                var order = (from t in _context.FinalOrders where t.OrderId == orderId select t).FirstOrDefault();
                _context.FinalOrders.Remove(order);
                response = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("Some error occured while deleting the order");
                throw;
            }
            return response;
        }

        public FinalOrders FetchOrderDetails(int orderId)
        {
            FinalOrders responseOrder;
            try
            {
                responseOrder = (from o in _context.FinalOrders where o.OrderId == orderId select o).FirstOrDefault();
                return responseOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some error happened while fetching order details from Database.");
                throw;
            }
        }
    }
}
