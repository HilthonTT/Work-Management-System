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
    private readonly IDepartmentEndpoint _departmentEndpoint;
    private readonly IUserEndpoint _userEndpoint;
    private readonly ITaskEndpoint _taskEndpoint;
    private readonly StatusViewModel _status;
    private bool _filterByArchivedDepartment = false;

    public AdminMaintenanceViewModel(IEventAggregator events,       
                                IWindowManager window,
                                IMapper mapper,
                                IDepartmentEndpoint departmentEndpoint,
                                IUserEndpoint userEndpoint,
                                ITaskEndpoint taskEndpoint,
                                StatusViewModel status)
	{
        _events = events;
        _window = window;
        _mapper = mapper;
        _departmentEndpoint = departmentEndpoint;
        _userEndpoint = userEndpoint;
        _taskEndpoint = taskEndpoint;
        _status = status;
    }

    protected override async void OnViewLoaded(object view)
    {
        base.OnViewLoaded(view);
        try
        {
            await LoadDepartments();
            await LoadUsers();
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

    public async Task SearchUser()
    {
        var userList = await _userEndpoint.GetAllAsync();
        var output = _mapper.Map<List<UserModel>>(userList);

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

    private string _departmentSearchText;

    public string DepartmentSearchText
    {
        get { return _departmentSearchText; }
        set 
        { 
            _departmentSearchText = value; 
            NotifyOfPropertyChange(() => DepartmentSearchText);
            SearchDepartment();
        }
    }

    public async Task SearchDepartment()
    {
        var departmentList = await _departmentEndpoint.GetAllAsync();
        var output = _mapper.Map<List<DepartmentDisplayModel>>(departmentList.Where(x => x.Archived == false));
        

        if (string.IsNullOrWhiteSpace(DepartmentSearchText) == false)
        {
            output = output.Where(x => x.DepartmentName.Contains(DepartmentSearchText, StringComparison.InvariantCultureIgnoreCase) ||
                x.Description.Contains(DepartmentSearchText, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            Departments = new BindingList<DepartmentDisplayModel>(output);
        }
        else
        {
            Departments = new BindingList<DepartmentDisplayModel>(output);
        }
    }

    public async Task LoadDepartments()
    {
        var departmentList = await _departmentEndpoint.GetAllAsync();
        var departments = _mapper.Map<List<DepartmentDisplayModel>>(departmentList).Where(x => x.Archived == false).ToList();
        Departments = new BindingList<DepartmentDisplayModel>(departments);
    }

    public async Task LoadUsers()
    {
        var userList = await _userEndpoint.GetAllAsync();
        Users = new BindingList<UserModel>(userList);
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
            NotifyOfPropertyChange(() => SelectedDepartment);
            NotifyOfPropertyChange(() => AssignTaskButtonColor);
            NotifyOfPropertyChange(() => CanAssignTask);
            NotifyOfPropertyChange(() => SelectedDepartmentButtonColor);
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
            NotifyOfPropertyChange(() => AssignTaskButtonColor);
            NotifyOfPropertyChange(() => CanAssignTask);
            NotifyOfPropertyChange(() => SelectedUserButtonColor);
        }
    }

    public string TitleButtonColor
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Title) == false)
            {
                return "Green";
            }

            return "Red";
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
            NotifyOfPropertyChange(() => TitleButtonColor);
        }
    }

    public string DateDueButtonColor
    {
        get
        {
            if (DateDue >= SqlDateTime.MinValue.Value)
            {
                return "Green";
            }

            return "Red";
        }
    }

    private DateTime _dateDue = DateTime.UtcNow;

    public DateTime DateDue
    {
        get { return _dateDue; }
        set 
        {
            _dateDue = value;
            NotifyOfPropertyChange(() => DateDue);
            NotifyOfPropertyChange(() => AssignTaskButtonColor);
            NotifyOfPropertyChange(() => CanAssignTask);
            NotifyOfPropertyChange(() => DateDueButtonColor);
        }
    }

    public string DescriptionButtonColor
    {
        get
        {
            if (string.IsNullOrWhiteSpace(TaskDescription) == false)
            {
                return "Green";
            }

            return "Red";
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
            NotifyOfPropertyChange(() => DescriptionButtonColor);
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
            if (string.IsNullOrWhiteSpace(Title) == false &&
                string.IsNullOrWhiteSpace(TaskDescription) == false &&
                DateDue >= SqlDateTime.MinValue.Value)
            {
                if (SelectedUser is null && SelectedDepartment is null)
                {
                    return false;
                }

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
                DepartmentId = SelectedDepartment?.Id,
                Title = Title,
                Description = TaskDescription,
                DateDue = DateDue,
                PercentageDone = 0,
                IsDone = false,
                DateCreated = DateTime.UtcNow,
            };

            await _events.PublishOnCurrentThreadAsync(new PostTaskEvent(), new CancellationToken());
            await _taskEndpoint.PostTaskAsync(newTask);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
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
            await LoadDepartments();
        }

        _filterByArchivedDepartment = !_filterByArchivedDepartment;
        NotifyOfPropertyChange(() => FilterArchivedDepartmentButtonColor);
    }
}
