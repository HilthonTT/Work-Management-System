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
    private readonly IItemEndpoint _itemEndpoint;
    private readonly IWindowManager _window;
    private readonly IMapper _mapper;
    private readonly StatusViewModel _status;

    public StockViewModel(IItemEndpoint itemEndpoint,
                          IWindowManager window,
                          IMapper mapper,
                          StatusViewModel status)
	{
        _itemEndpoint = itemEndpoint;
        _window = window;
        _mapper = mapper;
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

    public async Task LoadAllItems()
    {
        var itemList = await _itemEndpoint.GetAllAsync();
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
            ModelName = value?.ModelName;
            Description = value?.Description;
            Quantity = value?.Quantity;
            Price = value?.Price;
            EAN = value?.EAN;
            NotifyOfPropertyChange(() => SelectedItem);
            NotifyOfPropertyChange(() => SelectedItemButtonColor);
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
        }
    }

    private int? _quantity;

    public int? Quantity
    {
        get { return _quantity; }
        set
        {
            _quantity = value;
            NotifyOfPropertyChange(() => Quantity);
            NotifyOfPropertyChange(() => QuantityButtonColor);
        }
    }

    private decimal? _price;

    public decimal? Price
    {
        get { return _price; }
        set
        {
            _price = value;
            NotifyOfPropertyChange(() => Price);
            NotifyOfPropertyChange(() => PriceButtonColor);
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
            if (EAN >= 100_000_000_000)
            {
                return "Green";
            }

            return "Red";
        }
    }
}
