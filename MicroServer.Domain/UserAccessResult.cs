using System;
namespace MicroServer.Domain
{
    public enum UserAccessReuslt
    {
        OK, PhoneNumberNotFound, Lockout, NoPassword, PasswordError
    }
}

