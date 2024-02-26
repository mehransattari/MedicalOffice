using MedicalOffice.Server.Context;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalOffice.Shared.Helper.Mapper;

namespace MedicalOffice.Server.Controllers;

[Route("api/Reserves")]
[ApiController]
public class ReservesController : Controller
{
    #region Constructor
    private readonly AppDbContext _appDbContext;
    private readonly ProtectPassword _protect;

    public ReservesController(AppDbContext appDb, ProtectPassword protect)
    {
        _appDbContext = appDb;
        _protect = protect;
    }
    #endregion

    #region Methods


    [HttpGet]
    public async Task<List<ReserveDto>> Get()
    {
        var result = await _appDbContext.Reservations
                           .Include(x => x.User)
                           .Include(x => x.TimesReserve)
                           .ThenInclude(x => x.DaysReserve)
                           .Select(x => new ReserveDto()
                           {
                               FirstName = x.User.FirstName,
                               LastName = x.User.LastName,
                               Mobile = x.User.Mobile,
                               NationalCode = x.User.NationalCode,
                               Day = x.TimesReserve.DaysReserve.Day,
                               FromTime = x.TimesReserve.FromTime,
                               ToTime = x.TimesReserve.ToTime,
                               CreateDate = x.CreateDate,
                               UpdateDate = x.UpdateDate

                           }).ToListAsync();


        if (result != null && result.Count > 0)
        {
            return result;
        }
        else
        {
            return new List<ReserveDto>();
        }
    }

    [HttpGet("getReserveById/{id}")]
    public async Task<Reservation> Get(long id)
    {
        var result = await _appDbContext.Reservations.FirstOrDefaultAsync(x => x.Id == id);
        if (result != null)
        {
            return result;
        }
        else
        {
            return new Reservation();
        }
    }

    [HttpPost("createReserve")]
    public async Task<bool> Create([FromBody] ReserveDto Reserve)
    {
        var _Reserve = Reserve.Mapper();

        _Reserve.CreateDate = DateTime.Now;

        _Reserve.UpdateDate = DateTime.Now;

        await _appDbContext.Reservations.AddAsync(_Reserve);

        var result = await _appDbContext.SaveChangesAsync();

        return result != 0;
    }

    [HttpPut("updateReserve")]
    public async Task<bool> Update([FromBody] ReserveDto Reserve)
    {
        var _Reserve = Reserve.Mapper();

        _Reserve.CreateDate = Reserve.CreateDate;

        _Reserve.UpdateDate = DateTime.Now;

        _appDbContext.Reservations.Update(_Reserve);

        var result = await _appDbContext.SaveChangesAsync();

        return result != 0;
    }

    [HttpDelete("deleteReserve/{Id}")]
    public async Task<bool> Delete(long Id)
    {
        var _Reserve = await Get(Id);
        _appDbContext.Reservations.Remove(_Reserve);
        var result = await _appDbContext.SaveChangesAsync();

        return result != 0;
    }

