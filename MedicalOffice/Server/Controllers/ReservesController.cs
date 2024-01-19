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
public class ReservesController : ControllerBase
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
    public async Task<List<Reservation>> Get()
    {
        var result = await _appDbContext.Reservations.ToListAsync();
        if (result != null && result.Count > 0)
        {
            return result;
        }
        else
        {
            return new List<Reservation>();
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
        await _appDbContext.Reservations.AddAsync(_Reserve);
        var result = await _appDbContext.SaveChangesAsync();

        return result != 0;
     
    }

    [HttpPut("updateReserve")]
    public async Task<bool> Update([FromBody] ReserveDto Reserve)
    {
        var _Reserve = Reserve.Mapper();
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

    [HttpGet("Reserves/{search}")]
    public async Task<IEnumerable<ReserveDto>> GetReserves(string search)
    {

        if (!string.IsNullOrEmpty(search))
        {
            var Reserves = await _appDbContext.Reservations.Include(x=>x.User).Where(x => x.User.FirstName.Contains(search) ||
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
                Mobile =Reserve.User.Mobile,
                NationalCode= Reserve.User.NationalCode,
                Number = index + 1,

            }).ToList();

            return _Reserves;
        }
        return new List<ReserveDto>();
    }

    [HttpGet("Reserves/{skip}/{pagesize}")]
    public async Task<IEnumerable<ReserveDto>> GetReservesPaging(int skip, int pagesize)
    {
        var Reserves = await _appDbContext.Reservations
                      .OrderByDescending(p => p.Id)
                      .Skip(skip)
                      .Take(pagesize)
                      .ToListAsync();

        var _Reserves = Reserves.Select((Reserve, index) => new ReserveDto
        {
            Id = Reserve.Id,

        }).ToList();

        return _Reserves;
    }
    #endregion

    [HttpPost("ReserveUser")]
    public async Task<bool> ReserveUser([FromBody] ReserveDto reserve)
    {
        User _user = new User();
        if (await checkDuplicateUserByNationalCode(reserve.NationalCode, reserve.Mobile))
        {
            _user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.NationalCode == reserve.NationalCode && x.Mobile == reserve.Mobile);
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
                RoleId = reserve.RoleId
            };

            await _appDbContext.Users.AddAsync(_user);
        }


        if (_user != null)
        {
            var _reserve = new Reservation()
            {
                TimesReserveId = reserve.TimesReserveId,
                User = _user,
                UserId = _user.Id
            };

            await _appDbContext.Reservations.AddAsync(_reserve);

            await _appDbContext.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<bool> checkDuplicateUserByNationalCode(string nationalCode, string mobile)
    {
        var res = await _appDbContext.Users.AnyAsync(x => x.NationalCode == nationalCode && x.Mobile == mobile);

        return res;
    }
}
