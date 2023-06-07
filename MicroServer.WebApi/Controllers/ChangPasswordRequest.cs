using System;
namespace MicroServer.WebApi.Controllers
{
    public record ChangePasswordRequest(Guid Id, string Password);
}

