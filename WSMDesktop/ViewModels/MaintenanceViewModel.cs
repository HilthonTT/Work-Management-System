using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Library.API;
using UI.Library.Models;
using WSMDesktop.Models;

namespace WSMDesktop.ViewModels;

public class MaintenanceViewModel : Screen
{
    private readonly IEventAggregator _events;
    private readonly IWindowManager _window;
    private readonly IMapper _mapper;
    private readonly ICompanyEndpoint _companyEndpoint;
    private readonly IDepartmentEndpoint _departmentEndpoint;
    private readonly IJobTitleEndpoint _jobEndpoint;
    private readonly IUserEndpoint _userEndpoint;
    private readonly ITaskEndpoint _taskEndpoint;

    public MaintenanceViewModel(IEventAggregator events,       
                                IWindowManager window,
                                IMapper mapper,
                                ICompanyEndpoint companyEndpoint,
                                IDepartmentEndpoint departmentEndpoint,
                                IJobTitleEndpoint jobEndpoint,
                                IUserEndpoint userEndpoint,
                                ITaskEndpoint taskEndpoint)
	{
        _events = events;
        _window = window;
        _mapper = mapper;
        _companyEndpoint = companyEndpoint;
        _departmentEndpoint = departmentEndpoint;
        _jobEndpoint = jobEndpoint;
        _userEndpoint = userEndpoint;
        _taskEndpoint = taskEndpoint;
    }

    protected override async void OnViewLoaded(object view)
    {
        base.OnViewLoaded(view);
        try
        {
            await LoadCompanies();
            await LoadDepartments();
            await LoadUsers();
            await LoadJobs();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task LoadCompanies()
    {
        var companyList = await _companyEndpoint.GetAll();
        var companies = _mapper.Map<List<CompanyDisplayModel>>(companyList);
        Companies = new BindingList<CompanyDisplayModel>(companies);
    }

    public async Task LoadDepartments()
    {
        var departmentList = await _departmentEndpoint.GetAll();
        var departments = _mapper.Map<List<DepartmentDisplayModel>>(departmentList);
        Departments = new BindingList<DepartmentDisplayModel>(departments);
    }

    public async Task LoadUsers()
    {
        var userList = await _userEndpoint.GetAll();
        Users = new BindingList<UserModel>(userList);
    }

    public async Task LoadJobs()
    {
        var jobList = await _jobEndpoint.GetAll();
        JobTitles = new BindingList<JobTitleModel>(jobList);
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
            NotifyOfPropertyChange(() => SelectedCompany);
            NotifyOfPropertyChange(() => AssignTaskButtonColor);
            NotifyOfPropertyChange(() => CanAssignTask);
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
            NotifyOfPropertyChange(() => AssignTaskButtonColor);
            NotifyOfPropertyChange(() => CanAssignTask);
        }
    }


    private BindingList<JobTitleModel> _jobTitles;

    public BindingList<JobTitleModel> JobTitles
    {
        get { return _jobTitles; }
        set 
        { 
            _jobTitles = value; 
            NotifyOfPropertyChange(() => JobTitles);
        }
    }

    private JobTitleModel _selectedJobTitle;

    public JobTitleModel SelectedJobTitle
    {
        get { return _selectedJobTitle; }
        set 
        { 
            _selectedJobTitle = value; 
            NotifyOfPropertyChange(() => SelectedJobTitle);
            NotifyOfPropertyChange(() => AssignTaskButtonColor);
            NotifyOfPropertyChange(() => CanAssignTask);
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
            NotifyOfPropertyChange(() => AssignTaskButtonColor);
            NotifyOfPropertyChange(() => CanAssignTask);
        }
    }

    private string _taskDescription;

    public string TaskDescription
    {
        get { return _taskDescription; }
        set 
        { 
            _taskDescription = value; 
            NotifyOfPropertyChange(() => TaskDescription);
            NotifyOfPropertyChange(() => AssignTaskButtonColor);
            NotifyOfPropertyChange(() => CanAssignTask);
        }
    }

    public string AssignTaskButtonColor
    {
        get
        {
            if (CanAssignTask is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public bool CanAssignTask
    {
        get
        {
            if (SelectedCompany is not null && 
                SelectedDepartment is not null && 
                SelectedJobTitle is not null &&
                SelectedUser is not null &&
                string.IsNullOrWhiteSpace(TaskDescription) == false)
            {
                return true;
            }

            return false;
        }
    }

    public async Task AssignTask()
    {
        
    }

}
