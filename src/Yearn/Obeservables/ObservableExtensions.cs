namespace Yearn.Obeservables;

public static class ObservableExtensions
{
    public static Observable<TIn> Use<TIn>(this Observable<TIn> observable, Action<TIn, Action<TIn>> component) =>
        observable.Use(next => context => component(context, next));

    public static Observable<TIn, TOut> Use<TIn, TOut>(this Observable<TIn, TOut> observable, Func<TIn, Func<TIn, TOut>, TOut> component) =>
        observable.Use(next => context => component(context, next));
    
    public static Task RunAsync<TContext>(this Observable<TContext, Task> observable, TContext context) => 
        observable.Build().Invoke(context);
    
    public static Task<TResult> RunAsync<TContext, TResult>(this Observable<TContext, Task<TResult>> observable, TContext context) => 
        observable.Build().Invoke(context);
    
    public static ValueTask RunAsync<TContext>(this Observable<TContext, ValueTask> observable, TContext context) => 
        observable.Build().Invoke(context);
    
    public static ValueTask<TResult> RunAsync<TContext, TResult>(this Observable<TContext, ValueTask<TResult>> observable, TContext context) => 
        observable.Build().Invoke(context);
    
    public static TResult Run<TContext, TResult>(this Observable<TContext, TResult> observable, TContext context) => 
        observable.Build().Invoke(context);
    
    public static void Run<TContext>(this Observable<TContext> observable, TContext context) => 
        observable.Build().Invoke(context);
}