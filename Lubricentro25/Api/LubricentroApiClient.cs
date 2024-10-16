using CommunityToolkit.Mvvm.Messaging;
using Lubricentro25.Api.Contracts.Error;
using Lubricentro25.Api.Contracts.Login;
using Lubricentro25.Models.Helpers;
using Lubricentro25.Models.Helpers.Interface;
using Lubricentro25.Models.Messages;
using MapsterMapper;
using System.Text;
using System.Text.Json;

namespace Lubricentro25.Api;

public class LubricentroApiClient : ILubricentroApiClient
{
    private string? uri;
    private HttpClient? httpClient;
    private HttpClient HttpClient
    {
        get 
        {
            if(httpClient is null)
            {
                uri = Preferences.Get("ApiAddress", "");
                httpClient = new HttpClient() { BaseAddress = new Uri(uri) };
            }
            return httpClient;
        } 
    }
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly IChatConnectionHelper _connectionHelper ;
    private AuthenticationResponse? _authenticationResponse;
    private readonly IMapper _mapper;
    public LubricentroApiClient(IMapper mapper, IChatConnectionHelper connectionHelper)
    {
        _mapper = mapper;
        _connectionHelper = connectionHelper;
        _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }

    
    private static StringContent CreateContent(object request)
    {
        string json = JsonSerializer.Serialize(request);
        return new(json, Encoding.UTF8, "application/json");
    }

    private T? Deserialize<T>(string responseContent)
    {
        return JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
    }

    
    
    private async Task<HttpResponseMessage> DeleteAsync(string endpoint, object request)
    {
        HttpRequestMessage requestMessage = new(HttpMethod.Delete, endpoint)
        {
            Content = CreateContent(request)
        };
        return await HttpClient.SendAsync(requestMessage);
    }

    public async Task<ApiResponse> Delete(string endPoint, object request)
    {
        HttpResponseMessage response;
        try
        {
            response = await DeleteAsync(endPoint, request);
        }
        catch (Exception)
        {
            ErrorResponse er = new("Exception", []);
            er.Errors.Add("Unexpected", ["No se pudo establecer coneccion con el servidor."]);
            response = new()
            {
                StatusCode = System.Net.HttpStatusCode.RequestTimeout,
                Content = CreateContent(er)
            };
        }

        if (response.IsSuccessStatusCode)
        {
            return new();
        }
        var error = Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync());

        if (error is not null)
        {
            var key = error.Errors.Keys.First();
            return new(error.Errors[key].First());
        }
        return new("Error al conectarse con el servidor\nCompruebe su coneccion a internet");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"> output type</typeparam>
    /// <typeparam name="U"> api request response type</typeparam>
    /// <param name="endPoint"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResponse<T>> Post<T,U>(string endPoint, object request)
    {
        HttpResponseMessage response;
        try
        {
            var content = CreateContent(request);
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(30));
            response = await HttpClient.PostAsync(endPoint, content,cancellationTokenSource.Token);
        }
        catch (Exception)
        {
            ErrorResponse er = new("Exception", []);
            er.Errors.Add("Unexpected", ["No se pudo establecer coneccion con el servidor."]);
            response = new()
            {
                StatusCode = System.Net.HttpStatusCode.RequestTimeout,
                Content = CreateContent(er)
            };
        }

        if(response.IsSuccessStatusCode)
        {
            U? item = Deserialize<U>(await response.Content.ReadAsStringAsync());
            if(item is null)
            {
                return new("Error al descomprimir la respuesta del servidor");
            }
            T temp = _mapper.Map<T>(item);
            return new([temp]);
        }
        var error = Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync());

        if (error is not null)
        {
            string responseError = (error.Errors is null) ? error.Title : error.Errors[error.Errors.Keys.First()].First(); 
            return new(responseError);
        }
        return new("Error al conectarse con el servidor\nCompruebe su coneccion a internet");
    }
    public async Task<ApiResponse<T>> Get<T,U>(string endPoint)
    {
        HttpResponseMessage response;
        try
        {
            response = await HttpClient.GetAsync(endPoint);
        }
        catch (Exception)
        {
            ErrorResponse er = new("Exception", []);
            er.Errors.Add("Unexpected", ["No se pudo establecer coneccion con el servidor."]);
            response = new()
            {
                StatusCode = System.Net.HttpStatusCode.RequestTimeout,
                Content = CreateContent(er)
            };
        }
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<U>? list = Deserialize<IEnumerable<U>>(content);
            if (list is null)
            {
                return new("Error al descomprimir la respuesta del servidor");
            }

            return new(_mapper.Map<IEnumerable<T>>(list));
        }
        var error = Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync());

        if (error is not null)
        {
            var key = error.Errors.Keys.First();
            return new(error.Errors[key].First());
        }
        return new("Error al conectarse con el servidor\nCompruebe su coneccion a internet");
    }
    public async Task<ApiResponse> Login(LoginRequest request)
    {
        var response = await Post<AuthenticationResponse, AuthenticationResponse>("auth/login", request);
        if(response is null)
        {
            return new("Error al conectarse con el servidor\nCompruebe su coneccion a internet");
        }

        if (response.IsSuccessful)
        {
            _authenticationResponse = response.ResponseContent.First();

            if (_authenticationResponse is null)
            {
                return new ("Comuniquese con el desarrollador, Error de login");
            }
            
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authenticationResponse.Token}");

            ValidatePermissions(_authenticationResponse);
            Preferences.Set("CurrentUserEmail", _authenticationResponse.Email);
            return new();
        }
        return new(response.ErrorMessage);
    }

    private async void ValidatePermissions(AuthenticationResponse authResp)
    {
        List<string> policiesToValidate =
        [
            "EmployeeModificationsPolicy",
            "MigrationPolicy",
            "CompanyPolicy",
            "ChatPolicy"
        ];

        ApiResponse<AuthenticationHelper> response;

        foreach (string policy in policiesToValidate)
        {
            response = await ValidatePolicy(policy);

            if (response.IsSuccessful)
            {
                SendMessage(policy, response.ResponseContent.FirstOrDefault(defaultValue: new() { IsAllowed = false }).IsAllowed);
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", response.ErrorMessage, "Aceptar");
            }
        }

        response = await ValidatePolicy("ChatPolicy");


        if (response.IsSuccessful)
        {
            AuthenticationHelper authHelper = response.ResponseContent.FirstOrDefault(defaultValue: new() { IsAllowed = false });

            if(authHelper.IsAllowed)
            {

                Preferences.Set("UserId", authResp.Id);
                await _connectionHelper.Connect(authResp.Token);

                SendMessage("ChatPolicy", authHelper.IsAllowed);
            }

        }
        else
        {
            await Shell.Current.DisplayAlert("Error", response.ErrorMessage, "Aceptar");
        }

    }

    private async Task<ApiResponse<AuthenticationHelper>> ValidatePolicy(string policyName)
    {
        return await Post<AuthenticationHelper, PolicyValidationResponse>("/auth/policyVerification", new PolicyValidationRequest(policyName));
    }

    private static void SendMessage(string policyName, bool isValid)
    {
        WeakReferenceMessenger.Default.Send(new AddConfigurationPagesMessage(isValid, policyName));
    }

    public async Task<ApiResponse> PasswordRecovery(PasswordRecoveryRequest request)
    {
        string endPoint = uri + "/auth/passwordrecovery";
        var content = CreateContent(request);
        var response = await HttpClient.PostAsync(endPoint, content);
        
        if (response is null)
        {
            return new("Error al conectarse con el servidor\nCompruebe su coneccion a internet");
        }

        return new();
    }
}
