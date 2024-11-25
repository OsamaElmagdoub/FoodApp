using ProjectManagementSystem.Errors;

namespace FoodApp.Abstraction
{
    public class Result
    {
        public Result(bool isSuccess, Error error)
        {
            if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public Error Error { get; } = default!;

        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);

        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
    }

    public class Result<Value> : Result
    {
        private readonly Value? _data;

        public Result(Value? data, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _data = data;
        }

        public Value Data => IsSuccess
            ? _data!
            : default!;
    }
}
