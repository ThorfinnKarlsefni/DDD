using System;
using MicroServer.Domain;

namespace MicroServer.WebApi.Controllers
{
    public record CheckLoginByPhoneAndCodeRequest(PhoneNumber PhoneNumber, string Code);
}

