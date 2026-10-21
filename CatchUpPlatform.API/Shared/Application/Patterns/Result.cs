namespace CatchUpPlatform.API.Shared.Application.Patterns;

public abstract record Result<TValue, TError>
{
    public sealed record Success(TValue value) : Result<TValue, TError>;
    public sealed record Failure(TError error) : Result<TValue, TError>;
    
    public bool IsSuccess => this is Success;
    public bool IsFailure => this is Failure;

    public Result<TNext, TError> Map<TNext>(Func<TValue, TNext> onSuccess) => this switch
    {
        Success success => new Result<TNext, TError>.Success(onSuccess(success.value)),
        Failure failure => new Result<TNext, TError>.Failure(failure.error),
        _ => throw new InvalidOperationException("Invalid Result state")
    };
    
    public TResult Fold <TResult>(Func<TValue, TResult> onSuccess, Func<TError, TResult> onFailure) => this switch
    {
        Success success => onSuccess(success.value),
        Failure failure => onFailure(failure.error),
        _ => throw new InvalidOperationException("Invalid Result state")
    };
    
    public void Match(Action<TValue> onSuccess, Action<TError> onFailure)
    {
        switch (this)
        {
            case Success success:
                onSuccess(success.value);
                break;
            case Failure failure:
                onFailure(failure.error);
                break;
            default:
                throw new InvalidOperationException("Invalid Result state");
        }
    }


}