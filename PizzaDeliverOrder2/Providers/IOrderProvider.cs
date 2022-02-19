using PizzaDeliverOrder2.EntityModels;
using PizzaDeliverOrder2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Providers
{
    public interface IOrderProvider
    {
        PlaceOrderResponseModel PlaceOrder(PlaceOrderRequestModel placeOrderRequest);

        DeleteOrderSuccessModel DeleteOrder(int orderId);
        Orders GetOrderDetails(int orderId);
        UpdateResponseModel UpdateOrder(UpdateOrderRequestModel updateRequest);
    }
}
