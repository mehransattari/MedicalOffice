using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MedicalOffice.Shared.Helper.Mapper;
using MedicalOffice.Client.Repositories;

namespace MedicalOffice.Client.Pages.Roles;

public class ListRoleBase : ComponentBase
{
    #region ======= Fields ================

    #region Inject
    [Inject]
    public IRoleRepository _RoleRepository { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }
    #endregion

    #region Properties
    public List<Role> roles { get; set; } = new List<Role>();

    public IEnumerable<RoleDto> Roles = new List<RoleDto>();
    public Role EditedRole { get; set; } = new Role();
    public RoleDto RoleBeforeEdit { get; set; } = new RoleDto();

    public string searchString = null;

    public int totalItems;
    #endregion

    #region Settings
    public TableApplyButtonPosition applyButtonPosition = TableApplyButtonPosition.End;

    public TableEditButtonPosition editButtonPosition = TableEditButtonPosition.End;

    public TableEditTrigger editTrigger = TableEditTrigger.EditButton;

    #endregion

    #region Selected Row
    public HashSet<RoleDto> selectedItems = new HashSet<RoleDto>();

    public bool _selectOnRowClick = true;

    public RoleDto _selectedItem = new RoleDto();
    #endregion

    public IEnumerable<RoleDto> pagedData;

    public MudTable<RoleDto> RoleTable;

    #endregion

    #region=======  Methods ==============
    public async Task<TableData<RoleDto>> ServerReload(TableState state)
    {
         if (EditedRole.Id != 0)
        {
            var RoleDto = EditedRole.Mapper();
            var result = await _RoleRepository.UpdateRole(RoleDto);
            if (result.Response)
            {
                StateHasChanged();
                EditedRole.Id = 0;
            }
        }
        if (string.IsNullOrEmpty(searchString))
        {
            await  GetUsers(state);
            GetSortRoles(state);
        }

        if (!string.IsNullOrEmpty(searchString))
        {           
            var resultQuery = await _RoleRepository.GetAllRoles(searchString);

            if (resultQuery.Success)
            {
                Roles = resultQuery.Response.ToArray();
                totalItems = Roles.Count();
            }
        }

        pagedData = Roles.ToArray();
        return new TableData<RoleDto>() { TotalItems = totalItems, Items = pagedData };
    }

    public void OnSearch(string text)
    {
        searchString = text;      
        RoleTable.ReloadServerData();
    }

    #region Row Edit
    /// <summary>
    /// هنگام کلیک روی دکمه ویرایش سطری
    /// </summary>
    /// <param name="Role"></param>
    public void BackupItem(object Role)
    {
        RoleBeforeEdit = new()
        {
            Id = ((RoleDto)Role).Id,
            FaCaption = ((RoleDto)Role).FaCaption,
            EnCaption = ((RoleDto)Role).EnCaption,           
        };
    }
    /// <summary>
    /// ذخیره اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="Role"></param>
    public void ItemHasBeenCommitted(object Role)
    {
        var res = (RoleDto)Role;
        EditedRole = res.Mapper();
        RoleTable.ReloadServerData();
    }
    /// <summary>
    /// کنسل کردن  اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="Role"></param>
    public void ResetItemToOriginalValues(object Role)
    {
        ((RoleDto)Role).Id = RoleBeforeEdit.Id;
        ((RoleDto)Role).FaCaption = RoleBeforeEdit.FaCaption;
        ((RoleDto)Role).EnCaption = RoleBeforeEdit.EnCaption;
  
    }
    #endregion

    #region Delete Message Box
    public MudMessageBox mbox { get; set; }

    string state = "Message box hasn't been opened yet";

    public async void OnButtonClicked()
    {
        bool? result = await mbox.Show();

        state = result == null ? "Canceled" : "Deleted!";
        StateHasChanged();
    }

    public async Task DeleteRoles()
    {
        var ids = selectedItems.Select(x => x.Id);
        await _RoleRepository.DeleteRolesByIds(ids);
        await RoleTable.ReloadServerData();
    }
    #endregion

    #region Add Dialog
    /// <summary>
    /// باز شدن مدال برای افزودن
    /// </summary>
    /// <returns></returns>
    public async Task OpenAddDialogAdd()
    {
        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogRole>("افزودن کاربر", closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await RoleTable.ReloadServerData();
        }
    }
    #endregion

    #region Edit Dialog
    /// <summary>
    /// باز شدن مدال برای ویرایش
    /// </summary>
    /// <returns></returns>
    public async Task OpenAddDialogEdit()
    {
        var parameters = new DialogParameters<FormDialogRole>();
        var Id = selectedItems.Select(x => x.Id).FirstOrDefault();
        var FaCaption = selectedItems.Select(x => x.FaCaption).FirstOrDefault();
        var EnCaption = selectedItems.Select(x => x.EnCaption).FirstOrDefault();
 

        parameters.Add(x => x.Id, Id);
        parameters.Add(x => x.FaCaption, FaCaption);
        parameters.Add(x => x.EnCaption, EnCaption);


        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogRole>("ویرایش نقش", parameters, closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await RoleTable.ReloadServerData();
        }
    }
    #endregion
    public void OnRowClick(TableRowClickEventArgs<RoleDto> args)
    {
        _selectedItem = args.Item;
    }

    #endregion

    #region ======= Private Methods =======

    private async Task GetUsers(TableState state)
    {
        var responseusers = await _RoleRepository.GetAllRoles(state.Page * state.PageSize, state.PageSize);
        if (responseusers.Success)
        {
            Roles = responseusers.Response;
        }
        var resultCount = await _RoleRepository.GetAllRolesCount();
        if (resultCount.Success)
        {
            totalItems = resultCount.Response;
        }

    }
    private void GetSortRoles(TableState state)
    {
        switch (state.SortLabel)
        {
            case "Id_field":
                Roles = Roles.OrderByDirection(state.SortDirection, o => o.Id);
                break;
            case "FirstName_field":
                Roles = Roles.OrderByDirection(state.SortDirection, o => o.FaCaption);
                break;
            case "LastName_field":
                Roles = Roles.OrderByDirection(state.SortDirection, o => o.EnCaption);
                break;
       
        }
    }

    #endregion
}