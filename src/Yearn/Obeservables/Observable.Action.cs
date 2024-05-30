namespace Yearn.Obeservables;

public readonly ref struct Observable<TIn>(
    IEnumerable<Func<Action<TIn>, Action<TIn>>> components,
    Func<Action<TIn>, Action<TIn>>? func = null)
{
    public Observable<TIn> Use(Func<Action<TIn>, Action<TIn>> component) =>
        new(func is not null ? components.Append(func) : components, component);

    public Action<TIn> Build()
    {
        return components
            .Reverse()
            .Aggregate((Action<TIn>)Result, (current, component) => component(current));

        static void Result(TIn _) => throw new IndexOutOfRangeException();
    }
}