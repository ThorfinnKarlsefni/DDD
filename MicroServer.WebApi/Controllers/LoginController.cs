using System;
using MicroServer.Domain;
using MicroServer.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace MicroServer.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [UnitOfWork(typeof(UserDbContext))]
    public class LoginController : ControllerBase
    {

        private readonly UserDomainService _domainService;
        public LoginController(UserDomainService domainService)
        {
            _domainService = domainService;
        }

        [HttpPut]
        public async Task<IActionResult> LoginByPhoneAndPwd(LoginByPhoneAndPwdRequrest req)
        {
            if (req.Password.Length < 3)
                return BadRequest("密码长度不能小于3");

            var phoneNum = req.PhoneNumber;
            var result = await _domainService.CheckLoginAsync(phoneNum, req.Password);

            switch (result)
            {
                case UserAccessReuslt.OK:
                    return Ok("登录成功");
                case UserAccessReuslt.PhoneNumberNotFound:
                    return BadRequest("手机号或密码错误");
                case UserAccessReuslt.Lockout:
                    return BadRequest("用户被锁定");
                case UserAccessReuslt.NoPassword:
                case UserAccessReuslt.PasswordError:
                    return BadRequest("手机号密码错误");
                default:
                    throw new NotImplementedException();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendCodeByPhone(SendLoginByPhoneAndCodeRequest req)
        {
            var res = await _domainService.SendCodeAsync(req.PhoneNumber);
            switch (res)
            {
                case UserAccessReuslt.OK:
                    return Ok("验证码已发送出去");
                case UserAccessReuslt.Lockout:
                    return BadRequest("用户被锁定 请稍后再试");
                default:
                    return BadRequest("请求错误");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckCode(CheckLoginByPhoneAndCodeRequest req)
        {
            var res = await _domainService.CheckCodeAsync(req.PhoneNumber, req.Code);
            switch (res)
            {
                case CheckCodeResult.OK:
                    return Ok("登录成功");
                case CheckCodeResult.PhoneNumberNotFound:
                    return BadRequest("手机号或密码错误");
                case CheckCodeResult.Lockout:
                    return BadRequest("用户被锁定");
                case CheckCodeResult.CodeError:
                    return BadRequest("手机号密码错误");
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

