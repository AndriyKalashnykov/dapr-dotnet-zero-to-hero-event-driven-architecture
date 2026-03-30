using Moq;
using PlantBasedPizza.Deliver.Core.AssignDriver;
using PlantBasedPizza.Deliver.Core.Entities;

namespace PlantBasedPizza.Delivery.UnitTests;

public class AssignDeliveryRequestTests
{
    [Fact]
    public async Task ForValidOrder_ShouldAssignDriver()
    {
        var repository = new Mock<IDeliveryRequestRepository>();
        repository.Setup(p => p.GetDeliveryStatusForOrder(It.IsAny<string>()))
            .ReturnsAsync(new DeliveryRequest("ORD123", new Address("add1", "BT67YU")));

        var eventPublisher = new Mock<IDeliveryEventPublisher>();

        var handler = new AssignDriverRequestHandler(repository.Object);

        var result = await handler.Handle(new AssignDriverRequest()
        {
            OrderIdentifier = "ORD123",
            DriverName = "james"
        });

        Assert.NotNull(result);
        Assert.Equal("james", result.Driver);
    }

    [Fact]
    public async Task ForOrderNotFound_ShouldReturnNull()
    {
        var repository = new Mock<IDeliveryRequestRepository>();

        var eventPublisher = new Mock<IDeliveryEventPublisher>();

        var handler = new AssignDriverRequestHandler(repository.Object);

        var result = await handler.Handle(new AssignDriverRequest()
        {
            OrderIdentifier = "ORD123",
            DriverName = "james"
        });

        Assert.Null(result);
    }
}
