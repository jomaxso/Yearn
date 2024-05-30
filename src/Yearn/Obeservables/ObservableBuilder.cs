namespace Yearn.Obeservables;

public sealed class ObservableBuilder<TDelegate> 
    where TDelegate : Delegate
{
    private readonly IEnumerable<Func<TDelegate, TDelegate>> _components;
    private readonly Func<TDelegate, TDelegate>? _nextStep;
    private readonly TDelegate? _fallback;
    
    internal ObservableBuilder(IEnumerable<Func<TDelegate, TDelegate>> components, Func<TDelegate, TDelegate>? nextStep, TDelegate? fallback)
    {
        _components = components;
        _nextStep = nextStep;
        _fallback = fallback;
    }

    public ObservableBuilder<TDelegate> New() => new(_components, _nextStep, _fallback);
    
    public ObservableBuilder<TDelegate> Use(Func<TDelegate, TDelegate> component)
    {
        var extendedComponents = _nextStep is not null 
            ? _components.Append(_nextStep) 
            : _components;
        
        return new ObservableBuilder<TDelegate>(extendedComponents, component, _fallback);
    }

    public TDelegate Build() => _components
        .Reverse()
        .Aggregate(_fallback!, (current, component) =>
        {
            if(current is null)
                throw new InvalidOperationException("A Pipeline step cannot be null. Maybe you forgot to set a fallback.");
            
            return component(current);
        });
}