using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;

namespace MedicalOffice.Shared.Helper.Mapper;

public static class ReserveMapper
{
    public static Reservation Mapper(this ReserveDto reserveDto)
    {
        var reservation = new Reservation()
        {
            UserId = reserveDto.UserId,
            TimesReserveId = reserveDto.TimesReserveId,
            Id = reserveDto.Id,
            CreateDate = reserveDto.CreateDate,
            UpdateDate = reserveDto.UpdateDate,
        };

        return reservation;
    }

    public static ReserveDto Mapper(this Reservation reserve)
    {
        var reserveDto = new ReserveDto()
        {
            UserId = reserve.UserId,
            TimesReserveId = reserve.TimesReserveId,
            Id = reserve.Id,
            ReserveType = reserve.ReserveType,
            CreateDate = reserve.CreateDate,
            UpdateDate = reserve.UpdateDate,
        };

        return reserveDto;
    }
    public static IEnumerable<ReserveDto> Mapper(this IEnumerable<Reservation> models)
    {

        var result = models.Select(model => new ReserveDto
        {
            UserId = model.UserId,
            TimesReserveId = model.TimesReserveId,
            Id = model.Id,
            ReserveType = model.ReserveType,
            CreateDate = model.CreateDate,
            UpdateDate = model.UpdateDate,
        });

        return result;
    }
    public static IEnumerable<Reservation> Mapper(this IEnumerable<ReserveDto> models)
    {

        var result = models.Select(model => new Reservation
        {
            UserId = model.UserId,
            TimesReserveId = model.TimesReserveId,
            Id = model.Id,
            CreateDate = model.CreateDate,
            UpdateDate = model.UpdateDate,
        });


        return result;
    }
}
