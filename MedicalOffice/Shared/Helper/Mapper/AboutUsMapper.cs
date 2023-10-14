

using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;

namespace MedicalOffice.Shared.Helper.Mapper;

public static class AboutUsMapper
{
    public static AboutUs Mapper(this AboutUsDto model)
    {
        var result = new AboutUs()
        {
            Id = model.Id,
            Title = model.Title,
            Text = model.Text
        };

        return result;
    }

    public static AboutUsDto Mapper(this AboutUs model)
    {
        var result = new AboutUsDto()
        {
            Id = model.Id,
            Title = model.Title,
            Text = model.Text
        };

        return result;
    }
}
