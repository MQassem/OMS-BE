using Microsoft.AspNetCore.Mvc;
using Repository.Model;
using Service.DTO;
using Service.Intercaces;

namespace OMS.Solution
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _service;
        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [Route("GetOrders")]
        [HttpGet]
        public ActionResult<Order> GetOrders()
        {
            var orders = _service.GetAll();
            if (orders.Count() > 0)
                return Ok(orders);
            else
                return NotFound("Error: No Orders found.");
        }

        [Route("GetOrder/{id}")]
        [HttpGet]
        public ActionResult<OrderDTO> GetOrder(int id)
        {
            if (id == 0)
                return BadRequest("Id can't be empty");
            var orderDto = _service.GetById(id);
            if (orderDto != null)

                return Ok(orderDto);
            else
                return NotFound($"Error: Order with ID {id} not found.");
        }

        [Route("AddOrder")]
        [HttpPost]
        public ActionResult AddOrder(OrderDTO orderRequest)
        {
            if (orderRequest == null)
                return BadRequest("Order can't be Empty!");
            try
            {
                if (_service.Add(orderRequest))
                    return Created("", new { message = $"New Order added for client  {orderRequest.ClientId.ToString()}" });
                else
                    return BadRequest(new { message = "Nothing Added" });
            }
            catch (Exception ex)
            {
                var message = ex.ToString();
                return Problem($"Error in adding order: {ex.Message}");
            }
        }
        [Route("UpdateOrder")]
        [HttpPost]
        public ActionResult UpdateOrder(OrderDTO orderRequest)
        {
            if (orderRequest == null)
                return BadRequest("Order can't be Empty!");
            try
            {
                if (_service.Update(orderRequest))
                    return Ok($" Order {orderRequest.Id.ToString()} updated");
                else
                    return BadRequest("Nothing Added");
            }
            catch (Exception ex)
            {

                return Problem($"Error in adding order: {ex.Message}");
            }
        }

        [Route("DeleteOrder/{id}")]
        [HttpDelete]
        public ActionResult DeleteOrder(int id)
        {
            if (id == 0)
                return BadRequest("Id can't be empty");
            var result = _service.Delete(id);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound($"Error: Order with ID {id} not found.");
            }
        }

    }
}
