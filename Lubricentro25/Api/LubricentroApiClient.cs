﻿using Lubricentro25.Api.Contracts.Error;
using Lubricentro25.Api.Contracts.Login;
using MapsterMapper;
using System.Text;
using System.Text.Json;

namespace Lubricentro25.Api;

public class LubricentroApiClient : ILubricentroApiClient
{
    private HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private AuthenticationResponse? _authenticationResponse;
    private readonly IMapper _mapper;
    public LubricentroApiClient(IMapper mapper)
    {
        _mapper = mapper;
        _httpClient = new();
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
        return await _httpClient.SendAsync(requestMessage);
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
            response = await _httpClient.PostAsync(endPoint, content,cancellationTokenSource.Token);
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
            var key = error.Errors.Keys.First();
            return new(error.Errors[key].First());
        }
        return new("Error al conectarse con el servidor\nCompruebe su coneccion a internet");
    }
    public async Task<ApiResponse<T>> Get<T,U>(string endPoint)
    {
        HttpResponseMessage response;
        try
        {
            response = await _httpClient.GetAsync(endPoint);
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
            IEnumerable<U>? list = Deserialize<IEnumerable<U>>(await response.Content.ReadAsStringAsync());
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
        string uri = Preferences.Get("ApiAddress", "");

        if (string.IsNullOrEmpty(uri))
        {
            return new("Compruebe el string de conexión.");
        }

        _httpClient = new()
        {
            BaseAddress = new Uri(uri)
        };
        var response = await Post<AuthenticationResponse, AuthenticationResponse>("auth/login", request);
        if(response is null)
        {
            return new("Error al conectarse con el servidor\nCompruebe su coneccion a internet");
        }

        if (response.IsSuccess)
        {
            _authenticationResponse = response.ResponseContent.First();

            if (_authenticationResponse is null)
            {
                return new ("Comuniquese con el desarrollador, Error de login");
            }
            
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authenticationResponse.Token}");
            return new();
        }
        return new(response.ErrorMessage);
    }

    public AuthenticationResponse? GetAuthentication()
    {
        return _authenticationResponse;
    }
}
