using PizzaDeliverOrder2.EntityModels;
using PizzaDeliverOrder2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Repository
{
    public interface IOrderRepository
    {
        decimal CalculateCost(PlaceOrderRequestModel placeOrderRequest);
        int InsertFinalOrder(PlaceOrderRequestModel placeOrderRequest, decimal finalCost);
        bool validateOrderExistence(int orderId);
        int DeleteOrder(int orderId);
        Orders FetchOrderDetails(int orderId);
        OrderDetails GetOrderDetailsById(int orderId);
        int AddOrRemovePizzaToExistingOrder(UpdateOrderRequestModel updateRequest);
    }
}
