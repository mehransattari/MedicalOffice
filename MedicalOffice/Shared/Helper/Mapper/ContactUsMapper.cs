
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;

namespace MedicalOffice.Shared.Helper.Mapper;

public static class ContactUsMapper
{
    public static ContactUs Mapper(this ContactUsDto model)
    {
        var result = new ContactUs()
        {
            Id = model.Id,
            Title = model.Title,
            Text = model.Text,
            Mobile = model.Mobile,
            Address1 = model.Address1,
            Address2 = model.Address2,
            PhoneNumber = model.PhoneNumber,
            Image=model.ImageUrl
        };

        return result;
    }

    public static ContactUsDto Mapper(this ContactUs model)
    {
        var result = new ContactUsDto()
        {
            Id = model.Id,
            Title = model.Title,
            Text = model.Text,
            Mobile= model.Mobile,
            Address1 = model.Address1,
            Address2 = model.Address2,
            PhoneNumber = model.PhoneNumber,
            ImageUrl=model.Image
        };

        return result;
    }

    public static IEnumerable<ContactUsDto> Mapper(this IEnumerable<ContactUs> models)
    {

        var result = models.Select(model => new ContactUsDto
        {
            Id = model.Id,
            Title = model.Title,
            Text = model.Text,
            Mobile = model.Mobile,
            Address1 = model.Address1,
            Address2 = model.Address2,
            PhoneNumber = model.PhoneNumber,
            ImageUrl = model.Image
        });

        return result;
    }
    public static IEnumerable<ContactUs> Mapper(this IEnumerable<ContactUsDto> models)
    {

        var result = models.Select(model => new ContactUs
        {
            Id = model.Id,
            Title = model.Title,
            Text = model.Text,
            Mobile = model.Mobile,
            Address1 = model.Address1,
            Address2 = model.Address2,
            PhoneNumber = model.PhoneNumber,
            Image = model.ImageUrl
        });

        return result;
    }
}
