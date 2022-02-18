using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaDeliverOrder2.EntityModels;
using PizzaDeliverOrder2.Models;
using PizzaDeliverOrder2.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderProvider _orderProvider;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderProvider orderProvider, ILogger<OrderController> logger)
        {
            _orderProvider = orderProvider;
            _logger = logger;
        }

        [HttpPost("PlaceOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlaceOrderResponseModel> PlaceOrder([FromQuery] PlaceOrderModel? placeOrderRequest)
        {
            try
            {
                return _orderProvider.PlaceOrder(placeOrderRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Some Internal server error happened while placing the order.");
                var details = ProblemDetailsFactory.CreateProblemDetails(HttpContext, 500, "Internal Server Error", null, "Error while placing the order");
                return StatusCode(500, details);
            }
        }

        [HttpDelete("DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DeleteOrderSuccessModel> DeleteOrder(int orderId)
        {
            DeleteOrderSuccessModel response;
            try
            {
                response = _orderProvider.DeleteOrder(orderId);
                return response;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("The order with id " + orderId.ToString() + " can not be found and hence can not be deleted.");
                var details = ProblemDetailsFactory.CreateProblemDetails(HttpContext, 404, "Not Found", null, "Order not found");
                return StatusCode(404, details);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some Internal server error happened while deleting the order.");
                var details = ProblemDetailsFactory.CreateProblemDetails(HttpContext, 500, "Internal Server Error", null, "Error while deleting the order");
                return StatusCode(500, details);
            }
        }

        [HttpGet("GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FinalOrders> GetOrder(int orderId)
        {
            try
            {
                return _orderProvider.GetOrderDetails(orderId);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("The order with id " + orderId.ToString() + " is not found.");
                var details = ProblemDetailsFactory.CreateProblemDetails(HttpContext, 404, "Not Found", null, "Order not found");
                return StatusCode(404, details);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some Internal server error happened while fetching the order details.");
                var details = ProblemDetailsFactory.CreateProblemDetails(HttpContext, 500, "Internal Server Error", null, "Error while fetching the order details.");
                return StatusCode(500, details);
            }
        }

        [HttpPatch("UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> UpdateOrder()
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some Internal server error happened while updating the order.");
                var details = ProblemDetailsFactory.CreateProblemDetails(HttpContext, 500, "Internal Server Error", null, "Error while updating the order.");
                return StatusCode(500, details);
            }
            return "abc";
        }
    }
}
