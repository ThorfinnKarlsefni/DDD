using System;
namespace MicroServer.Domain
{
    public enum CheckCodeResult
    {
        OK, PhoneNumberNotFound, Lockout, CodeError
    }
}

