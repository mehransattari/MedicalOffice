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
[Route("api/slider")]
public class SliderController :  Controller
{
    #region Constructor
    private readonly AppDbContext _appDbContext;
    private readonly ProtectPassword _protect;
    private readonly FileHelper _fileHelper;
    public SliderController(AppDbContext appDb, ProtectPassword protect, FileHelper fileHelper)
    {
        _appDbContext = appDb;
        _protect = protect;
        _fileHelper = fileHelper;
    }
    #endregion

    #region Methods

    [HttpGet]
    public async Task<List<Slider>> Get()
    {
        try
        {
            var result = await _appDbContext.Sliders.ToListAsync();

            if (result != null && result.Count > 0)
            {
                return result;
            }

            else
            {
                return new List<Slider>();
            }
        }
        catch (Exception)
        {
            return new List<Slider>();
        }
    }

    [HttpGet("{id}")]
    public async Task<Slider> Get(long id)
    {
        try
        {
            var result = await _appDbContext.Sliders.FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                return result;
            }

            else
            {
                return new Slider();
            }
        }
        catch (Exception)
        {
            return new Slider();
        }
    }

    [HttpPost]
    public async Task<bool> Create([FromForm] SliderDto model)
    {
        try
        {
            var _model = model.Mapper();

            if (model.Image != null)
            {
                _model.Image = await _fileHelper.SaveFile(model.Image, "Images");
            }

            await _appDbContext.Sliders.AddAsync(_model);

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
    public async Task<bool> Update([FromForm] SliderDto model)
    {

        try
        {
            var _model = model.Mapper();

            if (model.Image != null)
            {
                _model.Image = await _fileHelper.SaveFile(model.Image, "Images");
            }
            _appDbContext.Sliders.Update(_model);

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
            _appDbContext.Sliders.Remove(_model);
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

    [HttpPost("deleteSlider")]
    public async Task<bool> Delete([FromBody] IEnumerable<long> ids)
    {
        var models = await _appDbContext.Sliders.Where(u => ids.Contains(u.Id)).ToListAsync();

        if (models.Any())
        {
            _appDbContext.Sliders.RemoveRange(models);

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
