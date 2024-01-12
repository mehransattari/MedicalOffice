using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Helper;
using Microsoft.AspNetCore.Components;

namespace MedicalOffice.Ui.Pages.Components
{
    public class ReservationComponentBase : ComponentBase
    {

        #region Inject
        [Inject]
        public IDaysReserveRepository daysReserveRepository { get; set; }

        [Inject]
        public ITimesRepository timesRepository { get; set; }
        #endregion

        #region Fields
        public PersianDayOfWeek? CurrentNameDay { get; set; }
        public string? CurrentDateDay { get; set; }
        public List<DaysReserve> DaysReserves { get; set; } = new();
        public List<IGrouping<long, TimesReserve>> TimesReserves { get; set; } = new();
        public string Selected { get; set; } = string.Empty;

        private TimesReserve SelectedTime { get; set; } = new();
        public string FinishInfo { get; set; } = "d-none";
        #endregion

        #region Methods
        protected override async Task  OnInitializedAsync()
        {
            var currentDate = DateTime.Now;
            CurrentDateDay = currentDate.ToShamsi();
            CurrentNameDay = currentDate.ToDayShamsi();
            await ShowDays();
        }

        /// <summary>
        /// Show Dates With Times
        /// </summary>
        /// <returns></returns>
        public async Task ShowDays()
        {
            var dates = await daysReserveRepository.GetTimesDayReserve();
            if (dates.Success)
            {
                var res = dates.Response.ToList();
                DaysReserves = res;
               
                var times = res.SelectMany(x => x.TimesReserves);

                if(times.Any())
                {
                    TimesReserves = times.GroupBy(x => x.DaysReserveId).ToList();
                }


            }
        }
        public void SetSelectedTime(TimesReserve selectTime) => SelectedTime = selectTime;
        public string SelectedClass(TimesReserve time)=> time == SelectedTime ? "selected" : string.Empty;
     
        public void ContinuePurchaseProcess()
        {
            FinishInfo = "d-block";
        }
        #endregion
    }
}
