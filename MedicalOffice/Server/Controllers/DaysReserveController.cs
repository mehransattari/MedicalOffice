using MedicalOffice.Server.Context;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalOffice.Shared.Helper.Mapper;
using MedicalOffice.Server.Helpers;

namespace MedicalOffice.Server.Controllers;

[Route("api/[controller]")]
[Route("api/timedays")]
public class DaysReserveController : ControllerBase
{
    #region Constructor
    private readonly AppDbContext _appDbContext;
    private readonly ProtectPassword _protect;
    private readonly FileHelper _fileHelper;

    public DaysReserveController(AppDbContext appDb, ProtectPassword protect, FileHelper fileHelper)
    {
        _appDbContext = appDb;
        _protect = protect;
        _fileHelper = fileHelper;
    }
    #endregion

    #region Methods

    [HttpGet]
    public async Task<List<DaysReserve>> Get()
    {
        try
        {
            var result = await _appDbContext.DaysReserves.OrderByDescending(x=>x.Day).ToListAsync();

            if (result != null && result.Count > 0)
            {
                return result;
            }
            else
            {
                return new List<DaysReserve>();
            }
        }
        catch (Exception)
        {
            return new List<DaysReserve>();
        }
    }

    [HttpGet("{id}")]
    public async Task<DaysReserve> Get(long id)
    {
        try
        {
            var result = await _appDbContext.DaysReserves.FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                return result;
            }
            else
            {
                return new DaysReserve();
            }
        }
        catch (Exception)
        {
            return new DaysReserve();
        }
    }

    [HttpGet("GetTimesDayReserve")]
    public async Task<List<DaysReserve>> GetTimesDayReserve()
    {
        try
        {
            var result = await _appDbContext.DaysReserves.OrderByDescending(x=>x.Id).Take(7).Include(x=>x.TimesReserves).ToListAsync();

            if (result != null)
            {
                return result;
            }
            else
            {
                return new List<DaysReserve>();
            }
        }
        catch (Exception)
        {
            return new List<DaysReserve>();
        }
    }
    [HttpPost]
    public async Task<bool> Create([FromForm] DaysReserveDto model)
    {
        try
        {
            var _model = model.Mapper();
           
            await _appDbContext.DaysReserves.AddAsync(_model);

            var result = await _appDbContext.SaveChangesAsync();

            return result != 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    [HttpPut]
    public async Task<bool> Update([FromForm] DaysReserveDto model)
    {
        try
        {
            var _model = model.Mapper();

            _appDbContext.DaysReserves.Update(_model);

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

            _appDbContext.DaysReserves.Remove(_model);

            await _appDbContext.SaveChangesAsync();

            var result = await _appDbContext.SaveChangesAsync();

            return result != 0;
        }
        catch (Exception)
        {
            return false;
        }

    }

    [HttpPost("deleteDaysReserve")]
    public async Task<bool> Delete([FromBody] IEnumerable<long> ids)
    {
        var models = await _appDbContext.DaysReserves.Where(u => ids.Contains(u.Id)).ToListAsync();

        if (models.Any())
        {
            _appDbContext.DaysReserves.RemoveRange(models);

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
