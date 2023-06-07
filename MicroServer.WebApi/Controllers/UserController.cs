using System;
using MicroServer.Domain;
using MicroServer.Domain.Entites;
using MicroServer.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace MicroServer.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [UnitOfWork(typeof(UserDbContext))]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;
        private readonly UserDomainService _userDomainService;
        private readonly IUserDomainRepository _userDomainRepository;

        public UserController(UserDbContext userDbContext, UserDomainService userDomainService, IUserDomainRepository repository)
        {
            _userDbContext = userDbContext;
            _userDomainService = userDomainService;
            _userDomainRepository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(PhoneNumber req)
        {
            if (await _userDomainRepository.FindOneAsync(req) != null)
                return BadRequest("手机号已经存在");

            User user = new User(req);
            _userDbContext.Add(user);
            return Ok("成功");
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest req)
        {
            var user = await _userDomainRepository.FindOneAsync(req.Id);
            if (user == null)
                return NotFound();

            user.ChangePassword(req.Password);
            return Ok("成功");
        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> Unlock(Guid id)
        {
            var user = await _userDomainRepository.FindOneAsync(id);
            if (user == null)
                return NotFound();

            _userDomainService.ResetAccessFail(user);
            return Ok("成功");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = _userDbContext.Users.ToList();
            return Ok(User);
        }
    }
}

