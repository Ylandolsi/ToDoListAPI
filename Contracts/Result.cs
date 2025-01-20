using System.Runtime.InteropServices.JavaScript;
using Models.Error;

namespace Contracts;

public class Result <TValue> where TValue: class
{
    private readonly TValue? _value; 
    private readonly Error _error;
    
    public readonly bool _isSuccess; 
    
    private Result(TValue value)
    {
        _value = value;
        _error = Error.None;
        _isSuccess = true;
    }

    private Result(Error error)
    {
        _value = default;
        _error = error;
        _isSuccess = false ;
    }
    public static Result<TValue> Success ( TValue value) => new (value);
    public static Result<TValue> Failure ( Error error) => new (error);

}
