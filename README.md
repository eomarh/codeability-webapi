## Ejercicio (objetivo):

- The API should expose the following methods:
  - GetProducts() – this method returns all products that are currently stored in the warehouse;
  - SetProductCapacity(productId, capacity) – this method sets the maximum storage capacity for specified product;
  - ReceiveProduct(productId, qty) – this method increments the current quantity of a product stored in the warehouse;
  - DispatchProduct(productId, qty) – this method decrements the current quantity of a product stored in the warehouse.

### Initial files
- The initial files are only the listed below:
  - Models.Products.cs
  - Repositories.IWarehouseRepository.cs
  - Repositories.WarehouseRepository.cs
  - Repositories.WarehouseEntry.cs

I added an AddScopped context to DI from IWarehouseRepository to WarehouseRepository, where is implemented the interface