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

namespace Api
{
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ITransaction _transaction;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserController(
            IMapper mapper,
            ITransaction transaction,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IUserRoleRepository userRoleRepository)
        {
            _mapper = mapper;
            _transaction = transaction;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// Users Search
        /// </summary>
        /// <returns></returns>
        // [Security("User")]
        [HttpGet, Route("api/users")]
        public async Task<IActionResult> Users_Search()
        {
            var users = await _userRepository.Entities.AsNoTracking().ToListAsync();
            return Ok(_mapper.Map<List<UserDto>>(users));
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
            var user = await _userRepository.FindAsync(id);
            if (user == null)
                throw new NotFoundException("not found.");
            var dto = _mapper.Map<UserUpdateDto>(user);
            dto.RoleIds = await _userRoleRepository.GetRoleIds(user.UserId);
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
            var roles = _mapper.Map<List<RoleDto>>(await _roleRepository.Entities.AsNoTracking().ToListAsync());
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
            var single = await _userRepository.Entities.AsNoTracking().SingleOrDefaultAsync(x => x.Account == dto.Account);
            if (single != null)
                throw new ValidationException("user's account exists. please try another one.");
            var user = _mapper.Map<User>(dto);
            await _userRepository.InsertAsync(user);
            if (dto.RoleIds.Count > 0)
                await _userRoleRepository.InsertAsync(
                    dto.RoleIds.Select(x => new UserRole
                    {
                        RoleId = x,
                        UserId = user.UserId
                    }).ToList());
            await _transaction.SaveChangesAsync();
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
                var user = await _userRepository.FindAsync(id);
                if (user == null)
                    throw new NotFoundException("not found.");
                var single = await _userRepository.Entities.AsNoTracking().SingleOrDefaultAsync(x => x.Account == dto.Account && x.UserId != dto.Id);
                if (single != null)
                    throw new ValidationException("user's account exists. please try another one.");
                if (dto.Version != user.Version.ToHexString())
                    throw new DbUpdateConcurrencyException("data changed.");
                await _userRepository.UpdateAsync(_mapper.Map(dto, user));
                var userRoles = dto.RoleIds.Select(x => new UserRole
                {
                    RoleId = x,
                    UserId = user.UserId
                }).ToList();
                var userRoles2 = await _userRoleRepository.Entities.Where(x => x.UserId == user.UserId).ToListAsync();
                var insertUserRoles = userRoles.Except(userRoles2).ToList();
                if (insertUserRoles.Count > 0)
                    await _userRoleRepository.InsertAsync(insertUserRoles);
                var deleteUserRoles = userRoles2.Except(userRoles).ToList();
                if (deleteUserRoles.Count > 0)
                    await _userRoleRepository.DeleteAsync(deleteUserRoles);
                await _transaction.SaveChangesAsync();
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
        [AllowAnonymous]
        [HttpDelete, Route("api/users/{id}")]
        public async Task<IActionResult> Users_Delete(Guid id)
        {
            var user = await _userRepository.FindAsync(id);
            if (user == null)
                throw new NotFoundException("not found.");
            await _userRepository.DeleteAsync(user);
            await _transaction.SaveChangesAsync();
            return NoContent();
        }
    }
}
