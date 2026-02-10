using RealPlaza.Domain.Entities;
using RealPlaza.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Application.Users.Queries
{
    public class GetUsersQuery
    {
        private readonly IUserRepository _repo;

        public GetUsersQuery(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<User>> Execute()
        {
            return await _repo.GetAllAsync();
        }
    }
}