    [HttpPost("deleteReserves")]
    public async Task<bool> DeleteReserves([FromBody] IEnumerable<long> ids)
    {
        var Reserves = await _appDbContext.Reservations.Where(u => ids.Contains(u.Id)).ToListAsync();

        if (Reserves.Any())
        {
            _appDbContext.Reservations.RemoveRange(Reserves);

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

    [HttpGet("countReserves")]
    public async Task<int> GetReservesCount()
    {
        var Reserves = _appDbContext.Reservations.Count();
        return await Task.FromResult(Reserves);
    }

    [HttpGet("countReserves/{search}")]
    public async Task<int> GetReservesCount(string search)
    {
        var reserves = _appDbContext.Reservations
                               .Include(x => x.User)
                               .Include(x => x.TimesReserve)
                               .ThenInclude(x => x.DaysReserve)
                               .Where(x => search == "-" ||
                                           EF.Functions.Like(x.User.FirstName, $"%{search}%") ||
                                           EF.Functions.Like(x.User.LastName, $"%{search}%") ||
                                           EF.Functions.Like(x.User.Mobile, $"%{search}%") ||
                                           EF.Functions.Like(x.User.NationalCode, $"%{search}%"))
                               .Count();

        return await Task.FromResult(reserves);
    }

    [HttpGet("Reserves/{search}")]
    public async Task<IEnumerable<ReserveDto>> GetReserves(string search)
    {

        if (!string.IsNullOrEmpty(search))
        {
            var Reserves = await _appDbContext.Reservations.Include(x => x.User).Where(x => x.User.FirstName.Contains(search) ||
                                                                       x.User.LastName.Contains(search) ||
                                                                       x.User.Mobile.Contains(search) ||
                                                                       x.User.NationalCode.Contains(search))
                                                           .OrderByDescending(p => p.Id)
                                                           .ToListAsync();

            var _Reserves = Reserves.Select((Reserve, index) => new ReserveDto
            {
                Id = Reserve.Id,
                FirstName = Reserve.User.FirstName,
                LastName = Reserve.User.LastName,
                Mobile = Reserve.User.Mobile,
                NationalCode = Reserve.User.NationalCode,
                Number = index + 1,

            }).ToList();

            return _Reserves;
        }
        return new List<ReserveDto>();
    }

    [HttpGet("pages/{skip}/{pagesize}/{search}")]
    public async Task<IEnumerable<ReserveDto>> GetReservesPaging(int skip, int pagesize, string search)
    {

        var reserves = await _appDbContext.Reservations
                             .Include(x => x.User)
                             .Include(x => x.TimesReserve)
                             .ThenInclude(x => x.DaysReserve)
                              .Where(x => search == "-" ||
                                         EF.Functions.Like(x.User.FirstName, $"%{search}%") ||
                                         EF.Functions.Like(x.User.LastName, $"%{search}%") ||
                                         EF.Functions.Like(x.User.Mobile, $"%{search}%") ||
                                         EF.Functions.Like(x.User.NationalCode, $"%{search}%"))
                             .Select(x => new ReserveDto()
                             {
                                 Id = x.Id,
                                 FirstName = x.User.FirstName,
                                 LastName = x.User.LastName,
                                 Mobile = x.User.Mobile,
                                 NationalCode = x.User.NationalCode,
                                 Day = x.TimesReserve.DaysReserve.Day,
                                 FromTime = x.TimesReserve.FromTime,
                                 ToTime = x.TimesReserve.ToTime,
                                 ReserveType = x.ReserveType,
                                 Status = x.Status,
                                 CreateDate = x.CreateDate,
                                 UpdateDate = x.UpdateDate
                             })
                             .OrderByDescending(p => p.CreateDate)
                             .ThenBy(x => x.Id)
                             .Skip(skip)
                             .Take(pagesize)
                             .ToListAsync();

        var _Reserves = reserves.Select((reserve, index) => new ReserveDto
        {
            Id = reserve.Id,
            FirstName = reserve.FirstName,
            LastName = reserve.LastName,
            Mobile = reserve.Mobile,
            NationalCode = reserve.NationalCode,
            Day = reserve.Day,
            FromTime = reserve.FromTime,
            ToTime = reserve.ToTime,
            ReserveType = reserve.ReserveType,
            Status = reserve.Status,
            CreateDate = reserve.CreateDate,
            UpdateDate = reserve.UpdateDate,
            Number = index + 1 + skip

        }).ToList();

        return _Reserves;
    }

    [HttpPost("ReserveUser")]
    public async Task<bool> ReserveUser([FromBody] ReserveDto reserve)
    {
        try
        {
            User _user = new User();

            if (await checkDuplicateUserByNationalCode(reserve.NationalCode))
            {
                _user = await _appDbContext.Users
                                    .FirstOrDefaultAsync(x => x.NationalCode == reserve.NationalCode &&
                                                              x.Mobile == reserve.Mobile);
            }
            else
            {
                _user = new User()
                {
                    FirstName = reserve.FirstName,
                    LastName = reserve.LastName,
                    Mobile = reserve.Mobile,
                    NationalCode = reserve.NationalCode,
                    Password = !string.IsNullOrEmpty(reserve.Password) ? _protect.HashPassword(reserve.Password) : "0",
                    RoleId = reserve.RoleId,
                    SingleUseCode = Convert.ToInt32(reserve.SingleUseCode)
                };

                await _appDbContext.Users.AddAsync(_user);
            }


            if (_user != null)
            {
               var checkDuplicateReservation = await CheckDuplicateReservation(reserve.NationalCode,reserve.TimesReserveId);
                if(checkDuplicateReservation)
                {
                    return false ;
                }
                var _reserve = new Reservation()
                {
                    TimesReserveId = reserve.TimesReserveId,
                    User = _user,
                    UserId = _user.Id,
                    Status = reserve.Status,
                    ReserveType = reserve.ReserveType,
                    Code = reserve.Code,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };

                await _appDbContext.Reservations.AddAsync(_reserve);

                await _appDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
        catch (Exception)
        {

            throw;
        }

    }

    [HttpPut("changeStatusReserveToReserved")]
    public async Task<bool> ChangeStatusReserveToReserved([FromBody] IEnumerable<long> ids)
    {
        var reserveds = await _appDbContext.Reservations.Where(u => ids.Contains(u.Id)).ToListAsync();

        if (reserveds.Any())
        {
            try
            {

                reserveds.ForEach(x => x.Status = StatusEnum.Reserved);
                _appDbContext.UpdateRange(reserveds);
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

    [HttpPut("changeStatusReserveToCancelled")]
    public async Task<bool> ChangeStatusReserveToCancelled([FromBody] IEnumerable<long> ids)
    {
        var cancellaesd = await _appDbContext.Reservations.Where(u => ids.Contains(u.Id)).ToListAsync();


        if (cancellaesd.Any())
        {
            try
            {
                cancellaesd.ForEach(x => x.Status = StatusEnum.Cancelled);
                _appDbContext.UpdateRange(cancellaesd);
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

    public async Task<bool> checkDuplicateUserByNationalCode(string nationalCode)
    {
        var res = await _appDbContext.Users.AnyAsync(x => x.NationalCode == nationalCode);

        return res;
    }

    [HttpPut("checkDuplicateReservation/{nationalCode}/{timesReserveId}")]
    public async Task<bool> CheckDuplicateReservation(string nationalCode, long timesReserveId)
    {
        var timesReserves = await _appDbContext.TimesReserves
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == timesReserveId);

        var res = await _appDbContext.Reservations.AnyAsync(x => x.User.NationalCode == nationalCode &&
                                                                 x.TimesReserve.DaysReserveId == timesReserves.DaysReserveId);

        return res;
    }

    [HttpGet("showDateAndTimeByTimeReserveId/{timesReserveId}")]
    public async Task<TimesReserve> ShowDateAndTimeByTimeReserveId(long timesReserveId)
    {
        try
        {
            var timeReserve = await _appDbContext.TimesReserves
                                     .Include(x => x.DaysReserve)
                                     .FirstOrDefaultAsync(x => x.Id == timesReserveId);
            if (timeReserve != null)
            {
                return timeReserve;
            }
            return new TimesReserve();
        }
        catch (Exception)
        {
            return new TimesReserve();

            throw;
        }

    }

    #endregion
}
