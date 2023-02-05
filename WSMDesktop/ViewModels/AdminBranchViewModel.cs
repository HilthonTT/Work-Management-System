using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI.Library.API;
using UI.Library.Models;
using WSMDesktop.Models;

namespace WSMDesktop.ViewModels;

public class AdminBranchViewModel : Screen
{
    private readonly ICompanyEndpoint _companyEndpoint;
    private readonly IDepartmentEndpoint _departmentEndpoint;
    private readonly IUserEndpoint _userEndpoint;
    private readonly StatusViewModel _status;
    private readonly IMapper _mapper;
    private readonly IWindowManager _window;

    public AdminBranchViewModel(ICompanyEndpoint companyEndpoint,
                                 IDepartmentEndpoint departmentEndpoint,
                                 IUserEndpoint userEndpoint,
                                 StatusViewModel status,
                                 IMapper mapper,
                                 IWindowManager window)
	{
        _companyEndpoint = companyEndpoint;
        _departmentEndpoint = departmentEndpoint;
        _userEndpoint = userEndpoint;
        _status = status;
        _mapper = mapper;
        _window = window;
    }

    protected override async void OnViewLoaded(object view)
    {
        base.OnViewLoaded(view);
        try
        {
            await LoadAllCompanies();
            await LoadAllDepartments();
            await LoadAllUsers();
        }
        catch (Exception ex)
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Title = "System Error";

            if (ex.Message == "Unauthorized")
            {
                _status.UpdateMessage("Unauthorized Access", "You do not have permission to interact with Users Page");
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
    
    private async Task LoadAllCompanies()
    {
        var companyList = await _companyEndpoint.GetAll();
        var companies = _mapper.Map<List<CompanyDisplayModel>>(companyList);
        Companies = new BindingList<CompanyDisplayModel>(companies);
    }

    private async Task LoadAllDepartments()
    {
        var departmentList = await _departmentEndpoint.GetAll();
        var departments = _mapper.Map<List<DepartmentDisplayModel>>(departmentList);
        Departments = new BindingList<DepartmentDisplayModel>(departments);
    }

    private async Task LoadAllUsers()
    {
        var userList = await _userEndpoint.GetAll();
        Users = new BindingList<UserModel>(userList);
    }

    private string _searchCompanyText;

    public string SearchCompanyText
    {
        get { return _searchCompanyText; }
        set 
        {
            _searchCompanyText = value; 
            NotifyOfPropertyChange(() => SearchCompanyText);
        }
    }

    public static string SearchCompanyButtonColor
    {
        get
        {
            return "#121212";
        }
    }

    public async Task SearchCompany()
    {
        if (string.IsNullOrWhiteSpace(SearchCompanyText))
        {
            await LoadAllCompanies();
        }
        else 
        {
            var companyList = Companies.Where(x => x.CompanyName.Contains(SearchCompanyText)).ToList();
            Companies = new BindingList<CompanyDisplayModel>(companyList);
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
            CompanyName = value?.CompanyName;
            AddressCompany = value?.Address;
            PhoneNumberCompany = value?.PhoneNumber;
            DescriptionCompany = value?.Description;
            DateFoundedCompany = value?.DateFounded;
            SelectedUser = Users.Where(x => x.Id == value?.ChairPersonId)?.FirstOrDefault();
            NotifyOfPropertyChange(() => SelectedCompany);
            NotifyOfPropertyChange(() => CanUpdateCompany);
            NotifyOfPropertyChange(() => UpdateCompanyButtonColor);
        }
    }

    public bool CanDeleteSelectedCompany
    {
        get
        {
            if (SelectedCompany is not null)
            {
                return true;
            }

            return false;
        }
    }

    public string DeleteCompanyButtonColor
    {
        get
        {
            if (CanDeleteSelectedCompany is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task DeleteSelectedCompany()
    {
        var mappedCompany = _mapper.Map<CompanyModel>(SelectedCompany);

        await _companyEndpoint.DeleteCompany(mappedCompany);
    }

    private string _searchDepartmentText;

    public string SearchDepartmentText
    {
        get { return _searchDepartmentText; }
        set
        {
            _searchDepartmentText = value;
            NotifyOfPropertyChange(() => SearchDepartmentText);
        }
    }

    public async Task SearchDepartment()
    {
        if (string.IsNullOrWhiteSpace(SearchDepartmentText))
        {
            await LoadAllDepartments();
        }
        else
        {
            var departmentList = Departments.Where(x => x.DepartmentName.Contains(SearchDepartmentText)).ToList();
            Departments = new BindingList<DepartmentDisplayModel>(departmentList);
        }
    }

    private BindingList<DepartmentDisplayModel> _departments;

    public BindingList<DepartmentDisplayModel> Departments
    {
        get { return _departments; }
        set 
        { 
            _departments = value; 
            NotifyOfPropertyChange(() => Departments);
        }
    }

    private DepartmentDisplayModel _selectedDepartment;

    public DepartmentDisplayModel SelectedDepartment
    {
        get { return _selectedDepartment; }
        set 
        {
            _selectedDepartment = value; 
            NotifyOfPropertyChange(() => SelectedDepartment);
        }
    }

    public bool CanDeleteSelectedDepartment
    {
        get
        {
            if (SelectedDepartment is not null)
            {
                return true;
            }

            return false;
        }
    }

    public string DeleteDepartmentButtonColor
    {
        get
        {
            if (CanDeleteSelectedDepartment is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task DeleteSelectedDepartment()
    {
        var mappedDepartment = _mapper.Map<DepartmentModel>(SelectedDepartment);

        await _departmentEndpoint.DeleteDepartment(mappedDepartment);
    }


    private string _CompanyName;

    public string CompanyName
    {
        get { return _CompanyName; }
        set 
        { 
            _CompanyName = value; 
            NotifyOfPropertyChange(() => CompanyName);
            NotifyOfPropertyChange(() => CanCreateCompany);
            NotifyOfPropertyChange(() => CreateCompanyButtonColor);
        }
    }

    private string _adressCompany;

    public string AddressCompany
    {
        get { return _adressCompany; }
        set 
        { 
            _adressCompany = value; 
            NotifyOfPropertyChange(() => AddressCompany);
            NotifyOfPropertyChange(() => CanCreateCompany);
            NotifyOfPropertyChange(() => CreateCompanyButtonColor);
        }
    }

    private string _phoneNumberCompany;

    public string PhoneNumberCompany
    {
        get { return _phoneNumberCompany; }
        set 
        { 
            _phoneNumberCompany = value; 
            NotifyOfPropertyChange(() => PhoneNumberCompany);
            NotifyOfPropertyChange(() => CanCreateCompany);
            NotifyOfPropertyChange(() => CreateCompanyButtonColor);
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
            NotifyOfPropertyChange(() => SelectedUser);
            NotifyOfPropertyChange(() => CanCreateCompany);
            NotifyOfPropertyChange(() => CreateCompanyButtonColor);
        }
    }

    private string _descriptionCompany;

    public string DescriptionCompany
    {
        get { return _descriptionCompany; }
        set 
        { 
            _descriptionCompany = value; 
            NotifyOfPropertyChange(() => DescriptionCompany);
            NotifyOfPropertyChange(() => CanCreateCompany);
            NotifyOfPropertyChange(() => CreateCompanyButtonColor);
        }
    }

    private DateTime? _dateFoundedCompany = SqlDateTime.MinValue.Value;

    public DateTime? DateFoundedCompany
    {
        get { return _dateFoundedCompany; }
        set 
        { 
            _dateFoundedCompany = value; 
            NotifyOfPropertyChange(() => DateFoundedCompany);
            NotifyOfPropertyChange(() => CanCreateCompany);
            NotifyOfPropertyChange(() => CreateCompanyButtonColor);
        }
    }

    private string _departmentName;

    public string DepartmentName
    {
        get { return _departmentName; }
        set 
        { 
            _departmentName = value; 
            NotifyOfPropertyChange(() => DepartmentName);
        }
    }

    private string _addressDepartment;

    public string AddressDepartment
    {
        get { return _addressDepartment; }
        set 
        { 
            _addressDepartment = value; 
            NotifyOfPropertyChange(() => AddressDepartment);
        }
    }

    private string _phoneNumberDepartment;

    public string PhoneNumberDepartment
    {
        get { return _phoneNumberDepartment; }
        set 
        { 
            _phoneNumberDepartment = value; 
            NotifyOfPropertyChange(() => PhoneNumberDepartment);
        }
    }

    private string _descriptionDepartment;

    public string DescriptionDepartment
    {
        get { return _descriptionDepartment; }
        set 
        { 
            _descriptionDepartment = value; 
            NotifyOfPropertyChange(() => DescriptionDepartment);
        }
    }

    private DateTime _dateFoundedDepartment = SqlDateTime.MinValue.Value;

    public DateTime DateFoundedDepartment
    {
        get { return _dateFoundedDepartment; }
        set 
        { 
            _dateFoundedDepartment = value; 
            NotifyOfPropertyChange(() => DateFoundedDepartment);
        }
    }

    public bool CanCreateCompany
    {
        get
        {
            if (string.IsNullOrWhiteSpace(CompanyName) == false &&
                string.IsNullOrWhiteSpace(AddressCompany) == false  &&
                string.IsNullOrWhiteSpace(PhoneNumberCompany) == false &&
                string.IsNullOrWhiteSpace(DescriptionCompany) == false &&
                SelectedUser is not null &&
                DateFoundedCompany >= SqlDateTime.MinValue.Value)
            {
                return true;
            }

            return false;
        }
    }

    public string CreateCompanyButtonColor
    {
        get
        {
            if (CanCreateCompany is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }


    public async Task CreateCompany()
    {
        CompanyModel company = new()
        {
            CompanyName = CompanyName,
            Address = AddressCompany,
            PhoneNumber = PhoneNumberCompany,
            ChairPersonId = SelectedUser.Id,
            Description = DescriptionCompany,
            DateFounded = DateFoundedCompany.Value,
        };
        
        var mappedCompany = _mapper.Map<CompanyDisplayModel>(company);

        CompanyName = "";
        AddressCompany = "";
        PhoneNumberCompany = "";
        SelectedUser = null;
        DescriptionCompany = "";
        DateFoundedCompany = DateTime.MinValue;

        Companies.Add(mappedCompany);
        await _companyEndpoint.PostCompany(company);
    }


    public bool CanUpdateCompany
    {
        get
        {
            if (SelectedCompany is not null && SelectedUser is not null)
            {
                return true;
            }

            return false;
        }
    }

    public string UpdateCompanyButtonColor
    {
        get
        {
            if (CanUpdateCompany is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task UpdateCompany()
    {
        CompanyModel company = new()
        {
            Id = SelectedCompany.Id,
            CompanyName = CompanyName,
            Address = AddressCompany,
            PhoneNumber = PhoneNumberCompany,
            ChairPersonId = SelectedUser.Id,
            Description = DescriptionCompany,
            DateFounded = DateFoundedCompany.Value,
        };

        CompanyName = "";
        AddressCompany = "";
        PhoneNumberCompany = "";
        SelectedUser = null;
        SelectedCompany = null;
        DescriptionCompany = "";
        DateFoundedCompany = DateTime.MinValue;

        await _companyEndpoint.UpdateCompany(company);
        await LoadAllCompanies();
    }
}
