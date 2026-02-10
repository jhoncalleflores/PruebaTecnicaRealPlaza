using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealPlaza.API.DTOs;
using RealPlaza.Application.Users.Commands;
using RealPlaza.Application.Users.Login;
using RealPlaza.Application.Users.Queries;
using RealPlaza.Domain.Entities;
using RealPlaza.Domain.Interfaces;

namespace RealPlaza.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetUsersQuery(_repo);
            var users = await query.Execute();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetUserByIdQuery(_repo);
            var user = await query.Execute(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var user = new User
            {
                Username = request.Username,
                PasswordHash = request.PasswordHash,
                Email = request.Email,
                BirthDate = request.BirthDate,
                IsActive = true
            };

            var cmd = new CreateUserCommand(_repo);
            var id = await cmd.Execute(user);

            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserRequest request)
        {
            var user = new User
            {
                Id = id,
                Username = request.Username,
                Email = request.Email,
                BirthDate = request.BirthDate,
                IsActive = request.IsActive
            };

            var cmd = new UpdateUserCommand(_repo);
            await cmd.Execute(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cmd = new DeleteUserCommand(_repo);
            await cmd.Execute(id);

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var cmd = new LoginCommand(_repo);
            var user = await cmd.Execute(request.Username, request.PasswordHash);

            if (user == null)
                return Unauthorized();

            return Ok(user);
        }


    }
}
