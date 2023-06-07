using System;
using MediatR;
using MicroServer.Domain;
using MicroServer.Domain.Entites;
using MicroServer.Domain.Events;
using Microsoft.EntityFrameworkCore;
using static MicroServer.Infrastructure.ExpressionHelper;


namespace MicroServer.Infrastructure
{
    public class UserDomainRepository : IUserDomainRepository
    {
        private readonly UserDbContext _dbContext;
        private readonly IMediator _mediator;

        public UserDomainRepository(IMediator mediator, UserDbContext dbContext)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public Task AddNewLoginHistoryAnsync(PhoneNumber phoneNumer, string msg)
        {
            throw new NotImplementedException();
        }

        public Task<User?> FindOneAsync(PhoneNumber phoneNumber)
        {
            return _dbContext.Users.Include(u => u.AccessFail).SingleOrDefaultAsync(MakeEqual((User u) => u.PhoneNumber, phoneNumber));
        }

        public Task<User?> FindOneAsync(Guid userId)
        {
            return _dbContext.Users.Include(u => u.AccessFail).SingleOrDefaultAsync(u => u.Id == userId);
        }

        public Task PublishEventAsync(UserAccessResultEvent resultEvent)
        {
            throw new NotImplementedException();
        }

        public Task<string?> RetrievePhoneCodeAsync(PhoneNumber phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task SavePhoneCodeAsync(PhoneNumber phoneNumber, string code)
        {
            throw new NotImplementedException();
        }
    }
}

