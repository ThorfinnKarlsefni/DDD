using System;
using MicroServer.Domain.Entites;
using MicroServer.Domain.Events;

namespace MicroServer.Domain
{
    public class UserDomainService
    {
        private readonly IUserDomainRepository _repository;
        private readonly ISmsCodeSender _smsCodeSender;

        public UserDomainService(IUserDomainRepository repository, ISmsCodeSender smsCodeSender)
        {
            _repository = repository;
            _smsCodeSender = smsCodeSender;
        }

        public async Task<UserAccessReuslt> CheckLoginAsync(PhoneNumber phoneNumber, string password)
        {
            User? user = await _repository.FindOneAsync(phoneNumber);
            UserAccessReuslt result;
            if (user == null)
            {
                result = UserAccessReuslt.PhoneNumberNotFound;
            }
            else if (IsLockOut(user))
            {
                result = UserAccessReuslt.Lockout;
            }
            else if (user.HasPassword() == false)
            {
                result = UserAccessReuslt.PasswordError;
            }
            else if (user.CheckPassword(password))
            {
                result = UserAccessReuslt.OK;
            }
            else
            {
                result = UserAccessReuslt.PasswordError;
            }

            if (user != null)
            {
                if (result == UserAccessReuslt.OK)
                {
                    this.ResetAccessFail(user);
                }
                else
                {
                    this.AccessFail(user);
                }
            }

            UserAccessResultEvent eventItem = new(phoneNumber, result);
            await _repository.PublishEventAsync(eventItem);
            return result;

        }

        public async Task<UserAccessReuslt> SendCodeAsync(PhoneNumber phone)
        {
            var user = await _repository.FindOneAsync(phone);
            if (user == null)
                return UserAccessReuslt.PhoneNumberNotFound;
            if (IsLockOut(user))
                return UserAccessReuslt.Lockout;

            string code = Random.Shared.Next(1000, 9999).ToString();
            await _repository.SavePhoneCodeAsync(phone, code);
            await _smsCodeSender.SendCodeAsync(phone, code);
            return UserAccessReuslt.OK;
        }

        public async Task<CheckCodeResult> CheckCodeAsync(PhoneNumber phone, string code)
        {
            var user = await _repository.FindOneAsync(phone);
            if (user == null)
                return CheckCodeResult.PhoneNumberNotFound;
            if (IsLockOut(user))
                return CheckCodeResult.Lockout;

            string codeInServer = await _repository.RetrievePhoneCodeAsync(phone);

            if (string.IsNullOrEmpty(codeInServer))
                return CheckCodeResult.CodeError;
            if (code == codeInServer)
                return CheckCodeResult.OK;

            AccessFail(user);
            return CheckCodeResult.CodeError;
        }

        public void ResetAccessFail(User user)
        {
            user.AccessFail.Reset();
        }

        public void AccessFail(User user)
        {
            user.AccessFail.Fail();
        }

        public bool IsLockOut(User user)
        {
            return user.AccessFail.IsLockOut();
        }
    }
}

