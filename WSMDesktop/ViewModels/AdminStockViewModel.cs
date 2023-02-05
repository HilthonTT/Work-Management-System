﻿using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UI.Library.API;
using UI.Library.Models;
using WSMDesktop.Models;

namespace WSMDesktop.ViewModels;

public class AdminStockViewModel : Screen
{
    private readonly IStockEndpoint _stockEndpoint;
    private readonly IMapper _mapper;
    private readonly IWindowManager _window;
    private readonly StatusViewModel _status;
    
    public AdminStockViewModel(IStockEndpoint stockEndpoint,
                               IMapper mapper,
                               IWindowManager window,
                               StatusViewModel status)
	{
        _stockEndpoint = stockEndpoint;
        _mapper = mapper;
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
        var partList = await _stockEndpoint.GetAllPartsAdmin();
        var parts = _mapper.Map<List<PartDisplayModel>>(partList);
        Parts = new BindingList<PartDisplayModel>(parts);
    }

    public async Task LoadAllMachines()
    {
        var machineList = await _stockEndpoint.GetAllMachinesAdmin();
        var machines = _mapper.Map<List<MachineDisplayModel>>(machineList);
        Machines = new BindingList<MachineDisplayModel>(machines);
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
            MachineName = value?.MachineName;
            ModelNameMachine = value?.ModelName;
            SelectedMachineName = value?.MachineName ?? "No Machine Selected";
            EuropeanArticleNumber = value?. EuropeanArticleNumber;
            PurchasedPriceMachine = value?.PurchasedPrice.ToString();
            DatePurchasedMachine = value?.DatePurchased;
            NotifyOfPropertyChange(() => SelectedMachine);
            NotifyOfPropertyChange(() => SelectedMachineName);
            NotifyOfPropertyChange(() => CanAddMachine);
            NotifyOfPropertyChange(() => CanDeleteSelectedMachine);
            NotifyOfPropertyChange(() => DeleteMachineButtonColor);
            NotifyOfPropertyChange(() => CanAddPart);
            NotifyOfPropertyChange(() => AddPartButtonColor);
            NotifyOfPropertyChange(() => CanUpdateMachine);
            NotifyOfPropertyChange(() => UpdateMachineButtonColor);
        }
    }

    private BindingList<PartDisplayModel> _parts;

    public BindingList<PartDisplayModel> Parts
    {
        get { return _parts; }
        set 
        { 
            _parts = value; 
            NotifyOfPropertyChange(() => Parts) ;
        }
    }

    private PartDisplayModel _selectedPart;

    public PartDisplayModel SelectedPart
    {
        get { return _selectedPart; }
        set 
        { 
            _selectedPart = value; 
            PartName = value?.PartName;
            ModelNamePart = value?.ModelName;
            PurchasedPricePart = value?.PurchasedPrice.ToString();
            SelectedMachineName = Machines.Where(x => x.Id == value?.MachineId).FirstOrDefault()?.MachineName;
            DatePurchasedPart = value?.DatePurchased;
            NotifyOfPropertyChange(() => SelectedPart);
            NotifyOfPropertyChange(() => CanDeleteSelectedPart);
            NotifyOfPropertyChange(() => DeletePartButtonColor);
            NotifyOfPropertyChange(() => CanUpdatePart);
            NotifyOfPropertyChange(() => UpdatePartButtonColor);
        }
    }

    private string _machineName;

    public string MachineName
    {
        get { return _machineName; }
        set 
        { 
            _machineName = value; 
            NotifyOfPropertyChange(() => MachineName);
            NotifyOfPropertyChange(() => CanAddMachine);
            NotifyOfPropertyChange(() => AddMachineButtonColor);
        }
    }

    private string _modelNameMachine;

    public string ModelNameMachine
    {
        get { return _modelNameMachine; }
        set 
        { 
            _modelNameMachine = value; 
            NotifyOfPropertyChange(() => ModelNameMachine);
            NotifyOfPropertyChange(() => CanAddMachine);
            NotifyOfPropertyChange(() => AddMachineButtonColor);
        }
    }

    private string _europeanArticleNumber;

    public string EuropeanArticleNumber
    {
        get { return _europeanArticleNumber; }
        set 
        { 
            _europeanArticleNumber = value; 
            NotifyOfPropertyChange(() => EuropeanArticleNumber);
            NotifyOfPropertyChange(() => CanAddMachine);
            NotifyOfPropertyChange(() => AddMachineButtonColor);
        }
    }

    private string _purchasedPriceMachine;

    public string PurchasedPriceMachine
    {
        get { return _purchasedPriceMachine; }
        set 
        { 
            _purchasedPriceMachine = value; 
            NotifyOfPropertyChange(() => PurchasedPriceMachine);
            NotifyOfPropertyChange(() => CanAddMachine);
            NotifyOfPropertyChange(() => AddMachineButtonColor);
        }
    }


    private DateTime? _datePurchasedMachine = SqlDateTime.MinValue.Value;

    public DateTime? DatePurchasedMachine
    {
        get { return _datePurchasedMachine; }
        set 
        { 
            _datePurchasedMachine = value; 
            NotifyOfPropertyChange(() => DatePurchasedMachine);
            NotifyOfPropertyChange(() => CanAddMachine);
            NotifyOfPropertyChange(() => AddMachineButtonColor);
        }
    }

    private string _partName;

    public string PartName
    {
        get { return _partName; }
        set
        {
            _partName = value;
            NotifyOfPropertyChange(() => PartName);
            NotifyOfPropertyChange(() => CanAddPart);
            NotifyOfPropertyChange(() => AddPartButtonColor);
        }
    }

    private string _modelNamePart;

    public string ModelNamePart
    {
        get { return _modelNamePart; }
        set 
        { 
            _modelNamePart = value; 
            NotifyOfPropertyChange(() => ModelNamePart);
            NotifyOfPropertyChange(() => CanAddPart);
            NotifyOfPropertyChange(() => AddPartButtonColor);
        }
    }

    private string _selectedMachineName;

    public string SelectedMachineName
    {
        get { return _selectedMachineName; }
        set 
        { 
            _selectedMachineName = value; 
            NotifyOfPropertyChange(() => SelectedMachineName);
        }
    }


    private string _purchasedPricePart;

    public string PurchasedPricePart
    {
        get { return _purchasedPricePart; }
        set 
        { 
            _purchasedPricePart = value; 
            NotifyOfPropertyChange(() => PurchasedPricePart);
            NotifyOfPropertyChange(() => CanAddPart);
            NotifyOfPropertyChange(() => AddPartButtonColor);
        }
    }


    private DateTime? _datePurchasedPart = SqlDateTime.MinValue.Value;

    public DateTime? DatePurchasedPart
    {
        get { return _datePurchasedPart; }
        set 
        { 
            _datePurchasedPart = value;
            NotifyOfPropertyChange(() => DatePurchasedPart);
            NotifyOfPropertyChange(() => CanAddPart);
            NotifyOfPropertyChange(() => AddPartButtonColor);
        }
    }

    public bool CanAddMachine
    {
        get
        {
            if (string.IsNullOrWhiteSpace(MachineName) == false &&
                string.IsNullOrWhiteSpace(ModelNameMachine) == false &&
                string.IsNullOrWhiteSpace(EuropeanArticleNumber) == false &&
                string.IsNullOrWhiteSpace(PurchasedPriceMachine) == false &&
                DatePurchasedMachine >= SqlDateTime.MinValue.Value)
            {
                return true;
            }

            return false;

        }
    }

    public string AddMachineButtonColor
    {
        get
        {
            if (CanAddMachine is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task AddMachine()
    {
        bool isConvertedToDecimal = decimal.TryParse(PurchasedPriceMachine, out var PurchasedPrice);

        if (isConvertedToDecimal)
        {
            MachineModel machine = new()
            {
                MachineName = MachineName,
                ModelName = ModelNameMachine,
                EuropeanArticleNumber = EuropeanArticleNumber,
                PurchasedPrice = PurchasedPrice,
                DatePurchased = DatePurchasedMachine.Value
            };

            MachineName = "";
            ModelNameMachine = "";
            EuropeanArticleNumber = "";
            PurchasedPriceMachine = "";
            DatePurchasedMachine = SqlDateTime.MinValue.Value;

            await _stockEndpoint.InsertMachine(machine);
            await LoadAllMachines();
        }
        else
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Title = "System Error";

            _status.UpdateMessage("Fatal Exception", "The Purchased Price wasn't a convertable number.");
            await _window.ShowDialogAsync(_status, null, settings);
        }
    }

    public bool CanAddPart
    {
        get
        {
            if (string.IsNullOrWhiteSpace(PartName) == false &&
                string.IsNullOrWhiteSpace(ModelNamePart) == false &&
                string.IsNullOrWhiteSpace(SelectedMachineName) == false &&
                SelectedMachine is not null &&
                string.IsNullOrWhiteSpace(PurchasedPricePart) == false &&
                DatePurchasedPart >= SqlDateTime.MinValue.Value)
            {
                return true;
            }

            return false;

        }
    }

    public string AddPartButtonColor
    {
        get
        {
            if (CanAddPart is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task AddPart()
    {
        bool isConvertedToDecimal = decimal.TryParse(PurchasedPricePart, out decimal purchasedPrice);

        if (isConvertedToDecimal)
        {
            PartModel part = new()
            {
                PartName = PartName,
                ModelName = ModelNamePart,
                MachineId = SelectedMachine.Id,
                PurchasedPrice = purchasedPrice,
                DatePurchased = DatePurchasedPart.Value
            };

            PartName = "";
            ModelNamePart = "";
            PurchasedPricePart = "";
            DatePurchasedMachine = SqlDateTime.MinValue.Value;
            SelectedMachine = null;

            await _stockEndpoint.InsertPart(part);
            await LoadAllPart();
        }
        else
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Title = "System Error";

            _status.UpdateMessage("Fatal Exception", "The Purchased Price wasn't a convertable number.");
            await _window.ShowDialogAsync(_status, null, settings);
        }
    }

    public bool CanDeleteSelectedMachine
    {
        get
        {
            if (SelectedMachine is not null)
                return true;

            return false;
        }
    }

    public string DeleteMachineButtonColor
    {
        get
        {
            if (CanDeleteSelectedMachine)
            {
                return "#121212";
            }

            return "Red";
        }
    }


    public async Task DeleteSelectedMachine()
    {
        await _stockEndpoint.DeleteMachine(SelectedMachine.Id);
        Machines.Remove(SelectedMachine);
    }


    public bool CanDeleteSelectedPart
    {
        get
        {
            if (SelectedPart is not null)
            {
                return true;
            }

            return false;
        }
    }

    public string DeletePartButtonColor
    {
        get
        {
            if (CanDeleteSelectedPart)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task DeleteSelectedPart()
    {
        await _stockEndpoint.DeletePart(SelectedPart.Id);
        Parts.Remove(SelectedPart);
    }


    private string _searchMachineText;

    public string SearchMachineText
    {
        get { return _searchMachineText; }
        set 
        {
            _searchMachineText = value;
            NotifyOfPropertyChange(() => SearchMachineText);
            NotifyOfPropertyChange(() => CanSearchMachine);
            NotifyOfPropertyChange(() => SearchMachineButtonColor);
        }
    }

    public static bool CanSearchMachine
    {
        get
        {
            return true;
        }
    }

    public static string SearchMachineButtonColor
    {
        get
        {
            if (CanSearchMachine is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }


    public async Task SearchMachine()
    {
        if (string.IsNullOrWhiteSpace(SearchMachineText))
        {
            await LoadAllMachines();
        }
        else
        {
            await LoadAllMachines();

            var machineList = Machines.Where(x => x.ModelName.Contains(SearchMachineText) ||
                                             x.MachineName.Contains(SearchMachineText)).ToList();

            Machines = new BindingList<MachineDisplayModel>(machineList);
        }
    }

    private string _searchPartText;

    public string SearchPartText
    {
        get { return _searchPartText; }
        set 
        { 
            _searchPartText = value; 
            NotifyOfPropertyChange(() => SearchPartText);
        }
    }

    public static bool CanSearchPart
    {
        get
        {
            return true;
        }
    }


    public static string SearchPartButtonColor
    {
        get
        {
            if (CanSearchPart is true)
            {
                return "#121212";
            }

            return "Red";
        } 
    }

    public async Task SearchPart()
    {
        if (string.IsNullOrWhiteSpace(SearchPartText))
        {
            await LoadAllPart();
        }
        else
        {
            await LoadAllPart();

            var partList = Parts.Where(x => x.ModelName.Contains(SearchPartText) || 
                                       x.PartName.Contains(SearchPartText)).ToList();

            Parts = new BindingList<PartDisplayModel>(partList);
        }
    }

    public bool CanUpdateMachine
    {
        get
        {
            if (SelectedMachine is not null && CanAddMachine is true)
            {
                return true;
            }

            return false;
        }
    }

    public string UpdateMachineButtonColor
    {
        get
        {
            if (CanUpdateMachine is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task UpdateMachine()
    {
        bool isConvertedToDecimal = decimal.TryParse(PurchasedPriceMachine, out var PurchasedPrice);

        if (isConvertedToDecimal is true)
        {
            MachineModel machine = new()
            {
                Id = SelectedMachine.Id,
                MachineName = MachineName,
                ModelName = ModelNameMachine,
                EuropeanArticleNumber = EuropeanArticleNumber,
                DatePurchased = DatePurchasedMachine.Value,
                PurchasedPrice = PurchasedPrice
            };

            MachineName = "";
            ModelNameMachine = "";
            EuropeanArticleNumber = "";
            DatePurchasedMachine = DateTime.MinValue;
            PurchasedPriceMachine = "";

            await _stockEndpoint.UpdateMachine(machine);
            await LoadAllMachines();
        }
        else
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Title = "System Error";

            _status.UpdateMessage("Fatal Exception", "The Purchased Price wasn't a convertable number.");
            await _window.ShowDialogAsync(_status, null, settings);
        }
    }

    public bool CanUpdatePart
    {
        get
        {
            if (SelectedPart is not null &&
                string.IsNullOrWhiteSpace(SelectedMachineName) == false && 
                string.IsNullOrWhiteSpace(PartName) == false &&
                string.IsNullOrWhiteSpace(ModelNamePart) == false &&
                string.IsNullOrWhiteSpace(PurchasedPricePart) == false &&
                DatePurchasedPart >= SqlDateTime.MinValue.Value)
            {
                return true;
            }

            return false;
        }
    }

    public string UpdatePartButtonColor
    {
        get
        {
            if (CanUpdatePart is true)
            {
                return "#121212";
            }

            else
            {
                return "Red";
            }
        }
    }

    public async Task UpdatePart()
    {
        bool isConvertedToDecimal = decimal.TryParse(PurchasedPricePart, out var PurchasedPrice);

        if(isConvertedToDecimal is true)
        {
            PartModel part = new()
            {
                Id = SelectedPart.Id,
                PartName = PartName,
                ModelName = ModelNamePart,
                PurchasedPrice = PurchasedPrice,
                MachineId = Machines.Where(x => x.MachineName == SelectedMachineName).FirstOrDefault().Id,
                DatePurchased = DatePurchasedPart.Value
            };

            MachineName = "";
            ModelNameMachine = "";
            EuropeanArticleNumber = "";
            DatePurchasedMachine = DateTime.MinValue;
            PurchasedPriceMachine = "";

            await _stockEndpoint.UpdatePart(part);
            await LoadAllPart();
        }
        else
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Title = "System Error";

            _status.UpdateMessage("Fatal Exception", "The Purchased Price wasn't a convertable number.");
            await _window.ShowDialogAsync(_status, null, settings);
        }

    }


}
