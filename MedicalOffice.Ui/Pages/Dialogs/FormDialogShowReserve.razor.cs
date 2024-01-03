using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Repositories.Inteface;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MedicalOffice.Ui.Pages.Dialogs
{
    /// <summary>
    /// Main Class
    /// </summary>
    public partial class FormDialogShowReserveBase : ComponentBase
    {
        #region Constructor
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        public void Submit() => MudDialog.Close(DialogResult.Ok(true));
        public void Cancel() => MudDialog.Cancel();

        [Inject]
        public required IDaysReserveRepository daysReserveRepository { get; set; }

        [Inject]
        public required ITimesRepository timesRepository { get; set; }
        #endregion

        #region Fileds
        public List<DaysReserve> DaysReserves { get; set; }
        public List<TimesReserveDto> TimesReserves { get; set; }

        #endregion

        #region Parameter
        [Parameter]
        public long DayId { get; set; }
        #endregion

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            await ShowDays();
        }

        public async Task ShowDays()
        {
            var dates =await daysReserveRepository.GetDaysReserve();
            if(dates.Success)
            {
                DaysReserves = dates.Response.ToList();
            }
        }

        public async Task ShowTimesByDate()
        {
            var times = await timesRepository.GetTimesReserveByDayId(DayId); 
            if(times.Success)
            {
                TimesReserves = times.Response.ToList();
            }
        }
 
        #endregion
    }


}
