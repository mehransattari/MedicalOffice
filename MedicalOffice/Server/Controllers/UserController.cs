
using MedicalOffice.Server.Context;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalOffice.Shared.Helper.Mapper;

namespace MedicalOffice.Server.Controllers;

[ApiController]
[Route("api/users")]
public class UserController
{
    #region Constructor
    private readonly AppDbContext _appDbContext;
    private readonly ProtectPassword _protect;

    public UserController(AppDbContext appDb, ProtectPassword protect)
    {
        _appDbContext = appDb;
        _protect = protect;
    }
    #endregion

    #region Methods
    [HttpGet("roles")]
    public async Task<List<Role>> Roles()
    {
        var result = _appDbContext.Roles.ToList();
        if (result != null && result.Count > 0)
        {
            return await Task.FromResult(result);
        }
        else
        {
            return await Task.FromResult(new List<Role>());
        }
    }

    [HttpPost("createUser")]
    public async Task<bool> CreateUser([FromBody] UserDto user)
    {
        var _user = user.Mapper();
        if (!string.IsNullOrEmpty(user.Password))
        {
            _user.Password = _protect.HashPassword(user.Password);
        }
        await _appDbContext.Users.AddAsync(_user);
        await _appDbContext.SaveChangesAsync();

        return true;
    }

    [HttpPost("registerUser")]
    public async Task<bool> RegisterUser([FromBody] RegisterDTO user)
    {
        var role = _appDbContext.Roles.FirstOrDefault(p => p.EnCaption == "user");
        User newUser = new User
        {
            Mobile = user.Mobile,
            RoleId = role.Id
        };
        if (!string.IsNullOrEmpty(user.Password))
            newUser.Password = _protect.HashPassword(user.Password);

        _appDbContext.Users.Add(newUser);

        await _appDbContext.SaveChangesAsync();

        return true;
    }

    [HttpGet("userList/{search}")]
    public async Task<IEnumerable<UserDto>> GetUsers(string search)
    {

        if (!string.IsNullOrEmpty(search))
        {
            var  users = await _appDbContext.Users.Where(x => x.FirstName.Contains(search) ||
                              x.LastName.Contains(search) ||
                              x.Mobile.Contains(search))
                             .Include(x => x.Role)
                             .OrderByDescending(p => p.Id)                 
                             .ToListAsync();

            var _users = users.Select((user, index) => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mobile = user.Mobile,
                RoleId = user.RoleId,
                RoleName = user.Role.FaCaption,
                Number = index + 1,
            })
             .ToList();
                    
      
        return _users;
       }
        return new List<UserDto>();
    }

    [HttpGet("countUsers")]
    public async Task<int> GetUsersCount()
    {
        var users = _appDbContext.Users.Count();
        return await Task.FromResult(users);
    }

    [HttpGet("userList/{skip}/{pagesize}")]
    public async Task<IEnumerable<UserDto>> GetUsers(int skip, int pagesize)
    {
        var users = await _appDbContext.Users
                      .Include(x => x.Role)
                      .OrderByDescending(p => p.Id)
                      .Skip(skip)
                      .Take(pagesize)                      
                      .ToListAsync();

        var _users = users.Select((user, index) => new UserDto
                         {
                             Id = user.Id,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Mobile = user.Mobile,
                             RoleId = user.RoleId,
                             RoleName = user.Role.FaCaption,
                             Number = index + 1,
                         })
                         .ToList();

        return _users;
    }

    [HttpPost("getUserById")]
    public async Task<User> GetUserById([FromBody] long Id)
    {
        var result = _appDbContext.Users.Where(p => p.Id == Id).FirstOrDefault();

        if (result != null)
        {
            return await Task.FromResult(result);
        }
        else
        {
            return await Task.FromResult(new User());
        }
    }

    [HttpPost("updateUser")]
    public async Task<bool> UpdateUser([FromBody] UserDto user)
    {
        var _user =await _appDbContext.Users.FirstOrDefaultAsync(x=>x.Id==user.Id);
        _user.FirstName = user.FirstName;
        _user.LastName = user.LastName;
        _user.Mobile = user.Mobile;
        _user.RoleId = user.RoleId;

        try
        {
            await _appDbContext.SaveChangesAsync();
            return await Task.FromResult(true);
        }
        catch (Exception)
        {
            return await Task.FromResult(false);
        }
    }

    [HttpPost("deleteUser")]
    public async Task<bool> DeleteUser([FromBody] UserDto user)
    {
        var _user = user.Mapper();
        _appDbContext.Users.Remove(_user);
        try
        {
            await _appDbContext.SaveChangesAsync();
            return await Task.FromResult(true);
        }
        catch (Exception)
        {
            return await Task.FromResult(false);
        }
    }

    [HttpPost("deleteUsers")]
    public async Task<bool> DeleteUsers([FromBody] IEnumerable<long> ids)
    {
        var users = await _appDbContext.Users.Where(u => ids.Contains(u.Id)).ToListAsync();

        if (users.Any())
        {
            _appDbContext.Users.RemoveRange(users);

            try
            {
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        return true;
    }


    #endregion
}
