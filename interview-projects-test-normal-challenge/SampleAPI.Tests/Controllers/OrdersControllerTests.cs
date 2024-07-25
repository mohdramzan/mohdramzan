using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SampleAPI.Controllers;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
public class OrdersControllerTests
{
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<ILogger<OrdersController>> _mockLogger;
    private readonly OrdersController _controller;
    public OrdersControllerTests()
    {
        _mockOrderRepository = new Mock<IOrderRepository>();
        _mockLogger = new Mock<ILogger<OrdersController>>();
        _controller = new OrdersController(_mockOrderRepository.Object, _mockLogger.Object);
    }
    [Fact]
    public async Task GetOrders_ReturnsAllOrders()
    {
        // Arrange
        var orders = new List<Order>
        {
            new Order {  Date = DateTime.UtcNow,
            Description = "description",
            Id= Guid.NewGuid(),
            OrderDeleted = true,
            OrderInvoiced = false },
            new Order {  Date = DateTime.UtcNow,
            Description = "description",
            Id= Guid.NewGuid(),
            OrderDeleted = true,
            OrderInvoiced = false }
            // Add more orders as needed
        };
        _mockOrderRepository.Setup(repo => repo.GetAll()).ReturnsAsync(orders);
        // Act
        var result = await _controller.GetOrders();
        // Assert
        Assert.Equal(orders.Count, result?.Value?.Count);
    }
    [Fact]
    public async Task SaveOrder_ReturnsNewOrderId()
    {
        // Arrange
        var order = new Order
        {
            Date = DateTime.UtcNow,
            Description = "description",
            Id = Guid.NewGuid(),
            OrderDeleted = true,
            OrderInvoiced = false
        };
        var newOrderId = Guid.NewGuid();
        _mockOrderRepository.Setup(repo => repo.AddNewOrder(It.IsAny<Order>())).ReturnsAsync(newOrderId);
        // Act
        var result = await _controller.SaveOrder(order);
  
        // Assert
        Assert.Equal(newOrderId, result.Value);
    }
}