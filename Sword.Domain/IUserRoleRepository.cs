using Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<List<Guid>> GetRoleIds(Guid userId);
    }
}
