using MedicalOffice.Server.Context;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalOffice.Shared.Helper.Mapper;
using MedicalOffice.Server.Helpers;

namespace MedicalOffice.Server.Controllers;

[ApiController]
[Route("api/aboutus")]
public class AboutUsController : ControllerBase
{
    #region Constructor
    private readonly AppDbContext _appDbContext;
    private readonly ProtectPassword _protect;
    private readonly FileHelper _fileHelper;
    public AboutUsController(AppDbContext appDb, ProtectPassword protect, FileHelper fileHelper)
    {
        _appDbContext = appDb;
        _protect = protect;
        _fileHelper = fileHelper;
    }
    #endregion

    #region Methods

    [HttpGet]
    public async Task<List<AboutUs>> Get()
    {
        try
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
        catch (Exception)
        {
            return new List<AboutUs>();
        }
    }

    [HttpGet("{id}")]
    public async Task<AboutUs> Get(long id)
    {
        try
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
        catch (Exception)
        {
            return new AboutUs();
        }
    }  

    [HttpPost]
    public async Task<bool> Create([FromForm] AboutUsDto model)
    {
        try
        {
            var _model = model.Mapper();

            if (model.Image != null)
            {
                _model.Image = await _fileHelper.SaveFile(model.Image, "Images");
            }

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
        catch (Exception)
        {
            return false;
        }
    }

    [HttpPut]
    public async Task<bool> Update([FromForm] AboutUsDto model)
    {     

        try
        {
            var _model = model.Mapper();

            if (model.Image != null)
            {
                _model.Image = await _fileHelper.SaveFile(model.Image, "Images");
            }
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
        catch (Exception)
        {
            return false;
        }
    }

    [HttpDelete("{Id}")]
    public async Task<bool> Delete(long Id)
    {       
        try
        {
            var _model = await Get(Id);
            _appDbContext.AboutUs.Remove(_model);
            await _appDbContext.SaveChangesAsync();
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
        catch (Exception)
        {
            return false;
        }
       
    }

    [HttpPost("deleteAboutUs")]
    public async Task<bool> Delete([FromBody] IEnumerable<long> ids)
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
