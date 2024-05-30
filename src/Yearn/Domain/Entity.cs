namespace Yearn.Domain;

public abstract class Entity<T> : EventProducer
    where T : struct
{
    public T Id { get; protected init; }
}