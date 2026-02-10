using RealPlaza.Domain.Entities;
using RealPlaza.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Application.Users.Commands
{
    public class UpdateUserCommand
    {
        private readonly IUserRepository _repo;

        public UpdateUserCommand(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task Execute(User user)
        {
            await _repo.UpdateAsync(user);
        }
    }
}
