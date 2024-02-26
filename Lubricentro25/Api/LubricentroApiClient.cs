using Lubricentro25.Api.Contracts.Employee;
using Lubricentro25.Api.Contracts.Error;
using Lubricentro25.Api.Contracts.Login;
using System.Text;
using System.Text.Json;

namespace Lubricentro25.Api;

public class LubricentroApiClient : ILubricentroApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    public LubricentroApiClient(LubricentroClientOptions lubricentroClientOptions)
    {
        _httpClient = new()
        {
            BaseAddress = new Uri(lubricentroClientOptions.ApiBaseAddress)
        };
        _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }

    private static string Serialize(object request)
    {
        return JsonSerializer.Serialize(request);
    }
    private static StringContent CreateContent(string jsonString)
    {
        return new(jsonString, Encoding.UTF8, "application/json");
    }

    private T? Deserialize<T>(string responseContent)
    {
        return JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
    }

    private async Task<HttpResponseMessage> Post(string url, StringContent content)
    {
        HttpResponseMessage response;
        try
        {
            response = await _httpClient.PostAsync(url, content);
        }
        catch (Exception)
        {
            ErrorResponse er = new("Exception", []);
            er.Errors.Add("Unexpected", ["No se pudo establecer coneccion con el servidor."]);
            response = new()
            {
                StatusCode = System.Net.HttpStatusCode.RequestTimeout,
                Content = CreateContent(Serialize(er))
            };
            
            return response;
        }
        return response;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var response = await Post("auth/login", CreateContent(Serialize(request)));

        if (response.IsSuccessStatusCode)
        {
            var auth = Deserialize<AuthenticationResponse>(await response.Content.ReadAsStringAsync());

            if (auth is null)
            {
                return new(false, "Comuniquese con el desarrollador, Error de login");
            }

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {auth.Token}");
            return new(true, string.Empty);
        }
        var str = await response.Content.ReadAsStringAsync();
        var error = Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync());

        if(error is not null)
        {
            var key = error.Errors.Keys.First();
            var value = error.Errors[key].First();
            return new(false, value);
        }
        return new(false, "Error al iniciar sesion\nCompruebe su coneccion a internet");

    }

    public async Task<List<Role>?> GetAllRoles()
    {
        var response = await _httpClient.GetAsync("/role/getall");
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Role>>(responseContent, _jsonOptions);
    }

    public Task<Role> CreateRole(Role role)
    {
        throw new NotImplementedException();
    }

    public Task<Role> UpdateRole(Role role)
    {
        throw new NotImplementedException();
    }

    public Task<Role> DeleteRole(string Id)
    {
        throw new NotImplementedException();
    }


    // EMPLOYEES //
    public Task<List<Employee>> GetAllEmployees()
    {
        throw new NotImplementedException();
    }

    public async Task<Employee?> CreateEmployee(Employee employee)
    {
        var request = new CreateEmployeeRequest(employee.Role.Id, employee.FirstName, employee.LastName, employee.Email);

        var response = await Post("/employee/create",CreateContent(Serialize(request)));

        if (response.IsSuccessStatusCode)
        {
            var responseContent = Deserialize<EmployeeResponse>(await response.Content.ReadAsStringAsync());
            if(responseContent is not null)
            {
                return new()
                {
                    Id = responseContent.Id,
                    FirstName = responseContent.FirstName,
                    LastName = responseContent.LastName,
                    Email = responseContent.Email,
                    Role = new() { Id = responseContent.Id, Name = responseContent.RoleName }
                };
            }
            

        }

        return null;

    }

    public Task<Employee> UpdateEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> DeleteEmployee(string id)
    {
        throw new NotImplementedException();
    }
}
