using System;
using MicroServer.Domain;
using Microsoft.Extensions.Logging;

namespace MicroServer.Infrastructure
{
    public class MockSmsCodeSender : ISmsCodeSender
    {
        private readonly ILogger<MockSmsCodeSender> _looger;

        public MockSmsCodeSender(ILogger<MockSmsCodeSender> logger)
        {
            _looger = logger;
        }
        public Task SendCodeAsync(PhoneNumber phoneNumber, string code)
        {
            _looger.LogInformation($"向{phoneNumber},发验证码{code}");
            return Task.CompletedTask;
        }
    }
}

