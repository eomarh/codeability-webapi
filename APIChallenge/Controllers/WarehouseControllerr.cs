using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Codility.WarehouseApi
{
    public class WarehouseControllerr : Controller
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseControllerr(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        // Return OkObjectResult(IEnumerable<WarehouseEntry>)
        public IActionResult GetProducts()
        {
            var entries = new WarehouseEntry();
            var products = _warehouseRepository.GetProductRecords()
                .Where(p => p.Quantity > 0);
            // Console.WriteLine("Sample debug output");
            return Ok(products);
        }

        // Return OkResult, BadRequestObjectResult(NotPositiveQuantityMessage), or BadRequestObjectResult(QuantityTooLowMessage)
        public IActionResult SetProductCapacity(int productId, int capacity)
        {
            var NotPositiveQuantity = new NotPositiveQuantityMessage();
            var QuantityTooLow = new QuantityTooLowMessage();

            var product = _warehouseRepository.GetProductRecords()
                .FirstOrDefault(p => p.ProductId == productId);

            if (product is null)
            {
                BadRequest();
            }

            if (capacity <= 0)
            {
                BadRequest(NotPositiveQuantity);
            }

            if (capacity < product.Quantity)
            {
                BadRequest(QuantityTooLow);
            }

            _warehouseRepository.SetCapacityRecord(productId, capacity);

            return Ok();
        }

        // Return OkResult, BadRequestObjectResult(NotPositiveQuantityMessage), or BadRequestObjectResult(QuantityTooHighMessage)
        public IActionResult ReceiveProduct(int productId, int qty)
        {
            var NotPositiveQuantity = new NotPositiveQuantityMessage();
            var QuantityTooHigh = new QuantityTooHighMessage();

            var productQty = _warehouseRepository.GetProductRecords()
                .FirstOrDefault(p => p.ProductId == productId);

            var productCty = _warehouseRepository.GetCapacityRecords()
                .FirstOrDefault(p => p.ProductId == productId);

            if (productQty is null)
            {
                BadRequest();
            }

            if (productCty is null)
            {
                BadRequest();
            }

            if (qty <= 0)
            {
                BadRequest(NotPositiveQuantity);
            }

            if ((qty + productQty.Quantity) > productCty.Capacity)
            {
                return BadRequest(QuantityTooHigh);
            }

            _warehouseRepository.SetProductRecord(productId, qty);

            return Ok();

        }

        // Return OkResult, BadRequestObjectResult(NotPositiveQuantityMessage), or BadRequestObjectResult(QuantityTooHighMessage)
        public IActionResult DispatchProduct(int productId, int qty)
        {
            var NotPositiveQuantity = new NotPositiveQuantityMessage();
            var QuantityTooHigh = new QuantityTooHighMessage();

            var productQty = _warehouseRepository.GetProductRecords()
                .FirstOrDefault(p => p.ProductId == productId);

            if (productQty is null)
            {
                BadRequest();
            }

            if (qty <= 0)
            {
                BadRequest(NotPositiveQuantity);
            }

            if (qty > productQty.Quantity)
            {
                BadRequest(QuantityTooHigh);
            }

            _warehouseRepository.SetProductRecord(productId, qty);

            return Ok();

        }
    }
}
