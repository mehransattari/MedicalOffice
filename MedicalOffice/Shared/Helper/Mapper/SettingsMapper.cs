

using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;

namespace MedicalOffice.Shared.Helper.Mapper;

public static class SettingsMapper
{
    public static Settings Mapper(this SettingsDto model)
    {
        var result = new Settings()
        {
            Id = model.Id,
            SiteName = model.SiteName,
            ShortDescription = model.ShortDescription,
            Logo = model.Logo,
            Instagram = model.Instagram,
            Telegram = model.Telegram,
            Whatsapp = model.Whatsapp

        };

        return result;
    }

    public static SettingsDto Mapper(this Settings model)
    {
        var result = new SettingsDto()
        {
            Id = model.Id,
            SiteName = model.SiteName,
            ShortDescription = model.ShortDescription,
            Logo = model.Logo,
            Instagram = model.Instagram,
            Telegram = model.Telegram,
            Whatsapp = model.Whatsapp
        };

        return result;
    }


    public static IEnumerable<SettingsDto> Mapper(this IEnumerable<Settings> models)
    {

        var result = models.Select(model => new SettingsDto
        {
            Id = model.Id,
            SiteName = model.SiteName,
            ShortDescription = model.ShortDescription,
            Logo = model.Logo,
            Instagram = model.Instagram,
            Telegram = model.Telegram,
            Whatsapp = model.Whatsapp
        });

        return result;
    }
    public static IEnumerable<Settings> Mapper(this IEnumerable<SettingsDto> models)
    {

        var result = models.Select(model => new Settings
        {
            Id = model.Id,
            SiteName = model.SiteName,
            ShortDescription = model.ShortDescription,
            Logo = model.Logo,
            Instagram = model.Instagram,
            Telegram = model.Telegram,
            Whatsapp = model.Whatsapp
        });

        return result;
    }
}
