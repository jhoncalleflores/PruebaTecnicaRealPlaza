using RealPlaza.Domain.Entities;
using RealPlaza.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Application.Users.Commands
{
    public class CreateUserCommand
    {
        private readonly IUserRepository _repo;

        public CreateUserCommand(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Execute(User user)
        {
            return await _repo.CreateAsync(user);
        }
    }
}
