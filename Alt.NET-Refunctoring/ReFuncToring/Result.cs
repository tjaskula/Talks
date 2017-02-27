namespace ReFuncToring
{
    public class Result
    {
        public static Result<TError, TSuccess> Success<TError, TSuccess>(TSuccess success)
        {
            return new Result<TError, TSuccess>(success);
        }

        public static Result<TError, TSuccess> Error<TError, TSuccess>(TError error)
        {
            return new Result<TError, TSuccess>(error);
        }
    }

    public class Result<TError, TSuccess>
    {
        private readonly TError _error;
        private readonly TSuccess _success;

        internal Result(TSuccess success)
        {
            _error = default(TError);
            _success = success;
            IsSuccess = true;
        }

        internal Result(TError error)
        {
            _error = error;
            _success = default(TSuccess);
            IsSuccess = false;
        }

        public TError Error
        {
            get
            {
                return _error;
            }
        }

        public TSuccess Success
        {
            get
            {
                return _success;
            }
        }

        public bool IsSuccess { get; private set; }
    }
}