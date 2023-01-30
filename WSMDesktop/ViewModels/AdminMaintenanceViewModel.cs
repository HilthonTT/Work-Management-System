using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using UI.Library.API;
using UI.Library.Models;
using WSMDesktop.EventModels;
using WSMDesktop.Models;

namespace WSMDesktop.ViewModels;

public class AdminMaintenanceViewModel : Screen
{
    private readonly IEventAggregator _events;
    private readonly IWindowManager _window;
    private readonly IMapper _mapper;
    private readonly ICompanyEndpoint _companyEndpoint;
    private readonly IDepartmentEndpoint _departmentEndpoint;
    private readonly IJobTitleEndpoint _jobEndpoint;
    private readonly IUserEndpoint _userEndpoint;
    private readonly ITaskEndpoint _taskEndpoint;
    private readonly StatusViewModel _status;

    public AdminMaintenanceViewModel(IEventAggregator events,       
                                IWindowManager window,
                                IMapper mapper,
                                ICompanyEndpoint companyEndpoint,
                                IDepartmentEndpoint departmentEndpoint,
                                IJobTitleEndpoint jobEndpoint,
                                IUserEndpoint userEndpoint,
                                ITaskEndpoint taskEndpoint,
                                StatusViewModel status)
	{
        _events = events;
        _window = window;
        _mapper = mapper;
        _companyEndpoint = companyEndpoint;
        _departmentEndpoint = departmentEndpoint;
        _jobEndpoint = jobEndpoint;
        _userEndpoint = userEndpoint;
        _taskEndpoint = taskEndpoint;
        _status = status;
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
            SelectedCompanyName = $"*{value.CompanyName}";
            NotifyOfPropertyChange(() => SelectedCompany);
            NotifyOfPropertyChange(() => AssignTaskButtonColor);
            NotifyOfPropertyChange(() => CanAssignTask);
            NotifyOfPropertyChange(() => SelectedCompanyName);
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
            SelectedDepartmentName = $"*{value.DepartmentName}";
            NotifyOfPropertyChange(() => SelectedDepartment);
            NotifyOfPropertyChange(() => SelectedDepartmentName);
            NotifyOfPropertyChange(() => AssignTaskButtonColor);
            NotifyOfPropertyChange(() => CanAssignTask);
        }
    }

    private string _selectedDepartmentName;

    public string SelectedDepartmentName
    {
        get { return _selectedDepartmentName; }
        set 
        { 
            _selectedDepartmentName = value; 
            NotifyOfPropertyChange(() => SelectedDepartmentName);
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
            SelectedJobTitleName = $"*{value.JobName}";
            NotifyOfPropertyChange(() => SelectedJobTitle);
            NotifyOfPropertyChange(() => AssignTaskButtonColor);
            NotifyOfPropertyChange(() => CanAssignTask);
        }
    }

    private string _selectedJobTitleName;

    public string SelectedJobTitleName
    {
        get { return _selectedJobTitleName; }
        set 
        { 
            _selectedJobTitleName = value; 
            NotifyOfPropertyChange(() => SelectedJobTitleName);
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
            SelectedUserName = $"*{value.FirstName} {value.LastName}";
            NotifyOfPropertyChange(() => SelectedUser);
            NotifyOfPropertyChange(() => AssignTaskButtonColor);
            NotifyOfPropertyChange(() => CanAssignTask);
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


    private string _title;

    public string Title
    {
        get { return _title; }
        set 
        { 
            _title = value; 
            NotifyOfPropertyChange(() => Title);
            NotifyOfPropertyChange(() => AssignTaskButtonColor);
            NotifyOfPropertyChange(() => CanAssignTask);
        }
    }

    private DateTime _dateDue = SqlDateTime.MinValue.Value;

    public DateTime DateDue
    {
        get { return _dateDue; }
        set 
        {
            _dateDue = value;
            NotifyOfPropertyChange(() => DateDue);
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

    private string _errorMessage;

    public string ErrorMessage
    {
        get { return _errorMessage; }
        set 
        { 
            _errorMessage = value; 
            NotifyOfPropertyChange(() => ErrorMessage);
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
                string.IsNullOrWhiteSpace(Title) == false &&
                string.IsNullOrWhiteSpace(TaskDescription) == false &&
                DateDue >= SqlDateTime.MinValue.Value == true)
            {
                return true;
            }

            return false;
        }
    }

    public async Task AssignTask()
    {
        try
        {
            TaskModel newTask = new()
            {
                UserId = SelectedUser.Id,
                DepartmentId = SelectedDepartment.Id,
                Title = Title,
                Description = TaskDescription,
                DateDue = DateDue,
                PercentageDone = 0,
                IsDone = false,
                DateCreated = DateTime.UtcNow,
            };

            await _events.PublishOnCurrentThreadAsync(new PostTaskEvent(), new CancellationToken());
            await _taskEndpoint.PostTask(newTask);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
