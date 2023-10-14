using Microsoft.AspNetCore.Mvc;
using MedicalOffice.Shared.Helper.Mapper;
using MedicalOffice.Server.Context;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Server.Controllers;

[ApiController]
[Route("api/contactus")]
public class ContactUsController : Controller
{
    #region Constructor
    private readonly AppDbContext _appDbContext;
    private readonly ProtectPassword _protect;

    public ContactUsController(AppDbContext appDb, ProtectPassword protect)
    {
        _appDbContext = appDb;
        _protect = protect;
    }
    #endregion

    #region Methods


    [HttpGet]
    public async Task<List<ContactUs>> Get()
    {
        var result = await _appDbContext.ContactUs.ToListAsync();
        if (result != null && result.Count > 0)
        {
            return result;
        }
        else
        {
            return new List<ContactUs>();
        }
    }

    [HttpGet("getAboutUsById/{id}")]
    public async Task<ContactUs> Get(long id)
    {
        var result = await _appDbContext.ContactUs.FirstOrDefaultAsync(x => x.Id == id);
        if (result != null)
        {
            return result;
        }
        else
        {
            return new ContactUs();
        }
    }

    [HttpPost("createAboutUs")]
    public async Task<bool> Create([FromBody]ContactUsDto model)
    {
        var _model = model.Mapper();
        await _appDbContext.ContactUs.AddAsync(_model);
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
    public async Task<bool> Update([FromBody]ContactUsDto model)
    {
        var _model = model.Mapper();
        _appDbContext.ContactUs.Update(_model);
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
        _appDbContext.ContactUs.Remove(_model);
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
    public async Task<bool> DeleteAboutUs([FromBody] IEnumerable<long> ids)
    {
        var models = await _appDbContext.ContactUs.Where(u => ids.Contains(u.Id)).ToListAsync();

        if (models.Any())
        {
            _appDbContext.ContactUs.RemoveRange(models);

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
