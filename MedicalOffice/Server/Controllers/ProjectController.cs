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
[Route("api/project")]
public class ProjectController : Controller
{
    #region Constructor
    private readonly AppDbContext _appDbContext;
    private readonly ProtectPassword _protect;
    private readonly FileHelper _fileHelper;

    public ProjectController(AppDbContext appDb, ProtectPassword protect, FileHelper fileHelper)
    {
        _appDbContext = appDb;
        _protect = protect;
        _fileHelper = fileHelper;
    }
    #endregion

    #region Methods

    [HttpGet]
    public async Task<List<Project>> Get()
    {
        try
        {
            var result = await _appDbContext.Projects.ToListAsync();

            if (result != null && result.Count > 0)
            {
                return result;
            }

            else
            {
                return new List<Project>();
            }
        }
        catch (Exception)
        {
            return new List<Project>();
        }
    }

    [HttpGet("{id}")]
    public async Task<Project> Get(long id)
    {
        try
        {
            var result = await _appDbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                return result;
            }

            else
            {
                return new Project();
            }
        }
        catch (Exception)
        {
            return new Project();
        }
    }

    [HttpPost]
    public async Task<bool> Create([FromForm] ProjectDto model)
    {
        try
        {
            var _model = model.Mapper();

            if (model.Image != null)
            {
                _model.Image = await _fileHelper.SaveFile(model.Image, "Images");
            }

            await _appDbContext.Projects.AddAsync(_model);

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
    public async Task<bool> Update([FromForm] ProjectDto model)
    {
        try
        {
            var _model = model.Mapper();

            if (model.Image != null)
            {
                _model.Image = await _fileHelper.SaveFile(model.Image, "Images");
            }

            _appDbContext.Projects.Update(_model);

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

            _appDbContext.Projects.Remove(_model);

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

    [HttpPost("deleteProject")]
    public async Task<bool> Delete([FromBody] IEnumerable<long> ids)
    {
        var models = await _appDbContext.Projects.Where(u => ids.Contains(u.Id)).ToListAsync();

        if (models.Any())
        {
            _appDbContext.Projects.RemoveRange(models);

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
