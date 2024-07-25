using Microsoft.AspNetCore.Mvc;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrdersController> _logger;
        // Add more dependencies as needed.

        public OrdersController(IOrderRepository orderRepository, ILogger<OrdersController> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
 
        [HttpGet("getorders")] // TODO: Change route, if needed.
        [ProducesResponseType(StatusCodes.Status200OK)] // TODO: Add all response types
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            try
            {
                _logger.LogInformation("Getting items from the database");
                return  await _orderRepository.GetAll();
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            finally
            {
                _logger.LogInformation("in Finally block");
            }
        }

        /// TODO: Add an endpoint to allow users to create an order using <see cref="CreateOrderRequest"/>.
        [HttpPost("saveorder")] // TODO: Change route, if needed.
        [ProducesResponseType(StatusCodes.Status201Created)]
        // TODO: Add all response types
        public async Task<IActionResult> SaveOrder([FromBody] Order order)
        {
            try
            {
                _logger.LogInformation("Saving items from the database");
                var result = await _orderRepository.AddNewOrder(order);
                return CreatedAtAction(nameof(GetOrderById), new { id = result },result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            
            finally {
                _logger.LogInformation("in Finally block");
            }
        }
        [HttpGet("GetOrderById")]
        public IActionResult GetOrderById(Guid id)
        {
            var model = _orderRepository.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }
    }
}
