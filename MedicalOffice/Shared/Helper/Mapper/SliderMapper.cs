
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;

namespace MedicalOffice.Shared.Helper.Mapper;

public static class SliderMapper
{
    public static Slider Mapper(this SliderDto model)
    {
        var result = new Slider()
        {
            Id = model.Id,
            Title = model.Title,
            Image = model.ImageUrl

        };

        return result;
    }

    public static SliderDto Mapper(this Slider model)
    {
        var result = new SliderDto()
        {
            Id = model.Id,
            Title = model.Title,
            ImageUrl=model.Image
        };

        return result;
    }


    public static IEnumerable<SliderDto> Mapper(this IEnumerable<Slider> models)
    {

        var result = models.Select(model => new SliderDto
        {
            Id = model.Id,
            Title = model.Title,
            ImageUrl = model.Image
        });

        return result;
    }
    public static IEnumerable<Slider> Mapper(this IEnumerable<SliderDto> models)
    {

        var result = models.Select(model => new Slider
        {
            Id = model.Id,
            Title = model.Title,
            Image = model.ImageUrl,
        });

        return result;
    }
}
