using RealPlaza.Domain.Entities;
using RealPlaza.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Application.Users.Queries
{
    public class GetUserByIdQuery
    {
        private readonly IUserRepository _repo;

        public GetUserByIdQuery(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User?> Execute(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
    }
}
