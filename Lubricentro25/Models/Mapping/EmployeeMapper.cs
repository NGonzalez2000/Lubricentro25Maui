using Lubricentro25.Api.Contracts.Employee;
using Mapster;

namespace Lubricentro25.Models.Mapping;

public class EmployeeMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<EmployeeResponse,Employee>()
            .Map(dest => dest.Role, src => new Role() { Id = src.RoleId, Name = src.RoleName});
    }
}
