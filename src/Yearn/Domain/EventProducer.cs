namespace Yearn.Domain;

public abstract class EventProducer
{
    private List<object>? _events;

    public IReadOnlyCollection<object> Events => _events ?? [];

    protected void RaiseEvent<TEvent>(TEvent @event) 
        where TEvent : notnull =>
        (_events ??= []).Add(@event);

    public void ClearEvents() => _events?.Clear();
}