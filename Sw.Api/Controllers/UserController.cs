using Api.Exceptions;
using Application.Dtos;
using AutoMapper;
using Core;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Api.Controllers
{
    public class UserController : BaseController
    {
        /*
        public UserController(
            IMapper mapper,
            ITransaction transaction,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IUserRoleRepository userRoleRepository)
        {
            Mapper = mapper;
            Transaction = transaction;
            RoleRepository = roleRepository;
            UserRepository = userRepository;
            UserRoleRepository = userRoleRepository;
        }
        public IMapper Mapper { get; }
        public ITransaction Transaction { get; }
        public IRoleRepository RoleRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }

        /// <summary>
        /// Users Search
        /// </summary>
        /// <returns></returns>
        // [Security("User")]
        [HttpGet, Route("api/users")]
        public async Task<IActionResult> Users_Search()
        {
            var users = await UserRepository.Entities.AsNoTracking().ToListAsync();
            return Ok(Mapper.Map<List<UserDto>>(users));
        }

        /// <summary>
        /// Users Single
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // [Security("User")]
        [HttpGet, Route("api/users/{id}", Name = "users:single")]
        public async Task<IActionResult> Users_Single(Guid id)
        {
            var user = await UserRepository.FindAsync(id);
            if (user == null)
                throw new NotFoundException("not found.");
            var dto = Mapper.Map<UserUpdateDto>(user);
            dto.RoleIds = await UserRoleRepository.GetRoleIds(user.UserId);
            return Ok(dto);
        }

        /// <summary>
        /// Users Lookup
        /// </summary>
        /// <returns></returns>
        // [Security("User.Create", "User.Update")]
        [HttpGet, Route("api/users/lookup")]
        public async Task<IActionResult> Users_Lookup()
        {
            var roles = Mapper.Map<List<RoleDto>>(await RoleRepository.Entities.AsNoTracking().ToListAsync());
            return Ok(new { roles });
        }

        /// <summary>
        /// Users Create
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // [Security("User.Create")]
        [HttpPost, Route("api/users")]
        public async Task<IActionResult> Users_Create([FromBody] UserCreateDto dto)
        {
            var single = await UserRepository.Entities.AsNoTracking().SingleOrDefaultAsync(x => x.Account == dto.Account);
            if (single != null)
                throw new ValidationException("user's account exists. please try another one.");
            var user = Mapper.Map<User>(dto);
            await UserRepository.InsertAsync(user);
            if (dto.RoleIds.Count > 0)
                await UserRoleRepository.InsertAsync(
                    dto.RoleIds.Select(x => new UserRole
                    {
                        RoleId = x,
                        UserId = user.UserId
                    }).ToList());
            await Transaction.SaveChangesAsync();
            return CreatedAtRoute("users:single", new { id = user.UserId }, "");
        }

        /// <summary>
        /// Users Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        // [Security("User.Update")]
        [HttpPut, Route("api/users/{id}")]
        public async Task<IActionResult> Users_Update(Guid id, [FromBody] UserUpdateDto dto)
        {
            if (dto.Id == id)
            {
                var user = await UserRepository.FindAsync(id);
                if (user == null)
                    throw new NotFoundException("not found.");
                var single = await UserRepository.Entities.AsNoTracking().SingleOrDefaultAsync(x => x.Account == dto.Account && x.UserId != dto.Id);
                if (single != null)
                    throw new ValidationException("user's account exists. please try another one.");
                if (dto.Version != user.Version.ToHexString())
                    throw new DbUpdateConcurrencyException("data changed.");
                await UserRepository.UpdateAsync(Mapper.Map(dto, user));
                var userRoles = dto.RoleIds.Select(x => new UserRole
                {
                    RoleId = x,
                    UserId = user.UserId
                }).ToList();
                var userRoles2 = await UserRoleRepository.Entities.Where(x => x.UserId == user.UserId).ToListAsync();
                var insertUserRoles = userRoles.Except(userRoles2).ToList();
                if (insertUserRoles.Count > 0)
                    await UserRoleRepository.InsertAsync(insertUserRoles);
                var deleteUserRoles = userRoles2.Except(userRoles).ToList();
                if (deleteUserRoles.Count > 0)
                    await UserRoleRepository.DeleteAsync(deleteUserRoles);
                await Transaction.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }

        /// <summary>
        /// Users Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // [Security("User.Delete")]
        [HttpDelete, Route("api/users/{id}")]
        public async Task<IActionResult> Users_Delete(Guid id)
        {
            var user = await UserRepository.FindAsync(id);
            if (user == null)
                throw new NotFoundException("entity not found.");
            await UserRepository.DeleteAsync(user);
            await Transaction.SaveChangesAsync();
            return NoContent();
        }
        */
    }
}
