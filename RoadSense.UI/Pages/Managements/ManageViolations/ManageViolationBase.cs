using Microsoft.AspNetCore.Components;
using MudBlazor;
using RoadSense.UI.Models;
using RoadSense.UI.Pages.Managements.ManageViolations.Modifications;
using RoadSense.UI.Services.ManageViolationService;
using System.Diagnostics;
using System.Net.Http.Json;


namespace RoadSense.UI.Pages.Managements.ManageViolations;

public class ManageViolationBase : ComponentBase
{
    [Inject] protected HttpClient Http { get; set; }
    [Inject] protected IDialogService DialogService { get; set; }
    [Inject] protected ViolationService ViolationService { get; set; }
    public List<ManageViolationModel> pagedData = new List<ManageViolationModel>();
    public MudTable<ManageViolationModel> table;

    public int totalItems;
    public string searchString = null;
    public string statusFilter = null;
    public async Task<TableData<ManageViolationModel>> ServerReload(TableState state, CancellationToken token)
    {
        var response = await ViolationService.GetViolationsAsync(
            skip: state.Page * state.PageSize,
            take: state.PageSize,
            search: searchString,
            status: statusFilter,
            token: token
        );

        pagedData = response.Data;
        totalItems = response.TotalRecords;

        return new TableData<ManageViolationModel>() { TotalItems = totalItems, Items = pagedData };
    }

    public void OnSearch(string text)
    {
        searchString = text;
        table.ReloadServerData();
    }

   
    protected async Task OpenModal(ManageViolationModel violator)
    {
        var parameters = new DialogParameters<UpdateViolation> { { x => x.ViolationModel, violator } };

        var dialog = await DialogService.ShowAsync<UpdateViolation>("Update Violation", parameters);
        var result = await dialog.Result;

        if (result.Canceled)
        {
            await ViolationService.GetViolationsAsync();
        }

        await table.ReloadServerData();
    }

    protected async Task AddViolator(bool IsAdd)
    {
        var parameters = new DialogParameters<UpdateViolation> { { x => x.IsEditMode, IsAdd } };

        var dialog = await DialogService.ShowAsync<UpdateViolation>("Add Violator", parameters);
        var result = await dialog.Result;

        if (result.Canceled)
        {
            await ViolationService.GetViolationsAsync();
        }

        await table.ReloadServerData();
    }
}