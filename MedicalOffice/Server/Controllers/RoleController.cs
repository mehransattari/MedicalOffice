using MedicalOffice.Server.Context;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalOffice.Shared.Helper.Mapper;
namespace MedicalOffice.Server.Controllers;

[ApiController]
[Route("api/roles")]
public class RoleController : ControllerBase
{
    #region Constructor
    private readonly AppDbContext _appDbContext;
    private readonly ProtectPassword _protect;

    public RoleController(AppDbContext appDb, ProtectPassword protect)
    {
        _appDbContext = appDb;
        _protect = protect;
    }
    #endregion

    #region Methods

   
    [HttpGet]
    public async Task<List<Role>> Get()
    {
        var result = await _appDbContext.Roles.ToListAsync();
        if (result != null && result.Count > 0)
        {
            return result;
        }
        else
        {
            return new List<Role>();
        }
    }

    [HttpGet("getRoleById/{id}")]
    public async Task<Role> Get(long id)
    {
        var result = await _appDbContext.Roles.FirstOrDefaultAsync(x=>x.Id==id);
        if (result != null)
        {
            return result;
        }
        else
        {
            return new Role();
        }
    }

    [HttpPost("createRole")]
    public async Task<bool> Create([FromBody] RoleDto role)
    {
        var _role = role.Mapper();
        await _appDbContext.Roles.AddAsync(_role);
        var result = await  _appDbContext.SaveChangesAsync();    
        if (result != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [HttpPut("updateRole")]
    public async Task<bool> Update([FromBody] RoleDto role)
    {
        var _role = role.Mapper();
         _appDbContext.Roles.Update(_role);
        var result = await _appDbContext.SaveChangesAsync();
        if (result != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [HttpDelete("deleteRole/{Id}")]
    public async Task<bool> Delete(long Id)
    {
         var _role =await Get(Id);
         _appDbContext.Roles.Remove(_role);
         var result = await _appDbContext.SaveChangesAsync();
        if (result != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [HttpPost("deleteRoles")]
    public async Task<bool> DeleteRoles([FromBody] IEnumerable<long> ids)
    {
        var roles = await _appDbContext.Roles.Where(u => ids.Contains(u.Id)).ToListAsync();

        if (roles.Any())
        {
            _appDbContext.Roles.RemoveRange(roles);

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

    [HttpGet("countRoles")]
    public async Task<int> GetRolesCount()
    {
        var roles = _appDbContext.Roles.Count();
        return await Task.FromResult(roles);
    }

    [HttpGet("roles/{search}")]
    public async Task<IEnumerable<RoleDto>> GetRoles(string search)
    {

        if (!string.IsNullOrEmpty(search))
        {
            var roles = await _appDbContext.Roles.Where(x => x.FaCaption.Contains(search) ||
                              x.EnCaption.Contains(search) )  
                             .OrderByDescending(p => p.Id)
                             .ToListAsync();

            var _roles = roles.Select((role, index) => new RoleDto
            {
                Id = role.Id,
                FaCaption = role.FaCaption,
                EnCaption = role.EnCaption,              
                Number = index + 1,
            }).ToList();

            return _roles;
        }
        return new List<RoleDto>();
    }

    [HttpGet("roles/{skip}/{pagesize}")]
    public async Task<IEnumerable<RoleDto>> GetRolesPaging(int skip, int pagesize)
    {
        var roles = await _appDbContext.Roles
                      .OrderByDescending(p => p.Id)
                      .Skip(skip)
                      .Take(pagesize)
                      .ToListAsync();

        var _roles = roles.Select((role, index) => new RoleDto
        {
            Id = role.Id,
            FaCaption = role.FaCaption,
            EnCaption = role.EnCaption,           
            Number = index + 1,
        }).ToList();

        return _roles;
    }
    #endregion
}
