
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;

namespace MedicalOffice.Shared.Helper.Mapper;

public static class ProvidingServiceMapper
{
    public static ProvidingService Mapper(this ProvidingServiceDto model)
    {
        var result = new ProvidingService()
        {
            Id = model.Id,
            Title = model.Title,
            Desc = model.Desc,
            Image = model.ImageUrl,
            ShortDesc = model.ShortDesc,


        };

        return result;
    }

    public static ProvidingServiceDto Mapper(this ProvidingService model)
    {
        var result = new ProvidingServiceDto()
        {
            Id = model.Id,
            Title = model.Title,
            Desc = model.Desc,
            ShortDesc = model.ShortDesc,
            ImageUrl = model.Image,

        };

        return result;
    }


    public static IEnumerable<ProvidingServiceDto> Mapper(this IEnumerable<ProvidingService> models)
    {

        var result = models.Select(model => new ProvidingServiceDto
        {
            Id = model.Id,
            Title = model.Title,
            Desc = model.Desc,
            ImageUrl = model.Image,
            ShortDesc = model.ShortDesc,

        });

        return result;
    }
    public static IEnumerable<ProvidingService> Mapper(this IEnumerable<ProvidingServiceDto> models)
    {

        var result = models.Select(model => new ProvidingService
        {
            Id = model.Id,
            Title = model.Title,
            Desc = model.Desc,
            ShortDesc = model.ShortDesc,

        });

        return result;
    }
}
