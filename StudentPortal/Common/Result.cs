namespace StudentPortal.Common
{
    public sealed record Result<T>(bool IsSuccess,string? Error,T? Data)
    {
        public static Result<T> Ok(T data) => new(true, null, data);
        public static Result<T> Fail(string error) => new(false, error, default);
    }

    public sealed record Result(bool IsSuccess,string? Error)
    {
        public static Result Ok() => new(true, null);
        public static Result Fail(string error) => new(false, error);
    }
}
