using System;
using MicroServer.Domain;

namespace MicroServer.WebApi.Controllers
{
    public record SendLoginByPhoneAndCodeRequest(PhoneNumber PhoneNumber);
}

