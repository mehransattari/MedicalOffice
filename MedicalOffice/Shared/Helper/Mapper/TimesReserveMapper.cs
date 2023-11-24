
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;

namespace MedicalOffice.Shared.Helper.Mapper;

public static class TimesReserveMapper
{
    public static TimesReserve Mapper(this TimesReserveDto model)
    {
        var result = new TimesReserve()
        {
            Id = model.Id,
            DaysReserveId = model.DaysReserveId,
            FromTime = model.FromTime,
            ToTime = model.ToTime
        };

        return result;
    }

    public static TimesReserveDto Mapper(this TimesReserve model)
    {
        var result = new TimesReserveDto()
        {
            Id = model.Id,
            DaysReserveId = model.DaysReserveId,
            FromTime = model.FromTime,
            ToTime = model.ToTime
        };

        return result;
    }

    public static IEnumerable<TimesReserveDto> Mapper(this IEnumerable<TimesReserve> models)
    {

        var result = models.Select(model => new TimesReserveDto
        {
            Id = model.Id,
            DaysReserveId = model.DaysReserveId,
            FromTime = model.FromTime,
            ToTime = model.ToTime
        });

        return result;
    }

    public static IEnumerable<TimesReserve> Mapper(this IEnumerable<TimesReserveDto> models)
    {

        var result = models.Select(model => new TimesReserve
        {
            Id = model.Id,
            DaysReserveId = model.DaysReserveId,
            FromTime = model.FromTime,
            ToTime = model.ToTime
        });

        return result;
    }
}
