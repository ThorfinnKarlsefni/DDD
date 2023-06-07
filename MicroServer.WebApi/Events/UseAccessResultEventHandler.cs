using System;
using MediatR;
using MicroServer.Domain;
using MicroServer.Domain.Events;

namespace MicroServer.WebApi.Events
{
    public class UseAccessResultEventHandler : INotificationHandler<UserAccessResultEvent>
    {
        private readonly IUserDomainRepository _repositiory;

        public UseAccessResultEventHandler(IUserDomainRepository userDomainRepository)
        {
            _repositiory = userDomainRepository;
        }

        public Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
        {
            var result = notification.Reuslt;
            var phoneNum = notification.PhoneNumber;

            string msg;
            switch (result)
            {
                case Domain.UserAccessReuslt.OK:
                    msg = $"{phoneNum}登录成功";
                    break;
                case Domain.UserAccessReuslt.PhoneNumberNotFound:
                    msg = $"{phoneNum}登录失败";
                    break;
                case Domain.UserAccessReuslt.PasswordError:
                    msg = $"{phoneNum}密码错误";
                    break;
                case Domain.UserAccessReuslt.NoPassword:
                    msg = $"{phoneNum}没有设置密码";
                    break;
                case Domain.UserAccessReuslt.Lockout:
                    msg = $"{phoneNum}帐号已锁定";
                    break;
                default:
                    throw new NotImplementedException();
            }
            return _repositiory.AddNewLoginHistoryAnsync(phoneNum, msg);
        }
    }
}

