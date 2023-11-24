

using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;

namespace MedicalOffice.Shared.Helper.Mapper;

public static class DaysReserveMapper
{
    public static DaysReserve Mapper(this DaysReserveDto model)
    {
        var result = new DaysReserve()
        {
            Id = model.Id,
            Day = model.Day,        
        };

        return result;
    }

    public static DaysReserveDto Mapper(this DaysReserve model)
    {
        var result = new DaysReserveDto()
        {
            Id = model.Id,        
            Day = model.Day,
        };

        return result;
    }

    public static IEnumerable<DaysReserveDto> Mapper(this IEnumerable<DaysReserve> models)
    {

        var result = models.Select(model => new DaysReserveDto
        {
            Id = model.Id,
            Day = model.Day,
        });

        return result;
    }

    public static IEnumerable<DaysReserve> Mapper(this IEnumerable<DaysReserveDto> models)
    {

        var result = models.Select(model => new DaysReserve
        {
            Id = model.Id,
            Day = model.Day,
        });

        return result;
    }
}
