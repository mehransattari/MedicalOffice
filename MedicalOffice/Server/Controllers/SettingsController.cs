using MedicalOffice.Server.Context;
using MedicalOffice.Server.Helpers;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalOffice.Shared.Helper.Mapper;
namespace MedicalOffice.Server.Controllers;


[ApiController]
[Route("api/settings")]
public class SettingsController : ControllerBase
{
    #region Constructor
    private readonly AppDbContext _appDbContext;
    private readonly ProtectPassword _protect;
    private readonly FileHelper _fileHelper;
    public SettingsController(AppDbContext appDb, ProtectPassword protect, FileHelper fileHelper)
    {
        _appDbContext = appDb;
        _protect = protect;
        _fileHelper = fileHelper;
    }
    #endregion

    #region Methods

    [HttpGet]
    public async Task<List<Settings>> Get()
    {
        try
        {
            var result = await _appDbContext.Settings.ToListAsync();

            if (result != null && result.Count > 0)
            {
                return result;
            }

            else
            {
                return new List<Settings>();
            }
        }
        catch (Exception)
        {
            return new List<Settings>();
        }
    }

    [HttpGet("getlogo")]
    public async Task<SettingsDto> GetLogo()
    {
        try
        {
            var result = await _appDbContext.Settings.Select(x=> new SettingsDto()
                                                                {
                                                                   Logo = x.Logo
                                                                })
                                                      .FirstOrDefaultAsync();

            if (result != null)
            {
                return result;
            }

            else
            {
                return new SettingsDto();
            }
        }
        catch (Exception)
        {
            return new SettingsDto();
        }
    }

    [HttpGet("{id}")]
    public async Task<Settings> Get(long id)
    {
        try
        {
            var result = await _appDbContext.Settings.FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                return result;
            }

            else
            {
                return new Settings();
            }
        }
        catch (Exception)
        {
            return new Settings();
        }
    }

    [HttpPost]
    public async Task<bool> Create([FromForm] SettingsDto model)
    {
        try
        {
            var _model = model.Mapper();

            if (model.Logo != null)
            {
                _model.Logo = await _fileHelper.SaveFile(model.LogoFile, "Images");
            }

            await _appDbContext.Settings.AddAsync(_model);

            var result = await _appDbContext.SaveChangesAsync();

            return result != 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    [HttpPut]
    public async Task<bool> Update([FromForm] SettingsDto model)
    {

        try
        {
            var _model = model.Mapper();

            if (model.LogoFile != null)
            {
                _model.Logo = await _fileHelper.SaveFile(model.LogoFile, "Images");
            }
            _appDbContext.Settings.Update(_model);

            var result = await _appDbContext.SaveChangesAsync();

            return result != 0;
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
            _appDbContext.Settings.Remove(_model);
            await _appDbContext.SaveChangesAsync();
            var result = await _appDbContext.SaveChangesAsync();

            return result != 0;
        }
        catch (Exception)
        {
            return false;
        }

    }

    [HttpPost("deleteSettings")]
    public async Task<bool> Delete([FromBody] IEnumerable<long> ids)
    {
        var models = await _appDbContext.Settings.Where(u => ids.Contains(u.Id)).ToListAsync();

        if (models.Any())
        {
            _appDbContext.Settings.RemoveRange(models);

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
