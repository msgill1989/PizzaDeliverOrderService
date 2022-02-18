﻿using PizzaDeliverOrder2.EntityModels;
using PizzaDeliverOrder2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Repository
{
    public interface IOrderRepository
    {
        decimal CalculateCost(PlaceOrderModel placeOrderRequest);

        int InsertFinalOrder(PlaceOrderModel placeOrderRequest, decimal finalCost);

        bool validateOrderExistence(int orderId);

        int DeleteOrder(int orderId);

        FinalOrders FetchOrderDetails(int orderId);
    }
}
