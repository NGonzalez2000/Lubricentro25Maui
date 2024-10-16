namespace Lubricentro25.Api;


public class ApiResponse<T>
{
    public bool IsSuccessful { get; set; } 
    public string ErrorMessage { get; set; }
    public IEnumerable<T> ResponseContent { get; set; }
    public ApiResponse(IEnumerable<T> responseContent)
    {
        IsSuccessful = true;
        ErrorMessage = string.Empty;
        ResponseContent = responseContent;
    }
    public ApiResponse(string errorMessage)
    {
        IsSuccessful = false;
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
