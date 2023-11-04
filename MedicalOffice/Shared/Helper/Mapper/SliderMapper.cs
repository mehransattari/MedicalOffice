
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
            Image = model.ImageUrl,
            ShortDesc1 = model.ShortDesc1,
            ShortDesc2 = model.ShortDesc2,
            ShortDesc3= model.ShortDesc3,
            Property1= model.Property1,
            Property2= model.Property2, 
            Property3= model.Property3,
            Desc= model.Desc,

        };

        return result;
    }

    public static SliderDto Mapper(this Slider model)
    {
        var result = new SliderDto()
        {
            Id = model.Id,
            Title = model.Title,
            ImageUrl=model.Image,
            ShortDesc1 = model.ShortDesc1,
            ShortDesc2 = model.ShortDesc2,
            ShortDesc3 = model.ShortDesc3,
            Property1 = model.Property1,
            Property2 = model.Property2,
            Property3 = model.Property3,
            Desc = model.Desc,
        };

        return result;
    }


    public static IEnumerable<SliderDto> Mapper(this IEnumerable<Slider> models)
    {

        var result = models.Select(model => new SliderDto
        {
            Id = model.Id,
            Title = model.Title,
            ImageUrl = model.Image,
            ShortDesc1 = model.ShortDesc1,
            ShortDesc2 = model.ShortDesc2,
            ShortDesc3 = model.ShortDesc3,
            Property1 = model.Property1,
            Property2 = model.Property2,
            Property3 = model.Property3,
            Desc = model.Desc,
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
            ShortDesc1 = model.ShortDesc1,
            ShortDesc2 = model.ShortDesc2,
            ShortDesc3 = model.ShortDesc3,
            Property1 = model.Property1,
            Property2 = model.Property2,
            Property3 = model.Property3,
            Desc = model.Desc,
        });

        return result;
    }
}
