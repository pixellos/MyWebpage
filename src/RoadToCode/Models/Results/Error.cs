namespace RoadToCode.Models.Results
{
    public class Exist<T> : Error<T>
    {
        public Exist(T t) : this(t, string.Empty)
        {
        }

        public Exist(T t, string msg) : base(t, msg)
        {

        }
    }

    public class ArgumentError<T> : Error<T>
    {
        public ArgumentError(T t) : this(t, string.Empty)
        {
        }

        public ArgumentError(T t, string msg) : base(t, msg)
        {

        }
    }

    public class Error<T> : Error
    {
        public T Value { get; }

        public Error(T t) : this(t, string.Empty)
        {
        }

        public Error(T t, string msg) : base(msg)
        {
            this.Value = t;
        }
    }
    public class Error : Result
    {
        public string Message { get; }
        
        public Error()
        {
        }

        public Error(string message)
        {
            this.Message = message;
        }
    }
}