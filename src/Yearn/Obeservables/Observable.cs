namespace Yearn.Obeservables;

public static class Observable
{
    public static ObservableBuilder<TDelegate> CreateBuilder<TDelegate>(TDelegate? fallback = null)
        where TDelegate : Delegate =>
        new([], null, null);

    public static Observable<TIn> Create<TIn>() => new([]);
    public static Observable<TIn, TOut> Create<TIn, TOut>() => new([]);
}