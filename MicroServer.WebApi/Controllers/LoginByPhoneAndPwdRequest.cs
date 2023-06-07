using System;
using MicroServer.Domain;

namespace MicroServer.WebApi.Controllers
{
    public record LoginByPhoneAndPwdRequrest(PhoneNumber PhoneNumber, string Password);
}

