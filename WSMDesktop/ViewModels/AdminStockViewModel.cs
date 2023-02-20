using AutoMapper;
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
    private readonly IItemEndpoint _itemEndpoint;
    private readonly IMapper _mapper;
    private readonly IWindowManager _window;
    private readonly StatusViewModel _status;
    
    public AdminStockViewModel(IItemEndpoint itemEndpoint,
                               IMapper mapper,
                               IWindowManager window,
                               StatusViewModel status)
	{
        _itemEndpoint = itemEndpoint;
        _mapper = mapper;
        _window = window;
        _status = status;
    }

    protected override async void OnViewLoaded(object view)
    {
        base.OnViewLoaded(view);
        try
        {
            await LoadAllItems();
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

    public async Task LoadAllItems()
    {
        var itemList = await _itemEndpoint.GetAllAdminAsync();
        var items = _mapper.Map<List<ItemDisplayModel>>(itemList)
            .Where(x => x.Archived == false)
            .ToList();

        Items = new BindingList<ItemDisplayModel>(items);
    }

    private BindingList<ItemDisplayModel> _items;

    public BindingList<ItemDisplayModel> Items
    {
        get { return _items; }
        set 
        { 
            _items = value; 
            NotifyOfPropertyChange(() => Items);
        }
    }

    private ItemDisplayModel _selectedItem;

    public ItemDisplayModel SelectedItem
    {
        get { return _selectedItem; }
        set 
        { 
            _selectedItem = value;
            ModelName = value.ModelName;
            Description = value.Description;
            Quantity = value.Quantity;
            Price = value.Price;
            EAN = value.EAN;
            NotifyOfPropertyChange(() => SelectedItem);
            NotifyOfPropertyChange(() => SelectedItemButtonColor);
            NotifyOfPropertyChange(() => CanSubmit);
            NotifyOfPropertyChange(() => SubmitButtonColor);
        }
    }

    private string _searchItemText;

    public string SearchItemText
    {
        get { return _searchItemText; }
        set 
        { 
            _searchItemText = value;
            NotifyOfPropertyChange(() => SearchItemText);
        }
    }

    public async Task SearchItem()
    {
        if (string.IsNullOrWhiteSpace(SearchItemText))
        {
            await LoadAllItems();
        }
        else
        {
            var itemList = Items.Where(x => x.ModelName.Contains(SearchItemText)).ToList();
            Items = new BindingList<ItemDisplayModel>(itemList);
        }
    }

    private string _modelName;

    public string ModelName
    {
        get { return _modelName; }
        set
        {
            _modelName = value;
            NotifyOfPropertyChange(() => ModelName);
            NotifyOfPropertyChange(() => ModelNameButtonColor);
            NotifyOfPropertyChange(() => CanSubmit);
            NotifyOfPropertyChange(() => SubmitButtonColor);
        }
    }

    private string _description;

    public string Description
    {
        get { return _description; }
        set 
        { 
            _description = value; 
            NotifyOfPropertyChange(() => Description);
            NotifyOfPropertyChange(() => DescriptionButtonColor);
            NotifyOfPropertyChange(() => CanSubmit);
            NotifyOfPropertyChange(() => SubmitButtonColor);
        }
    }

    private int _quantity;

    public int Quantity
    {
        get { return _quantity; }
        set 
        { 
            _quantity = value; 
            NotifyOfPropertyChange(() => Quantity);
            NotifyOfPropertyChange(() => QuantityButtonColor);
            NotifyOfPropertyChange(() => CanSubmit);
            NotifyOfPropertyChange(() => SubmitButtonColor);
        }
    }

    private decimal _price;

    public decimal Price
    {
        get { return _price; }
        set 
        { 
            _price = value; 
            NotifyOfPropertyChange(() => Price);
            NotifyOfPropertyChange(() => PriceButtonColor);
            NotifyOfPropertyChange(() => CanSubmit);
            NotifyOfPropertyChange(() => SubmitButtonColor);
        }
    }

    private decimal? _ean;

    public decimal? EAN
    {
        get { return _ean; }
        set 
        { 
            _ean = value; 
            NotifyOfPropertyChange(() => EAN);
            NotifyOfPropertyChange(() => EANButtonColor);
            NotifyOfPropertyChange(() => CanSubmit);
            NotifyOfPropertyChange(() => SubmitButtonColor);
        }
    }

    public string SelectedItemButtonColor
    {
        get
        {
            if (SelectedItem is not null)
            {
                return "Green";
            }

            return "Red";
        }
    }

    public string ModelNameButtonColor
    {
        get
        {
            if (string.IsNullOrWhiteSpace(ModelName) == false)
            {
                return "Green";
            }

            return "Red";
        }
    }

    public string DescriptionButtonColor
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Description) == false)
            {
                return "Green";
            }

            return "Red";
        }
    }

    public string QuantityButtonColor
    {
        get
        {
            if (Quantity >= 0)
            {
                return "Green";
            }

            return "Red";
        }
    }

    public string PriceButtonColor
    {
        get
        {
            if (Price >= 0)
            {
                return "Green";
            }

            return "Red";
        }
    }

    public string EANButtonColor
    {
        get
        {
            if (EAN >= 100_000_000_000 && EAN <= 999_999_999_999)
            {
                return "Green";
            }

            return "Red";
        }
    }

    public bool CanSubmit
    {
        get
        {
            if (string.IsNullOrWhiteSpace(ModelName) == false &&
                string.IsNullOrWhiteSpace(Description) == false &&
                Quantity >= 0 && EAN >= 0)
            {
                return true;
            }

            return false;
        }
    }

    public string SubmitButtonColor
    {
        get
        {
            if (CanSubmit)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task Submit()
    {
        if (SelectedItem is null)
        {
            ItemModel i = new()
            {
                ModelName = ModelName,
                Description = Description,
                Quantity = Quantity,
                Price = Price,
                EAN = EAN,
                Archived = false
            };

            var mappedItem = _mapper.Map<ItemDisplayModel>(i);

            await _itemEndpoint.InsertItemAsync(i);
            Items.Add(mappedItem);
            ModelName = "";
            Description = "";
            Quantity = 0;
            Price = 0;
            EAN = 0;
        }
        else
        {
            ItemModel i = new()
            {
                Id = SelectedItem.Id,
                ModelName = ModelName,
                Description = Description,
                Quantity = Quantity,
                Price = Price,
                EAN = EAN
            };

            await _itemEndpoint.UpdateItemAsync(i);
            ModelName = "";
            Description = "";
            Quantity = 0;
            Price = 0;
            EAN = 0;
        }
    }
}
