using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI.Library.API;
using UI.Library.Models;
using WSMDesktop.Models;

namespace WSMDesktop.ViewModels;

public class StockViewModel : Screen
{
    private readonly IStockEndpoint _stockEndpoint;
    private readonly IWindowManager _window;
    private readonly IMapper _mapper;
    private readonly StatusViewModel _status;

    public StockViewModel(IStockEndpoint stockEndpoint,
                          IWindowManager window,
                          IMapper mapper,
                          StatusViewModel status)
	{
        _stockEndpoint = stockEndpoint;
        _window = window;
        _mapper = mapper;
        _status = status;
    }

    protected override async void OnViewLoaded(object view)
    {
        base.OnViewLoaded(view);
        try
        {
            await LoadMachines();
            await LoadParts();
        }
        catch (Exception ex)
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Title = "System Error";

            if (ex.Message == "Unauthorized")
            {
                _status.UpdateMessage("Unauthorized Access", "You do not have permission to interact with this page.");
                await _window.ShowDialogAsync(_status, null, settings);
            }
            else
            {
                _status.UpdateMessage("Fatal Exception", ex.Message);
                await _window.ShowDialogAsync(_status, null, settings);
            }
        }
    }

    public async Task LoadMachines()
    {
        var machineList = await _stockEndpoint.GetAllMachines();
        var machines = _mapper.Map<List<MachineDisplayModel>>(machineList);
        Machines = new BindingList<MachineDisplayModel>(machines);
    }

    public async Task LoadParts()
    {
        var partList = await _stockEndpoint.GetAllParts();
        var parts = _mapper.Map<List<PartDisplayModel>>(partList);
        Parts = new BindingList<PartDisplayModel>(parts);
    }

    private BindingList<MachineDisplayModel> _machines;

    public BindingList<MachineDisplayModel> Machines
    {
        get { return _machines; }
        set 
        {
            _machines = value; 
            NotifyOfPropertyChange(() => Machines);
        }
    }

    private MachineDisplayModel _selectedMachine;

    public MachineDisplayModel SelectedMachine
    {
        get { return _selectedMachine; }
        set 
        { 
            _selectedMachine = value; 
            NotifyOfPropertyChange(() => SelectedMachine);
            NotifyOfPropertyChange(() => SelectedMachineName);
            NotifyOfPropertyChange(() => SelectedMachineModelName);
            NotifyOfPropertyChange(() => SelectedMachineEAN);
            NotifyOfPropertyChange(() => SelectedMachineDatePurchased);
        }
    }


    private BindingList<PartDisplayModel> _parts;

    public BindingList<PartDisplayModel> Parts
    {
        get { return _parts; }
        set 
        { 
            _parts = value; 
            NotifyOfPropertyChange(() => Parts);
        }
    }

    private PartDisplayModel _selectedPart;

    public PartDisplayModel SelectedPart
    {
        get { return _selectedPart; }
        set 
        { 
            _selectedPart = value; 
            NotifyOfPropertyChange(() => SelectedPart);
            NotifyOfPropertyChange(() => SelectedPartName);
            NotifyOfPropertyChange(() => SelectedPartModelName);
            NotifyOfPropertyChange(() => SelectedPartMachineModelName);
            NotifyOfPropertyChange(() => SelectedPartDatePurchased);
        }
    }

    public string SelectedMachineName
    {
        get
        {
            if (SelectedMachine is not null)
            {
                return $"{SelectedMachine.MachineName}";
            }

            return "No Machine Selected";
        }
    }

    public string SelectedMachineModelName
    {
        get
        {
            if (SelectedMachine is not null)
            {
                return $"{SelectedMachine.ModelName}";
            }

            return "No Machine Selected";
        }
    }

    public string SelectedMachineEAN
    {
        get
        {
            if (SelectedMachine is not null)
            {
                return $"{SelectedMachine.EuropeanArticleNumber}";
            }

            return "No Machine Selected";
        }
    }

    public string SelectedMachineDatePurchased
    {
        get
        {
            if (SelectedMachine is not null)
            {
                return $"{SelectedMachine.DatePurchased:dd/MM/yyyy}";
            }

            return "No Machine Selected";
        }
    }

    public string SelectedPartName
    {
        get
        {
            if (SelectedPart is not null)
            {
                return $"{SelectedPart.PartName}";
            }

            return "No Part Selected";
        }
    }

    public string SelectedPartModelName
    {
        get
        {
            if (SelectedPart is not null)
            {
                return $"{SelectedPart.ModelName}";
            }

            return "No Part Selected";
        }
    }

    public string SelectedPartMachineModelName
    {
        get
        {
            if (SelectedPart is not null)
            {
                var selectedMachine = Machines.Where(x => x.Id == SelectedPart.MachineId).FirstOrDefault();
                return $"{selectedMachine.MachineName} - {selectedMachine.ModelName}";
            }

            return "No Part Selected";
        }
    }

    public string SelectedPartDatePurchased
    {
        get
        {
            if (SelectedPart is not null)
            {
                return $"{SelectedPart.DatePurchased:dd/MM/yyyy}";
            }

            return "No Part Selected";
        }
    }
}
