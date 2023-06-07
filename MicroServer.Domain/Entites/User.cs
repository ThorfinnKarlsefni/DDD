using System;

namespace MicroServer.Domain.Entites
{
    public record User : IAggregateRoot
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public PhoneNumber PhoneNumber { get; private set; }
        public string? PasswordHash;
        public UserAccessFail AccessFail { get; private set; }

        private User() { }

        public User(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
            this.Id = Guid.NewGuid();
            this.AccessFail = new UserAccessFail(this);
        }

        public bool HasPassword()
        {
            return !string.IsNullOrEmpty(this.PasswordHash);
        }

        public void ChangePassword(string value)
        {
            if (value.Length <= 3)
                throw new ArgumentException("密码长度不能小于3");

            PasswordHash = HashHelper.ComputeMd5Hash(value);
        }

        public bool CheckPassword(string password)
        {
            return PasswordHash == HashHelper.ComputeMd5Hash(password);
        }

        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            phoneNumber = phoneNumber;
        }
    }
}

