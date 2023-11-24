using MedicalOffice.Server.Context;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Server.Controllers;

[ApiController]
[Route("api/timesreserve")]
public class TimesReserveController : ControllerBase
{
    #region Constructor
    private readonly AppDbContext _appDbContext;


    public TimesReserveController(AppDbContext appDb)
    {
        _appDbContext = appDb;
    }
    #endregion

    #region Methods

    [HttpGet]
    public async Task<List<TimesReserve>> Get()
    {
        try
        {
            var result = await _appDbContext.TimesReserves.ToListAsync();

            if (result != null && result.Count > 0)
            {
                return result;
            }
            else
            {
                return new List<TimesReserve>();
            }
        }
        catch (Exception)
        {
            return new List<TimesReserve>();
        }
    }

    [HttpGet("{id}")]
    public async Task<TimesReserve> Get(long id)
    {
        try
        {
            var result = await _appDbContext.TimesReserves.FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                return result;
            }
            else
            {
                return new TimesReserve();
            }
        }
        catch (Exception)
        {
            return new TimesReserve();
        }
    }

    [HttpGet("getbyday/{id}")]
    public async Task<List<TimesReserve>> GetByDay(long id)
    {
        try
        {
            var result = await _appDbContext.TimesReserves
                                            .Where(x => x.DaysReserveId == id)
                                            .ToListAsync();

            if (result != null)
            {
                return result;
            }
            else
            {
                return new List<TimesReserve>();
            }
        }
        catch (Exception)
        {
            return new List<TimesReserve>();
        }
    }

    [HttpPost]
    public async Task<bool> Create([FromForm] TimesReserveDto model)
    {
        try
        {
            var _model = model.Mapper();

            await _appDbContext.TimesReserves.AddAsync(_model);

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
    public async Task<bool> Update([FromForm] TimesReserveDto model)
    {
        try
        {
            var _model = model.Mapper();

            _appDbContext.TimesReserves.Update(_model);

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

            _appDbContext.TimesReserves.Remove(_model);

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

    [HttpPost("deleteTimesReserve")]
    public async Task<bool> Delete([FromBody] IEnumerable<long> ids)
    {
        var models = await _appDbContext.TimesReserves.Where(u => ids.Contains(u.Id)).ToListAsync();

        if (models.Any())
        {
            _appDbContext.TimesReserves.RemoveRange(models);

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

    [HttpPost("checkDuplicateTimesReserve")]
    public async Task<bool> CheckDuplicateTimesReserve(TimesReserveDto model)
    {
        if (model.DaysReserveId == 0)
        {
            return false;   
        }

        var result=await _appDbContext.TimesReserves.AnyAsync(x => x.DaysReserveId == model.DaysReserveId &&
                                                                   x.FromTime==model.FromTime && 
                                                                   x.ToTime==model.ToTime);

        return result;   
        
     }
    #endregion
}
