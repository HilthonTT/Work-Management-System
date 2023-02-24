using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    private readonly ICompanyEndpoint _companyEndpoint;
    private readonly IUserEndpoint _userEndpoint;
    private readonly IMapper _mapper;
    private readonly IWindowManager _window;
    private readonly StatusViewModel _status;
    private bool _FilteredByArchived = false;
    
    public AdminStockViewModel(IItemEndpoint itemEndpoint,
                               ICompanyEndpoint companyEndpoint,
                               IUserEndpoint userEndpoint,
                               IMapper mapper,
                               IWindowManager window,
                               StatusViewModel status)
	{
        _itemEndpoint = itemEndpoint;
        _companyEndpoint = companyEndpoint;
        _userEndpoint = userEndpoint;
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
            await LoadAllUsers();
            await LoadAllCompanies();
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

    private async Task LoadAllItems()
    {
        var itemList = await _itemEndpoint.GetAllAdminAsync();
        var items = _mapper.Map<List<ItemDisplayModel>>(itemList);

        Items = new BindingList<ItemDisplayModel>(items);
    }

    private async Task LoadAllUsers()
    {
        var userList = await _userEndpoint.GetAllAsync();
        Users = new BindingList<UserModel>(userList);
    }

    private async Task LoadAllCompanies()
    {
        var companyList = await _companyEndpoint.GetAllAsync();
        var companies = _mapper.Map<List<CompanyDisplayModel>>(companyList)
            .Where(x => x.Archived == false)
            .ToList();

        Companies = new BindingList<CompanyDisplayModel>(companies);
    }

    public string FilterButtonColor
    {
        get
        {
            if (_FilteredByArchived == false)
            {
                return "Red";
            }

            return "#121212";
        }
    }

    public async Task FilterByArchived()
    {
        if (_FilteredByArchived == false)
        {
            var itemList = await _itemEndpoint.GetAllAdminAsync();
            var items = _mapper.Map<List<ItemDisplayModel>>(itemList)
                .Where(x => x.Archived)
                .ToList();
            Items = new BindingList<ItemDisplayModel>(items);
            _FilteredByArchived = true;
        }
        else
        {
            await LoadAllItems();
            _FilteredByArchived = false;
        }

        NotifyOfPropertyChange(() => FilterButtonColor);
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
            
            if (value is null)
            {
                ModelName = "";
                Description = "";
                SelectedUserName = "";
                SelectedCompanyName = "";
                Location = "";
                Quantity = 0;
                Price = 0;
                EAN = 0;
            }
            else
            {
                ModelName = value.ModelName;
                Description = value.Description;
                SelectedUserName = Users.Where(x => x.Id == value.InternalSupplierPersonId).FirstOrDefault()?.FullName != null ? Users.Where(x => x.Id == value.InternalSupplierPersonId).FirstOrDefault()?.FullName : "No Internal Supplier [Person]";
                SelectedCompanyName = Companies.Where(x => x.Id == value.InternalSupplierCompanyId).FirstOrDefault()?.CompanyName != null ? Companies.Where(x => x.Id == value.InternalSupplierCompanyId).FirstOrDefault()?.CompanyName : "No Internal Supplier [Company]";
                Quantity = value.Quantity;
                Location = value.Location;
                Price = value.Price;
                EAN = value.EAN;
            }
            NotifyOfPropertyChange(() => SelectedItem);
            NotifyOfPropertyChange(() => SelectedItemButtonColor);
            NotifyOfPropertyChange(() => CanSubmit);
            NotifyOfPropertyChange(() => SubmitButtonColor);
            NotifyOfPropertyChange(() => CanArchive);
            NotifyOfPropertyChange(() => ArchivedButtonColor);
        }
    }

    private BindingList<UserModel> _users;

    public BindingList<UserModel> Users
    {
        get { return _users; }
        set 
        { 
            _users = value;
            NotifyOfPropertyChange(() => Users);
        }
    }

    private UserModel _selectedUser;

    public UserModel SelectedUser
    {
        get { return _selectedUser; }
        set 
        {
            _selectedUser = value;
            SelectedUserName = value?.FullName;
            NotifyOfPropertyChange(() => SelectedUser);
            NotifyOfPropertyChange(() => CanSubmit);
            NotifyOfPropertyChange(() => SubmitButtonColor);
            NotifyOfPropertyChange(() => SelectedUserButtonColor);
        }
    }

    private BindingList<CompanyDisplayModel> _companies;

    public BindingList<CompanyDisplayModel> Companies
    {
        get { return _companies; }
        set 
        { 
            _companies = value;
            NotifyOfPropertyChange(() => Companies);
        }
    }

    private CompanyDisplayModel _selectedCompany;

    public CompanyDisplayModel SelectedCompany
    {
        get { return _selectedCompany; }
        set 
        { 
            _selectedCompany = value;
            SelectedCompanyName = value?.CompanyName;
            NotifyOfPropertyChange(() => SelectedCompany);
            NotifyOfPropertyChange(() => CanSubmit);
            NotifyOfPropertyChange(() => SubmitButtonColor);
            NotifyOfPropertyChange(() => SelectedCompanyButtonColor);
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
            SearchItem();
        }
    }

    private async Task SearchItem()
    {
        var itemList = await _itemEndpoint.GetAllAsync();
        var output = _mapper.Map<List<ItemDisplayModel>>(itemList);

        if (_FilteredByArchived)
        {
            output = output.Where(x => x.Archived).ToList();
        }
        else
        {
            output = output.Where(x => x.Archived == false).ToList();
        }

        if (string.IsNullOrWhiteSpace(SearchItemText) == false)
        {
            output = output.Where(x => x.ModelName.Contains(SearchItemText, StringComparison.InvariantCultureIgnoreCase) ||
                x.Description.Contains(SearchItemText, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            Items = new BindingList<ItemDisplayModel>(output);
        }
        else
        {
            Items = new BindingList<ItemDisplayModel>(output);
        }
    }

    private string _companySearchText;

    public string CompanySearchText
    {
        get { return _companySearchText; }
        set 
        { 
            _companySearchText = value; 
            NotifyOfPropertyChange(() => CompanySearchText);
            SearchCompany();
        }
    }


    private async Task SearchCompany()
    {
        var companylist = await _companyEndpoint.GetAllAsync();
        var output = _mapper.Map<List<CompanyDisplayModel>>(companylist.Where(x => x.Archived == false).ToList());

        if (string.IsNullOrWhiteSpace(CompanySearchText) == false)
        {
            output = output.Where(x => x.CompanyName.Contains(CompanySearchText, StringComparison.InvariantCultureIgnoreCase) ||
                x.Description.Contains(CompanySearchText, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            Companies = new BindingList<CompanyDisplayModel>(output);
        }
        else
        {
            Companies = new BindingList<CompanyDisplayModel>(output);
        }
    }

    private string _userSearchText;

    public string UserSearchText
    {
        get { return _userSearchText; }
        set 
        {
            _userSearchText = value;
            NotifyOfPropertyChange(() => UserSearchText);
            SearchUser();
        }
    }

    private async Task SearchUser()
    {
        var output = await _userEndpoint.GetAllAsync();

        if (string.IsNullOrWhiteSpace(UserSearchText) == false)
        {
            output = output.Where(x => x.FirstName.Contains(UserSearchText, StringComparison.InvariantCultureIgnoreCase) ||
                    x.LastName.Contains(UserSearchText, StringComparison.InvariantCultureIgnoreCase) ||
                    x.EmailAddress.Contains(UserSearchText, StringComparison.InvariantCultureIgnoreCase) ||
                    x.PhoneNumber.Contains(UserSearchText, StringComparison.InvariantCultureIgnoreCase)).ToList();

            Users = new BindingList<UserModel>(output);
        }
        else
        {
            Users = new BindingList<UserModel>(output);
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

    private string _location;

    public string Location
    {
        get { return _location; }
        set 
        {
            _location = value; 
            NotifyOfPropertyChange(() => Location);
            NotifyOfPropertyChange(() => LocationButtonColor);
            NotifyOfPropertyChange(() => CanSubmit);
            NotifyOfPropertyChange(() => SubmitButtonColor);
        }
    }

    private decimal? _ean = 0;

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

    private string _selectedUserName;

    public string SelectedUserName
    {
        get { return _selectedUserName; }
        set 
        { 
            _selectedUserName = value; 
            NotifyOfPropertyChange(() => SelectedUserName);
        }
    }

    private string _selectedCompanyName;

    public string SelectedCompanyName
    {
        get { return _selectedCompanyName; }
        set 
        { 
            _selectedCompanyName = value; 
            NotifyOfPropertyChange(() => SelectedCompanyName);
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

    public string LocationButtonColor
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Location) == false)
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

    public string SelectedUserButtonColor
    {
        get
        {
            if (SelectedUser is not null)
            {
                return "Green";
            }

            return "Red";
        }
    }

    public string SelectedCompanyButtonColor
    {
        get
        {
            if (SelectedCompany is not null)
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
                string.IsNullOrWhiteSpace(Location) == false &&
                Quantity >= 0 && EAN >= 0)
            {
                if (SelectedCompany is null && SelectedUser is null)
                {
                    return false;
                }

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
                InternalSupplierPersonId = SelectedUser?.Id,
                InternalSupplierCompanyId = SelectedCompany?.Id,
                Location = Location,
                EAN = EAN,
                Archived = false
            };

            var mappedItem = _mapper.Map<ItemDisplayModel>(i);

            await _itemEndpoint.InsertItemAsync(i);
            Items.Add(mappedItem);
            ModelName = "";
            Description = "";
            Quantity = 0;
            Location = "";
            SelectedUser = null;
            SelectedCompany = null;
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
                Location = Location,
                InternalSupplierPersonId = SelectedUser?.Id,
                InternalSupplierCompanyId = SelectedCompany?.Id,
                Price = Price,
                EAN = EAN
            };

            await _itemEndpoint.UpdateItemAsync(i);
            ModelName = "";
            Description = "";
            Quantity = 0;
            Location = "";
            SelectedUser = null;
            SelectedCompany = null;
            Price = 0;
            EAN = 0;

            SelectedItem.ModelName = i.ModelName;
            SelectedItem.Description = i.Description;
            SelectedItem.Quantity = i.Quantity;
            SelectedItem.Location = i.Location;
            SelectedItem.InternalSupplierPersonId = i.InternalSupplierPersonId;
            SelectedItem.InternalSupplierCompanyId = i.InternalSupplierCompanyId;
            SelectedItem.EAN = i.EAN;
            SelectedItem.Price = i.Price;
        }
        
        NotifyOfPropertyChange(() => Items);
    }


    public bool CanArchive
    {
        get
        {
            if (SelectedItem is not null)
            {
                return true;
            }

            return false;
        }
    }

    public string ArchivedButtonColor 
    {
        get
        {
            if (CanArchive)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task Archive()
    {
        var mappedItem = _mapper.Map<ItemModel>(SelectedItem);
        mappedItem.Archived = !mappedItem.Archived;
        SelectedItem.Archived = mappedItem.Archived;

        SelectedItem.Archived = mappedItem.Archived;

        
        await _itemEndpoint.ArchiveItemAsync(mappedItem);
        NotifyOfPropertyChange(() => Items);
    }
}
