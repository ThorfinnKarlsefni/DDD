using System;
using MicroServer.Domain.Entites;
using MicroServer.Domain.Events;

namespace MicroServer.Domain
{
    public interface IUserDomainRepository
    {
        Task<User?> FindOneAsync(PhoneNumber phoneNumber);
        Task<User?> FindOneAsync(Guid userId);
        Task AddNewLoginHistoryAnsync(PhoneNumber phoneNumer, string msg);

        Task PublishEventAsync(UserAccessResultEvent resultEvent);
        Task SavePhoneCodeAsync(PhoneNumber phoneNumber, string code);
        Task<string?> RetrievePhoneCodeAsync(PhoneNumber phoneNumber);
    }
}

