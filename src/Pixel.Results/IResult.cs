namespace RoadToCode.Models.Results
{
    public interface IResult
    {
        object Value { get; }
    } 
    
    public interface IResult<T>
    {
        T Value { get; }
    }
}