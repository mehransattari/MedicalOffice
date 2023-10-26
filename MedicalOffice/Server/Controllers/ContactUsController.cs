using Microsoft.AspNetCore.Mvc;
using MedicalOffice.Shared.Helper.Mapper;
using MedicalOffice.Server.Context;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using MedicalOffice.Server.Helpers;

namespace MedicalOffice.Server.Controllers;

[ApiController]
[Route("api/contactus")]
public class ContactUsController : ControllerBase
{
    #region Constructor
    private readonly AppDbContext _appDbContext;
    private readonly ProtectPassword _protect;
    private readonly FileHelper _fileHelper;

    public ContactUsController(AppDbContext appDb, ProtectPassword protect, FileHelper fileHelper)
    {
        _appDbContext = appDb;
        _protect = protect;
        _fileHelper = fileHelper;
    }
    #endregion

    #region Methods

    [HttpGet]
    public async Task<List<ContactUs>> Get()
    {
        try
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
        catch (Exception)
        {
            return new List<ContactUs>();
        }
    }

    [HttpGet("{id}")]
    public async Task<ContactUs> Get(long id)
    {
        try
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
        catch (Exception)
        {
            return new ContactUs();
        }
    }

    [HttpPost]
    public async Task<bool> Create([FromForm] ContactUsDto model)
    {
        try
        {
            var _model = model.Mapper();

            if (model.Image != null)
            {
                _model.Image = await _fileHelper.SaveFile(model.Image, "Images");
            }

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
        catch (Exception)
        {
            return false;
        }
    }

    [HttpPut]
    public async Task<bool> Update([FromForm] ContactUsDto model)
    {
        try
        {
            var _model = model.Mapper();

            if (model.Image != null)
            {
                _model.Image = await _fileHelper.SaveFile(model.Image, "Images");
            }

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

            _appDbContext.ContactUs.Remove(_model);

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

    [HttpPost("deleteContactUs")]
    public async Task<bool> Delete([FromBody] IEnumerable<long> ids)
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
