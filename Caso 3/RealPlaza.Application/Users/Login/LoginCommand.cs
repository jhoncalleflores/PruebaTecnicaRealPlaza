using RealPlaza.Domain.Entities;
using RealPlaza.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Application.Users.Login
{
    public class LoginCommand
    {
        private readonly IUserRepository _repo;

        public LoginCommand(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User?> Execute(string username, string passwordHash)
        {
            var user = await _repo.GetByUsernameAsync(username);

            if (user == null)
                return null;

            if (user.PasswordHash != passwordHash)
                return null;

            if (!user.IsActive)
                return null;

            return user;
        }
    }
}
