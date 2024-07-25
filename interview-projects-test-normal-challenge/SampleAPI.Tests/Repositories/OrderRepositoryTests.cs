using Microsoft.EntityFrameworkCore;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Tests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
public class OrderRepositoryTests
{
  
    [Fact]
    public async Task AddNewOrder_ShouldAddOrderAndReturnNewId()
    {
        // Arrange
        using var context = MockSampleApiDbContextFactory.GenerateMockContext(); 
        var repository = new OrderRepository(context);
        var order = new Order
        {
            // Initialize order properties as needed
            Date = DateTime.UtcNow,
            Description = "description",
            Id= Guid.NewGuid(),
            OrderDeleted = true,
            OrderInvoiced = false
        };
        // Act
        var resultId = await repository.AddNewOrder(order);
        // Assert
        Assert.NotEqual(Guid.Empty, resultId);
        var addedOrder = await context.Orders.FindAsync(resultId);
        Assert.NotNull(addedOrder);
    }
    [Fact]
    public async Task GetAll_ShouldReturnAllOrders()
    {
        // Arrange
        using var context = MockSampleApiDbContextFactory.GenerateMockContext();
        var repository = new OrderRepository(context);
        var orders = new List<Order>
        {
            new Order {   Date = DateTime.UtcNow,
            Description = "description",
            Id= Guid.NewGuid(),
            OrderDeleted = true,
            OrderInvoiced = false },
            new Order {   Date = DateTime.UtcNow,
            Description = "description - 1",
            Id= Guid.NewGuid(),
            OrderDeleted = true,
            OrderInvoiced = false }
            // Add more orders as needed
        };
        foreach (var order in orders)
        {
            context.Orders.Add(order);
        }
        context.SaveChanges();
        // Act
        var resultOrders = await repository.GetAll();
        // Assert
        Assert.Equal(orders.Count, resultOrders.Count);
        // Additional assertions to verify the order of the returned list, if necessary
    }
}