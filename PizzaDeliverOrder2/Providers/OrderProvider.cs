using Microsoft.Extensions.Logging;
using PizzaDeliverOrder2.EntityModels;
using PizzaDeliverOrder2.Models;
using PizzaDeliverOrder2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly IOrderRepository _orderRepository;
        private ILogger<OrderProvider> _logger;
        public OrderProvider(ILogger<OrderProvider> logger, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public PlaceOrderResponseModel PlaceOrder(PlaceOrderRequestModel placeOrderRequest)
        {
            try
            {
                //Calculate the cost of the order
                decimal finalCost = _orderRepository.CalculateCost(placeOrderRequest);

                //Place the order and deduct money from credit card
                int orderId = _orderRepository.InsertFinalOrder(placeOrderRequest, finalCost);

                return new PlaceOrderResponseModel() { cost = finalCost, orderId = orderId, message = "Order has been placed successfully." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some error occured while placong the order.");
                throw;
            }
        }

        public DeleteOrderSuccessModel DeleteOrder(int orderId)
        {
            try
            {
                var orderExists = _orderRepository.validateOrderExistence(orderId);

                if (orderExists == false)
                {
                    throw new KeyNotFoundException();
                }

                var deletionResult = _orderRepository.DeleteOrder(orderId);
                if (deletionResult > 0)
                {
                    return new DeleteOrderSuccessModel() { Message = "Order has been succesfully deleted", OrderId = orderId };
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Some error happened while deleting the order with id {0}",orderId);
                throw;
            }
        }

        public Orders GetOrderDetails(int orderId)
        {
            Orders orderDetails;
            try
            {
                orderDetails = _orderRepository.FetchOrderDetails(orderId);

                if (orderDetails != null)
                {
                    return orderDetails;
                }
                else
                { 
                    throw new KeyNotFoundException();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some error happened while fetching the order details;");
                throw;
            }
        }

        public UpdateResponseModel UpdateOrder(UpdateOrderRequestModel updateRequest)
        {
            try
            {
                //Add/Remove pizza to existing Order
                var response = _orderRepository.AddOrRemovePizzaToExistingOrder(updateRequest);
                if (response == 0)
                {
                    throw new Exception();
                }
                return new UpdateResponseModel() { orderId = response, message = "Order has been successfully updated."};
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some error happened while updating the order");
                throw;
            }
        }
    }
}
