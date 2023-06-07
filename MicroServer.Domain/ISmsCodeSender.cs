using System;
namespace MicroServer.Domain
{
    public interface ISmsCodeSender
    {
        Task SendCodeAsync(PhoneNumber phoneNumber, string code);
    }
}

