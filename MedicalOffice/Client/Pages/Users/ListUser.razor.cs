using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MedicalOffice.Shared.Helper.Mapper;

namespace MedicalOffice.Client.Pages.Users;

public class ListUserBase : ComponentBase
{
    #region ======= Fields ================

    #region Inject
    [Inject]
    public IUserRepository _userRepository { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }
    #endregion

    #region Properties
    public List<Role> roles { get; set; } = new List<Role>();

    public IEnumerable<UserDto> Users = new List<UserDto>();
    public User EditedUser { get; set; } = new User();
    public UserDto UserBeforeEdit { get; set; } = new UserDto();

    public string searchString = null;

    public int totalItems;
    #endregion

    #region Settings
    public TableApplyButtonPosition applyButtonPosition = TableApplyButtonPosition.End;

    public TableEditButtonPosition editButtonPosition = TableEditButtonPosition.End;

    public TableEditTrigger editTrigger = TableEditTrigger.EditButton;

    #endregion

    #region Selected Row
    public HashSet<UserDto> selectedItems = new HashSet<UserDto>();

    public bool _selectOnRowClick = true;

    public UserDto _selectedItem = new UserDto();
    #endregion

    public IEnumerable<UserDto> pagedData;

    public MudTable<UserDto> UserTable;

    #endregion

    #region=======  Methods ==============
    public async Task<TableData<UserDto>> ServerReload(TableState state)
    {
         if (EditedUser.Id != 0)
        {
            var userDto = EditedUser.Mapper();
            var result = await _userRepository.UpdateUser(userDto);
            if (result.Response)
            {
                StateHasChanged();
                EditedUser.Id = 0;
            }
        }
        if (string.IsNullOrEmpty(searchString))
        {
            await GetUsers(state);
            GetSortUsers(state);
        }

        if (!string.IsNullOrEmpty(searchString))
        {           
            var resultQuery = await _userRepository.GetAllUsers(searchString);

            if (resultQuery.Success)
            {
                Users = resultQuery.Response.ToArray();
                totalItems = Users.Count();
            }
        }

        pagedData = Users.ToArray();
        return new TableData<UserDto>() { TotalItems = totalItems, Items = pagedData };
    }

    public void OnSearch(string text)
    {
        searchString = text;      
        UserTable.ReloadServerData();
    }

    #region Row Edit
    /// <summary>
    /// هنگام کلیک روی دکمه ویرایش سطری
    /// </summary>
    /// <param name="User"></param>
    public void BackupItem(object User)
    {
        UserBeforeEdit = new()
        {
            Id = ((UserDto)User).Id,
            FirstName = ((UserDto)User).FirstName,
            LastName = ((UserDto)User).LastName,
            Mobile = ((UserDto)User).Mobile,
            RoleId = ((UserDto)User).RoleId,
        };
    }
    /// <summary>
    /// ذخیره اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="User"></param>
    public void ItemHasBeenCommitted(object user)
    {
        var res = (UserDto)user;
        EditedUser = res.Mapper();
        UserTable.ReloadServerData();
    }
    /// <summary>
    /// کنسل کردن  اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="User"></param>
    public void ResetItemToOriginalValues(object User)
    {
        ((UserDto)User).Id = UserBeforeEdit.Id;
        ((UserDto)User).FirstName = UserBeforeEdit.FirstName;
        ((UserDto)User).LastName = UserBeforeEdit.LastName;
        ((UserDto)User).Mobile = UserBeforeEdit.Mobile;
        ((UserDto)User).RoleId = UserBeforeEdit.RoleId;
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

    public async Task DeleteUsers()
    {
        var ids = selectedItems.Select(x => x.Id);
        await _userRepository.DeleteUsersByIds(ids);
        await UserTable.ReloadServerData();
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

        var result = await DialogService.Show<FormDialogUser>("افزودن کاربر", closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await UserTable.ReloadServerData();
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
        var parameters = new DialogParameters<FormDialogUser>();
        var Id = selectedItems.Select(x => x.Id).FirstOrDefault();
        var FirstName = selectedItems.Select(x => x.FirstName).FirstOrDefault();
        var LastName = selectedItems.Select(x => x.LastName).FirstOrDefault();
        var Mobile = selectedItems.Select(x => x.Mobile).FirstOrDefault();
        var RoleId = selectedItems.Select(x => x.RoleId).FirstOrDefault();

        parameters.Add(x => x.Id, Id);
        parameters.Add(x => x.FirstName, FirstName);
        parameters.Add(x => x.LastName, LastName);
        parameters.Add(x => x.Mobile, Mobile);
        parameters.Add(x => x.RoleId, RoleId);

        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogUser>("ویرایش کاربر", parameters, closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await UserTable.ReloadServerData();
        }
    }
    #endregion
    public void OnRowClick(TableRowClickEventArgs<UserDto> args)
    {
        _selectedItem = args.Item;
    }
   
    #endregion

    #region ======= Private Methods =======
    private async Task GetUsers(TableState state)
    {
        var responseusers = await _userRepository.GetAllUsers(state.Page * state.PageSize, state.PageSize);
        if (responseusers.Success)
        {
            Users = responseusers.Response;
        }
        var resultCount = await _userRepository.GetAllUsersCount();
        if (resultCount.Success)
        {
            totalItems = resultCount.Response;
        }

        var resultRoles = await _userRepository.Roles();
        if (resultRoles.Success)
        {
            roles = resultRoles.Response;
        }
    }

    private void GetSortUsers(TableState state)
    {
        switch (state.SortLabel)
        {
            case "Id_field":
                Users = Users.OrderByDirection(state.SortDirection, o => o.Id);
                break;
            case "FirstName_field":
                Users = Users.OrderByDirection(state.SortDirection, o => o.FirstName);
                break;
            case "LastName_field":
                Users = Users.OrderByDirection(state.SortDirection, o => o.LastName);
                break;
            case "Mobile_field":
                Users = Users.OrderByDirection(state.SortDirection, o => o.Mobile);
                break;
        }
    }

    #endregion
}