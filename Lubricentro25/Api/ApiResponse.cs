namespace Lubricentro25.Api;


public class ApiResponse<T>
{
    public bool IsSuccess { get; set; } 
    public string ErrorMessage { get; set; }
    public IEnumerable<T> ResponseContent { get; set; }
    public ApiResponse(IEnumerable<T> responseContent)
    {
        IsSuccess = true;
        ErrorMessage = string.Empty;
        ResponseContent = responseContent;
    }
    public ApiResponse(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
        ResponseContent = Enumerable.Empty<T>();
    }
}

public class ApiResponse : ApiResponse<int>
{
    public ApiResponse(string errorMessage) : base(errorMessage)
    {
        
    }

    public ApiResponse() : base(Enumerable.Empty<int>())
    {

    }
}
