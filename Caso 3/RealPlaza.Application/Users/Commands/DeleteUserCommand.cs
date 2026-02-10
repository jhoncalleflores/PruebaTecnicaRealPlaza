using RealPlaza.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Application.Users.Commands
{
    public class DeleteUserCommand
    {
        private readonly IUserRepository _repo;

        public DeleteUserCommand(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task Execute(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
