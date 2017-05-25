namespace RoadToCode.Models.Results
{
    public class Error<T> : Error
    {
        public new T Value => (T)base.Value;

        public Error(T t) : this(t, string.Empty)
        {
        }

        public Error(T t, string msg) : base(t, msg)
        {
        }
    }

    public class Error : Result
    {
        public string Message { get; }
        public object Value { get; }

        public Error()
        {
        }

        public Error(object value, string message)
        {
            this.Value = value;
            this.Message = message;
        }
        public Error(string message)
        {
            this.Message = message;
        }
    }
}