namespace PlantBasedPizza.Deliver.Infrastructure;

public record DeadLetterMessage
{
    public required string EventId { get; set; }

    public required string EventType { get; set; }

    public required string EventData { get; set; }

    public required string TraceParent { get; set; }
}