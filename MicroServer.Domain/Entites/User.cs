using System;
using MicroServer.Domain.ValueObjects;
using Zack.Commons;

namespace MicroServer.Domain.Entites
{
    public record User : IAggregateRoot
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public PhoneNumber PhoneNumber { get; private set; }
        public string? passwordHash;
        public UserAccessFail userAccessFail { get; private set; }

        private User() { }

        public User(PhoneNumber phoneNumber)
        {
            phoneNumber = phoneNumber;
            this.Id = Guid.NewGuid();
            this.userAccessFail = new UserAccessFail(this);
        }

        public bool HasPassword()
        {
            return !string.IsNullOrEmpty(this.passwordHash);
        }

        public void ChangePassword(string value)
        {
            if (value.Length <= 3)
                throw new ArgumentException("密码长度不能小于3");

            passwordHash = HashHelper.ComputeMd5Hash(value);
        }

        public bool CheckPassword(string password)
        {
            return passwordHash == HashHelper.ComputeMd5Hash(password);
        }

        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            phoneNumber = phoneNumber;
        }
    }
}

