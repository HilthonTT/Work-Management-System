using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI.Library.API;

namespace WSMDesktop.ViewModels;

public class AdminStockViewModel : Screen
{
    private readonly IStockEndpoint _stockEndpoint;
    private readonly IWindowManager _window;
    private readonly StatusViewModel _status;

    public AdminStockViewModel(IStockEndpoint stockEndpoint,
							IWindowManager window,
							StatusViewModel status)
	{
        _stockEndpoint = stockEndpoint;
        _window = window;
        _status = status;
    }

    protected override async void OnViewLoaded(object view)
    {
        base.OnViewLoaded(view);
        try
        {
            await LoadAllPart();
            await LoadAllMachines();
        }
        catch (Exception ex)
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Title = "System Error";

            if (ex.Message == "Unauthorized")
            {
                _status.UpdateMessage("Unauthorized Access", "You do not permission to interact with Administrative Stock Page");
                await _window.ShowDialogAsync(_status, null, settings);
            }
            else
            {
                _status.UpdateMessage("Fatal Exception", ex.Message);
                await _window.ShowDialogAsync(_status, null, settings);
            }

            await TryCloseAsync();
        }
    }

    public async Task LoadAllPart()
    {
        await _stockEndpoint.GetAllPartsAdmin();
    }

    public async Task LoadAllMachines()
    {
        await _stockEndpoint.GetAllMachinesAdmin();
    }
}
