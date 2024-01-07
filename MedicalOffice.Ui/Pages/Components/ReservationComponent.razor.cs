using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Helper;
using Microsoft.AspNetCore.Components;
using static MedicalOffice.Ui.Services.Helper.ConvertDate;

namespace MedicalOffice.Ui.Pages.Components
{
    public class ReservationComponentBase : ComponentBase
    {

        #region Inject
        [Inject]
        public required IDaysReserveRepository daysReserveRepository { get; set; }

        [Inject]
        public required ITimesRepository timesRepository { get; set; }
        #endregion

        #region Fields
        public PersianDayOfWeek? CurrentNameDay { get; set; }
        public  string? CurrentDateDay { get; set; }
        public List<DaysReserve>? DaysReserves { get; set; }
        public List<IGrouping<long,TimesReserve>>? TimesReserves { get; set; }
        #endregion

        #region Methods
        protected override async Task  OnInitializedAsync()
        {
            var currentDate = DateTime.Now;
            CurrentDateDay = currentDate.ToShamsi();
            CurrentNameDay = currentDate.ToDayShamsi();
            await ShowDays();
        }

        public async Task ShowDays()
        {
            var dates = await daysReserveRepository.GetTimesDayReserve();
            if (dates.Success)
            {
                var res = dates.Response.ToList();
                DaysReserves = res;
               // TimesReserves = res.SelectMany(x => x.TimesReserves).GroupBy(x=>x.DaysReserveId).ToList();
               TimesReserves = res
                                .SelectMany(x => x.TimesReserves)
                                .GroupBy(x => x.DaysReserveId)
                                .ToList();
            }
        }

   
        #endregion
    }
}
