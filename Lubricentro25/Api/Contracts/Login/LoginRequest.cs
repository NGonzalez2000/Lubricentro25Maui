﻿namespace Lubricentro25.Api.Contracts.Login
{
    public record LoginRequest(string Email, string Password, string BranchId)
    {
    }
}
