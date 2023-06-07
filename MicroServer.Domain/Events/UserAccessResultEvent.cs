using System;
using MediatR;

namespace MicroServer.Domain.Events
{
    public record class UserAccessResultEvent(PhoneNumber PhoneNumber, UserAccessReuslt Reuslt) : INotification;
}

