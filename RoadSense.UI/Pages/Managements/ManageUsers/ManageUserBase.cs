using Microsoft.AspNetCore.Components;
using MudBlazor;
using RoadSense.UI.Models;
using RoadSense.UI.Pages.Managements.ManageUsers.Modifications;
using RoadSense.UI.Pages.Managements.ManageViolations.Modifications;
using RoadSense.UI.Services.ManageUsers;
using RoadSense.UI.Services.ManageViolationService;

namespace RoadSense.UI.Pages.Managements.ManageUsers;

public class ManageUserBase : ComponentBase
{
    [Inject] protected HttpClient Http { get; set; }
    [Inject] protected IDialogService DialogService { get; set; }
    [Inject] protected UserService UserService { get; set; }
    public List<UserModel> pagedData = new List<UserModel>();
    public MudTable<UserModel> table;

    public int totalItems;
    public string searchString = null;
    public string statusFilter = null;
    public async Task<TableData<UserModel>> ServerReload(TableState state, CancellationToken token)
    {
        var response = await UserService.GetUsersAsync(
            skip: state.Page * state.PageSize,
            take: state.PageSize,
            search: searchString,
            status: statusFilter,
            token: token
        );

        pagedData = response.Data;
        totalItems = response.TotalRecords;

        return new TableData<UserModel>() { TotalItems = totalItems, Items = pagedData };
    }

    public void OnSearch(string text)
    {
        searchString = text;
        table.ReloadServerData();
    }


    protected async Task OpenModal(UserModel violator)
    {
        var parameters = new DialogParameters<UpdateUser> { { x => x.UserModel, violator } };
        DialogOptions _maxWidth = new() { MaxWidth = MaxWidth.Medium, FullWidth = true, Position = DialogPosition.TopCenter };

        var dialog = await DialogService.ShowAsync<UpdateUser>("Update User", parameters, _maxWidth);
        var result = await dialog.Result;

        if (result.Canceled)
        {
            await UserService.GetUsersAsync();
        }

        await table.ReloadServerData();
    }

    protected async Task AddViolator(bool IsAdd)
    {
        var parameters = new DialogParameters<UpdateUser> { { x => x.IsEditMode, IsAdd } };
        DialogOptions _maxWidth = new() { MaxWidth = MaxWidth.Medium, FullWidth = true, Position = DialogPosition.TopCenter };
       
        var dialog = await DialogService.ShowAsync<UpdateUser>("Add User", parameters, _maxWidth);
        var result = await dialog.Result;

        if (result.Canceled)
        {
            await UserService.GetUsersAsync();
        }

        await table.ReloadServerData();
    }
}