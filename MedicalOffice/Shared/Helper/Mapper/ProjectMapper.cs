
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;

namespace MedicalOffice.Shared.Helper.Mapper;

public static class ProjectMapper
{
    public static Project Mapper(this ProjectDto model)
    {
        var result = new Project()
        {
            Id = model.Id,
            Title = model.Title,
            Desc = model.Desc,      
            Image = model.ImageUrl
        };

        return result;
    }

    public static ProjectDto Mapper(this Project model)
    {
        var result = new ProjectDto()
        {
            Id = model.Id,
            Title = model.Title,
            Desc = model.Desc,
            ImageUrl = model.Image
        };

        return result;
    }

    public static IEnumerable<ProjectDto> Mapper(this IEnumerable<Project> models)
    {

        var result = models.Select(model => new ProjectDto
        {
            Id = model.Id,
            Title = model.Title,
            Desc = model.Desc,
            ImageUrl = model.Image
        });

        return result;
    }
    public static IEnumerable<Project> Mapper(this IEnumerable<ProjectDto> models)
    {

        var result = models.Select(model => new Project
        {
            Id = model.Id,
            Title = model.Title,
            Desc = model.Desc,
            Image = model.ImageUrl
        });

        return result;
    }
}
