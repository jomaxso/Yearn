namespace Yearn.Obeservables;

public readonly ref struct Observable<TIn, TOut>(
    IEnumerable<Func<Func<TIn, TOut>, Func<TIn, TOut>>> components,
    Func<Func<TIn, TOut>, Func<TIn, TOut>>? func = null)
{
    public Observable<TIn, TOut> Use(Func<Func<TIn, TOut>, Func<TIn, TOut>> component) =>
        new(func is not null ? components.Append(func) : components, component);
    
    public Func<TIn, TOut> Build()
    {
        return components
            .Reverse()
            .Aggregate((Func<TIn, TOut>)Result, (current, component) => component(current));

        static TOut Result(TIn _) => throw new IndexOutOfRangeException();
    }
}