using Core;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ITransaction transaction) : base(transaction) { }

        public async Task<List<Guid>> GetRoleIds(Guid userId)
        {
            return await Entities.Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync();
        }
    }
}
