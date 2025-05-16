using System;
using System.Linq;
using APIChallenge.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Codility.WarehouseApi
{
    public class WarehouseController : Controller
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseController(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        // Return OkObjectResult(IEnumerable<WarehouseEntry>)
        // [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _warehouseRepository.GetProductRecords();

            return Ok(products);
        }

        // Return OkResult, BadRequestObjectResult(NotPositiveQuantityMessage), or BadRequestObjectResult(QuantityTooLowMessage)
        public IActionResult SetProductCapacity(int productId, int capacity)
        {
            var NotPositiveQuantity = new NotPositiveQuantityMessage();
            var QuantityTooLow = new QuantityTooLowMessage();
            if (capacity <= 0)
            {
                return BadRequest(NotPositiveQuantity);
            }

            var found = _warehouseRepository.GetProductRecords()
                .FirstOrDefault(p => p.ProductId == productId);

            if (found is null)
            {
                return NoContent();
            }

            if (capacity < found.Quantity)
            {
                return BadRequest();
            }

            return Ok();
        }

        // Return OkResult, BadRequestObjectResult(NotPositiveQuantityMessage), or BadRequestObjectResult(QuantityTooHighMessage)
        public IActionResult ReceiveProduct(int productId, int qty)
        {
            var NotPositiveQuantity = new NotPositiveQuantityMessage();
            var QuantityTooHigh = new QuantityTooHighMessage();
            if (qty <= 0)
            {
                return BadRequest(NotPositiveQuantity);
            }

            var foundQty = _warehouseRepository.GetProductRecords()
                .FirstOrDefault(p => p.ProductId == productId);

            var foundCapacity = _warehouseRepository.GetCapacityRecords()
                .FirstOrDefault(p => p.ProductId == productId);

            if (foundQty is null)
            {
                return NoContent();
            }

            if ((qty + foundQty.Quantity) > foundCapacity.Capacity)
            {
                return BadRequest(QuantityTooHigh);
            }

            return Ok();

        }

        // Return OkResult, BadRequestObjectResult(NotPositiveQuantityMessage), or BadRequestObjectResult(QuantityTooHighMessage)
        public IActionResult DispatchProduct(int productId, int qty)
        {
            var NotPositiveQuantity = new NotPositiveQuantityMessage();
            var QuantityTooHigh = new QuantityTooHighMessage();
            if (qty <= 0)
            {
                return BadRequest(NotPositiveQuantity);
            }

            var foundQty = _warehouseRepository.GetProductRecords()
                .FirstOrDefault(p => p.ProductId == productId);

            var foundCapacity = _warehouseRepository.GetCapacityRecords()
            .FirstOrDefault(p => p.ProductId == productId);

            if ((qty + foundQty.Quantity) > foundCapacity.Capacity)
            {
                return BadRequest(QuantityTooHigh);
            }

            return Ok();
        }
    }

}