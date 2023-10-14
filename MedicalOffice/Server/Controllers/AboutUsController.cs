using MedicalOffice.Server.Context;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalOffice.Shared.Helper.Mapper;

namespace MedicalOffice.Server.Controllers;

[ApiController]
[Route("api/aboutus")]
public class AboutUsController : Controller
{
    #region Constructor
    private readonly AppDbContext _appDbContext;
    private readonly ProtectPassword _protect;

    public AboutUsController(AppDbContext appDb, ProtectPassword protect)
    {
        _appDbContext = appDb;
        _protect = protect;
    }
    #endregion

    #region Methods


    [HttpGet]
    public async Task<List<AboutUs>> Get()
    {
        var result = await _appDbContext.AboutUs.ToListAsync();
        if (result != null && result.Count > 0)
        {
            return result;
        }
        else
        {
            return new List<AboutUs>();
        }
    }

    [HttpGet("getAboutUsById/{id}")]
    public async Task<AboutUs> Get(long id)
    {
        var result = await _appDbContext.AboutUs.FirstOrDefaultAsync(x => x.Id == id);
        if (result != null)
        {
            return result;
        }
        else
        {
            return new AboutUs();
        }
    }

    [HttpPost("createAboutUs")]
    public async Task<bool> Create([FromBody] AboutUsDto model)
    {
        var _model = model.Mapper();
        await _appDbContext.AboutUs.AddAsync(_model);
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

    [HttpPut("updateAboutUs")]
    public async Task<bool> Update([FromBody] AboutUsDto model)
    {
        var _model = model.Mapper();
        _appDbContext.AboutUs.Update(_model);
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

    [HttpDelete("deleteAboutUs/{Id}")]
    public async Task<bool> Delete(long Id)
    {
        var _model = await Get(Id);
        _appDbContext.AboutUs.Remove(_model);
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

    [HttpPost("deleteAboutUs")]
    public async Task<bool> DeleteRoles([FromBody] IEnumerable<long> ids)
    {
        var models = await _appDbContext.AboutUs.Where(u => ids.Contains(u.Id)).ToListAsync();

        if (models.Any())
        {
            _appDbContext.AboutUs.RemoveRange(models);

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
