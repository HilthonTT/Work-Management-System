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

public class AdminBranchViewModel : Screen
{
    private readonly ICompanyEndpoint _companyEndpoint;
    private readonly IDepartmentEndpoint _departmentEndpoint;
    private readonly IUserEndpoint _userEndpoint;
    private readonly StatusViewModel _status;
    private readonly IMapper _mapper;
    private readonly IWindowManager _window;
    private bool _filterByArchivedCompany = false;
    private bool _filterByArchivedDepartment = false;

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
        var companyList = await _companyEndpoint.GetAllAsync();
        var companies = _mapper.Map<List<CompanyDisplayModel>>(companyList).Where(x => x.Archived == false).ToList();
        Companies = new BindingList<CompanyDisplayModel>(companies);
    }

    private async Task LoadAllDepartments()
    {
        var departmentList = await _departmentEndpoint.GetAllAsync();
        var departments = _mapper.Map<List<DepartmentDisplayModel>>(departmentList).Where(x => x.Archived == false).ToList();
        Departments = new BindingList<DepartmentDisplayModel>(departments);
    }

    private async Task LoadAllUsers()
    {
        var userList = await _userEndpoint.GetAllAsync();
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
            SearchCompany();
        }
    }

    public static string ArchivedDepartmentButtonColor
    {
        get
        {
            return "#121212";
        }
    }

    public async Task SearchCompany()
    {
        var companyList = await _companyEndpoint.GetAllAsync();
        var output = _mapper.Map<List<CompanyDisplayModel>>(companyList);

        if (_filterByArchivedCompany)
        {
            output = output.Where(x => x.Archived).ToList();
        }
        else
        {
            output = output.Where(x => x.Archived == false).ToList();
        }

        if (string.IsNullOrWhiteSpace(SearchCompanyText) == false)
        {
            output = output.Where(x => x.CompanyName.Contains(SearchCompanyText, StringComparison.InvariantCultureIgnoreCase) ||
                x.Description.Contains(SearchCompanyText, StringComparison.InvariantCultureIgnoreCase) || 
                x.Address.Contains(SearchCompanyText, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            Companies = new BindingList<CompanyDisplayModel>(output);
        }
        else
        {
            Companies = new BindingList<CompanyDisplayModel>(output);
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
            NotifyOfPropertyChange(() => SelectedCompanyText);
            NotifyOfPropertyChange(() => CanArchiveSelectedCompany);
            NotifyOfPropertyChange(() => ArchiveCompanyButtonColor);
            NotifyOfPropertyChange(() => SelectedCompanyButtonColor);
        }
    }

    public bool CanArchiveSelectedCompany
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

    public string ArchiveCompanyButtonColor
    {
        get
        {
            if (CanArchiveSelectedCompany is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task ArchiveSelectedCompany()
    {
        var mappedCompany = _mapper.Map<CompanyModel>(SelectedCompany);
        mappedCompany.Archived = true;

        Companies.Remove(SelectedCompany);
        await _companyEndpoint.ArchiveCompanyAsync(mappedCompany);
    }

    private string _searchDepartmentText;

    public string SearchDepartmentText
    {
        get { return _searchDepartmentText; }
        set
        {
            _searchDepartmentText = value;
            NotifyOfPropertyChange(() => SearchDepartmentText);
            SearchDepartment();
        }
    }

    public static string SearchDepartmentButtonColor
    {
        get
        {
            return "#121212";
        }
    }

    public async Task SearchDepartment()
    {
        var departmentList = await _departmentEndpoint.GetAllAsync();
        var output = _mapper.Map<List<DepartmentDisplayModel>>(departmentList);

        if (_filterByArchivedDepartment)
        {
            output = output.Where(x => x.Archived).ToList();
        }
        else
        {
            output = output.Where(x => x.Archived == false).ToList();
        }

        if (string.IsNullOrWhiteSpace(SearchDepartmentText) == false)
        {
            output = output.Where(x => x.DepartmentName.Contains(SearchDepartmentText, StringComparison.InvariantCultureIgnoreCase) ||
                x.Description.Contains(SearchDepartmentText, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            Departments = new BindingList<DepartmentDisplayModel>(output);
        }
        else
        {
            Departments = new BindingList<DepartmentDisplayModel>(output);
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

    public string SelectedDepartmentButtonColor
    {
        get
        {
            if (SelectedDepartment is not null)
            {
                return "Green";
            }

            return "Red";
        }
    }

    private DepartmentDisplayModel _selectedDepartment;

    public DepartmentDisplayModel SelectedDepartment
    {
        get { return _selectedDepartment; }
        set 
        {
            _selectedDepartment = value;
            DepartmentName = value?.DepartmentName;
            AddressDepartment = value?.Address;
            SelectedUser = Users.Where(x => x.Id == value?.ChairPersonId)?.FirstOrDefault();
            SelectedCompany = Companies.Where(x => x.Id == value?.CompanyId)?.FirstOrDefault();
            PhoneNumberDepartment = value?.PhoneNumber;
            DescriptionDepartment = value?.Description;
            DateFoundedDepartment = value?.CreatedDate;
            NotifyOfPropertyChange(() => SelectedDepartment);
            NotifyOfPropertyChange(() => SelectedDepartmentText);
            NotifyOfPropertyChange(() => CanArchiveSelectedDepartment);
            NotifyOfPropertyChange(() => ArchiveDepartmentButtonColor);
            NotifyOfPropertyChange(() => SelectedDepartmentButtonColor);
        }
    }

    public bool CanArchiveSelectedDepartment
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

    public string ArchiveDepartmentButtonColor
    {
        get
        {
            if (CanArchiveSelectedDepartment is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task ArchiveSelectedDepartment()
    {
        var mappedDepartment = _mapper.Map<DepartmentModel>(SelectedDepartment);
        mappedDepartment.Archived = true;

        Departments.Remove(SelectedDepartment);
        await _departmentEndpoint.ArchiveDepartmentAsync(mappedDepartment);
    }

    public string CompanyNameButtonColor
    {
        get
        {
            if (string.IsNullOrWhiteSpace(CompanyName) == false)
            {
                return "Green";
            }

            return "Red";
        }
    }

    private string _companyName;

    public string CompanyName
    {
        get { return _companyName; }
        set 
        { 
            _companyName = value; 
            NotifyOfPropertyChange(() => CompanyName);
            NotifyOfPropertyChange(() => CanCreateCompany);
            NotifyOfPropertyChange(() => CreateCompanyButtonColor);
            NotifyOfPropertyChange(() => CompanyNameButtonColor);
        }
    }

    public string AddressCompanyButtonColor
    {
        get
        {
            if (string.IsNullOrWhiteSpace(AddressCompany) == false)
            {
                return "Green";
            }

            return "Red";
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
            NotifyOfPropertyChange(() => AddressCompanyButtonColor);
        }
    }

    public string PhoneNumberCompanyButtonColor
    {
        get
        {
            if (string.IsNullOrWhiteSpace(PhoneNumberCompany) == false)
            {
                return "Green";
            }

            return "Red";
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
            NotifyOfPropertyChange(() => PhoneNumberCompanyButtonColor);
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
            NotifyOfPropertyChange(() => CanCreateDepartment);
            NotifyOfPropertyChange(() => CreateDepartmentButtonColor);
            NotifyOfPropertyChange(() => CanUpdateDepartment);
            NotifyOfPropertyChange(() => UpdateDepartmentButtonColor);
            NotifyOfPropertyChange(() => SelectedUserButtonColor);
            NotifyOfPropertyChange(() => CanUpdateCompany);
            NotifyOfPropertyChange(() => UpdateCompanyButtonColor);
        }
    }

    public string SelectedCompanyText
    {
        get
        {
            if (SelectedCompany is not null)
            {
                return $"{SelectedCompany.CompanyName}";
            }

            return "No Company Selected";
        }
    }

    public string SelectedDepartmentText
    {
        get
        {
            if (SelectedDepartment is not null)
            {
                return $"{SelectedDepartment.DepartmentName}";
            }

            return "No Department Selected";
        }
    }

    public string DescriptionCompanyButtonButton
    {
        get
        {
            if (string.IsNullOrWhiteSpace(DescriptionCompany) == false)
            {
                return "Green";
            }

            return "Red";
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
            NotifyOfPropertyChange(() => DescriptionCompanyButtonButton);
        }
    }

    public string DateFoundedCompanyButtonColor
    {
        get
        {
            if (DateFoundedCompany >= SqlDateTime.MinValue.Value)
            {
                return "Green";
            }

            return "Red";
        }
    }

    private DateTime? _dateFoundedCompany = DateTime.UtcNow;

    public DateTime? DateFoundedCompany
    {
        get { return _dateFoundedCompany; }
        set 
        { 
            _dateFoundedCompany = value; 
            NotifyOfPropertyChange(() => DateFoundedCompany);
            NotifyOfPropertyChange(() => CanCreateCompany);
            NotifyOfPropertyChange(() => CreateCompanyButtonColor);
            NotifyOfPropertyChange(() => DateFoundedCompanyButtonColor);
        }
    }

    public string DepartmentNameButtonColor
    {
        get
        {
            if (string.IsNullOrWhiteSpace(DepartmentName) == false)
            {
                return "Green";
            }

            return "Red";
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
            NotifyOfPropertyChange(() => CanCreateDepartment);
            NotifyOfPropertyChange(() => CreateDepartmentButtonColor);
            NotifyOfPropertyChange(() => CanUpdateDepartment);
            NotifyOfPropertyChange(() => UpdateDepartmentButtonColor);
            NotifyOfPropertyChange(() => DepartmentNameButtonColor);
        }
    }

    public string AddressDepartmentButtonColor
    {
        get
        {
            if (string.IsNullOrWhiteSpace(AddressDepartment) == false)
            {
                return "Green";
            }

            return "Red";
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
            NotifyOfPropertyChange(() => CanCreateDepartment);
            NotifyOfPropertyChange(() => CreateDepartmentButtonColor);
            NotifyOfPropertyChange(() => CanUpdateDepartment);
            NotifyOfPropertyChange(() => UpdateDepartmentButtonColor);
            NotifyOfPropertyChange(() => AddressDepartmentButtonColor);
        }
    }

    public string PhoneNumberDepartmentButtonColor
    {
        get
        {
            if (string.IsNullOrWhiteSpace(PhoneNumberDepartment) == false)
            {
                return "Green";
            }

            return "Red";
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
            NotifyOfPropertyChange(() => CanCreateDepartment);
            NotifyOfPropertyChange(() => CreateDepartmentButtonColor);
            NotifyOfPropertyChange(() => CanUpdateDepartment);
            NotifyOfPropertyChange(() => UpdateDepartmentButtonColor);
            NotifyOfPropertyChange(() => PhoneNumberDepartmentButtonColor);
        }
    }

    public string DescriptionDepartmentButtonColor
    {
        get
        {
            if (string.IsNullOrWhiteSpace(DescriptionDepartment) == false)
            {
                return "Green";
            }

            return "Red";
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
            NotifyOfPropertyChange(() => CanCreateDepartment);
            NotifyOfPropertyChange(() => CreateDepartmentButtonColor);
            NotifyOfPropertyChange(() => CanUpdateDepartment);
            NotifyOfPropertyChange(() => UpdateDepartmentButtonColor);
            NotifyOfPropertyChange(() => DescriptionDepartmentButtonColor);
        }
    }

    public string DateFoundedDepartmentButtonColor
    {
        get
        {
            if (DateFoundedDepartment >= SqlDateTime.MinValue.Value)
            {
                return "Green";
            }

            return "Red";
        }
    }

    private DateTime? _dateFoundedDepartment = DateTime.UtcNow;

    public DateTime? DateFoundedDepartment
    {
        get { return _dateFoundedDepartment; }
        set 
        { 
            _dateFoundedDepartment = value; 
            NotifyOfPropertyChange(() => DateFoundedDepartment);
            NotifyOfPropertyChange(() => CanCreateDepartment);
            NotifyOfPropertyChange(() => CreateDepartmentButtonColor);
            NotifyOfPropertyChange(() => CanUpdateDepartment);
            NotifyOfPropertyChange(() => UpdateDepartmentButtonColor);
            NotifyOfPropertyChange(() => DateFoundedDepartmentButtonColor);
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
        DateFoundedCompany = DateTime.UtcNow;

        Companies.Add(mappedCompany);
        await _companyEndpoint.PostCompanyAsync(company);
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

        SelectedCompany.CompanyName = company.CompanyName;
        SelectedCompany.Address = company.PhoneNumber;
        SelectedCompany.ChairPersonId = company.ChairPersonId;

        CompanyName = "";
        AddressCompany = "";
        PhoneNumberCompany = "";
        SelectedUser = null;
        SelectedCompany = null;
        DescriptionCompany = "";
        DateFoundedCompany = DateTime.UtcNow;

        await _companyEndpoint.UpdateCompanyAsync(company);
        NotifyOfPropertyChange(() => Companies);
    }

    public bool CanCreateDepartment
    {
        get
        {
            if (string.IsNullOrWhiteSpace(DepartmentName) == false &&
                string.IsNullOrWhiteSpace(AddressDepartment) == false &&
                string.IsNullOrWhiteSpace(PhoneNumberDepartment) == false &&
                string.IsNullOrWhiteSpace(DescriptionDepartment) == false &&
                DateFoundedDepartment >= SqlDateTime.MinValue.Value &&
                SelectedUser is not null &&
                SelectedCompany is not null)
            {
                return true;
            }

            return false;
        }
    }

    public string CreateDepartmentButtonColor
    {
        get
        {
            if (CanCreateDepartment is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task CreateDepartment()
    {
        DepartmentModel department = new()
        {
            CompanyId = SelectedCompany.Id,
            DepartmentName = DepartmentName,
            Address = AddressDepartment,
            ChairPersonId = SelectedUser.Id,
            PhoneNumber = PhoneNumberDepartment,
            Description = DescriptionDepartment,
            CreatedDate = DateFoundedDepartment.Value
        };

        DepartmentName = "";
        AddressDepartment = "";
        SelectedUser = null;
        PhoneNumberDepartment = "";
        DescriptionDepartment = "";
        DateFoundedDepartment = DateTime.UtcNow;
        
        var mappedDepartment = _mapper.Map<DepartmentDisplayModel>(department);

        Departments.Add(mappedDepartment);
        await _departmentEndpoint.PostDepartmentAsync(department);
    }

    public bool CanUpdateDepartment
    {
        get
        {
            if (SelectedDepartment is not null && 
                SelectedUser is not null && 
                SelectedCompany is not null)
            {
                return true;
            }

            return false;
        }
    }

    public string UpdateDepartmentButtonColor
    {
        get
        {
            if (CanUpdateDepartment is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task UpdateDepartment()
    {
        DepartmentModel department = new()
        {
            Id = SelectedDepartment.Id,
            CompanyId = SelectedCompany.Id,
            DepartmentName = DepartmentName,
            Address = AddressDepartment,
            ChairPersonId = SelectedUser.Id,
            PhoneNumber = PhoneNumberDepartment,
            Description = DescriptionDepartment,
            CreatedDate = DateFoundedDepartment.Value
        };

        SelectedDepartment.CompanyId = department.CompanyId;
        SelectedDepartment.DepartmentName = department.DepartmentName;
        SelectedDepartment.Address = AddressDepartment;
        SelectedDepartment.ChairPersonId = SelectedUser.Id;
        SelectedDepartment.PhoneNumber = PhoneNumberDepartment;
        SelectedDepartment.Description = DescriptionDepartment;
        SelectedDepartment.CreatedDate = DateFoundedDepartment.Value;

        SelectedCompany = null;
        DepartmentName = "";
        AddressDepartment = "";
        SelectedUser = null;
        PhoneNumberDepartment = "";
        DescriptionDepartment = "";
        DateFoundedDepartment = DateTime.UtcNow;

        await _departmentEndpoint.UpdateDepartmentAsync(department);
        NotifyOfPropertyChange(() => Departments);
    }

    public string FilterArchivedCompanyButtonColor
    {
        get
        {
            if (_filterByArchivedCompany)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task FilterArchivedCompany()
    {
        if (_filterByArchivedCompany == false)
        {
            var companyList = await _companyEndpoint.GetAllAsync();
            var companies = _mapper.Map<List<CompanyDisplayModel>>(companyList)
                .Where(x => x.Archived)
                .ToList();

            Companies = new BindingList<CompanyDisplayModel>(companies);
        }
        else
        {
            await LoadAllCompanies();
        }

        _filterByArchivedCompany = !_filterByArchivedCompany;
        NotifyOfPropertyChange(() => FilterArchivedCompanyButtonColor);
    }

    public string FilterArchivedDepartmentButtonColor
    {
        get
        {
            if (_filterByArchivedDepartment)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task FilterArchivedDepartment()
    {
        if (_filterByArchivedDepartment == false)
        {
            var departmentList = await _departmentEndpoint.GetAllAsync();
            var departments = _mapper.Map<List<DepartmentDisplayModel>>(departmentList)
                .Where(x => x.Archived)
                .ToList();

            Departments = new BindingList<DepartmentDisplayModel>(departments);
        }
        else
        {
            await LoadAllDepartments();
        }

        _filterByArchivedDepartment = !_filterByArchivedDepartment;
        NotifyOfPropertyChange(() => FilterArchivedDepartmentButtonColor);
    }
}
