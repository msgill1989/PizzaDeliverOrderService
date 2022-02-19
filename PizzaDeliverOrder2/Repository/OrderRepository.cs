using Microsoft.Extensions.Logging;
using PizzaDeliverOrder2.EntityModels;
using PizzaDeliverOrder2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

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
        public decimal CalculateCost(PlaceOrderRequestModel placeOrderRequest)
        {
            decimal totalCost = 0;
            try
            {
                foreach (var pizza in placeOrderRequest.Pizzas)
                {
                    var pizzaPrice = (from o in _context.PizzaDetails where o.PizzaName == pizza.PizzaName select o.PizzaCost).FirstOrDefault();
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
        public int InsertFinalOrder(PlaceOrderRequestModel placeOrderRequest, decimal finalCost)
        {
            int orderId = 0;
            bool paymentResponse;
            try
            {
                using (var transaction = _context.Db.BeginTransaction())
                {
                    var newOrder = new Orders()
                    {
                        CustomerId = placeOrderRequest.CustomerId,
                        OrderDate = DateTime.Now,
                        OrderQuantity = placeOrderRequest.Pizzas.Count()
                    };

                    _context.Orders.Add(newOrder);
                    _context.SaveChanges();
                    orderId = newOrder.Id;

                    paymentResponse = ProcessPayment(placeOrderRequest.Payment, finalCost);

                    if (paymentResponse == true)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }

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
                if (_context.Orders.Any(u => u.Id == orderId))
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
                var order = (from t in _context.Orders where t.Id == orderId select t).FirstOrDefault();
                _context.Orders.Remove(order);
                response = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("Some error occured while deleting the order");
                throw;
            }
            return response;
        }

        public Orders FetchOrderDetails(int orderId)
        {
            Orders responseOrder;
            try
            {
                responseOrder = (from o in _context.Orders where o.Id == orderId select o).FirstOrDefault();
                return responseOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some error happened while fetching order details from Database.");
                throw;
            }
        }

        public OrderDetails GetOrderDetailsById(int orderId)
        {
            try
            {
                var orderDetails = (from o in _context.OrderDetails where o.Id == orderId select o).FirstOrDefault();
                return orderDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some error happened while fetching order details from Database.");
                throw;
            }
        }

        public int AddOrRemovePizzaToExistingOrder(UpdateOrderRequestModel updateRequest)
        {
            int orderId = 0;
            try
            {
                if (updateRequest.AddPizza.Count != 0)
                {
                    foreach (var pizza in updateRequest.AddPizza)
                    {
                        _context.OrderDetails.Add(new OrderDetails() { OrderId = updateRequest.OrderId, PizzaId = pizza.Id, Toppings = string.Join(',', pizza.Toppings)});
                        _context.SaveChanges();
                    }
                }

                if (updateRequest.RemovePizza.Count != 0)
                {
                    foreach (var pizzaId in updateRequest.RemovePizza)
                    {
                        var pizzasToRemove = (from o in _context.OrderDetails where o.OrderId == updateRequest.OrderId && o.PizzaId == pizzaId select o);
                        _context.OrderDetails.RemoveRange(pizzasToRemove);
                    }
                }

                if (updateRequest.UpdatePizza.Count != 0)
                {
                    foreach (var updatePizza in updateRequest.UpdatePizza)
                    {
                        var pizza = (from o in _context.OrderDetails where o.OrderId == updateRequest.OrderId && o.PizzaId == updatePizza.Id select o).FirstOrDefault();
                        _context.OrderDetails.Remove(pizza);

                        _context.OrderDetails.Add(new OrderDetails() { OrderId = updateRequest.OrderId, PizzaId = pizza.Id, Toppings = string.Join(',', pizza.Toppings)});
                    }
                }
                return orderId = updateRequest.OrderId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some error happened while adding/removing the pizza to existing order.");
                throw;
            }
        }

        private bool ProcessPayment(PaymentCard cardDetails, decimal costToDeduct)
        {
            //Write the code to deduct the money from the payment card. Here we can use payment API like strip etc.
            //For now I'm hardcoding and returning true as payment status
            return true;
        }
    }
}
