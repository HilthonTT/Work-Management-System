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

namespace WSMDesktop.ViewModels;

public class UserRolesViewModel : Screen
{
    private readonly IUserEndpoint _userEndpoint;
    private readonly IJobTitleEndpoint _jobEndpoint;
    private readonly IWindowManager _window;
    private readonly StatusViewModel _status;

    public UserRolesViewModel(IUserEndpoint userEndpoint,
						   IJobTitleEndpoint jobEndpoint,
						   IWindowManager window,
						   StatusViewModel status)
	{
        _userEndpoint = userEndpoint;
        _jobEndpoint = jobEndpoint;
        _window = window;
        _status = status;
    }

    protected override async void OnViewLoaded(object view)
    {
        base.OnViewLoaded(view);
        try
        {
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

    private async Task LoadUsers()
    {
        var userList = await _userEndpoint.GetAll();
        Users = new BindingList<UserModel>(userList);
    }

    private async Task LoadRoles()
    {
        var roles = await _userEndpoint.GetAllRoles();

        AvailableRoles.Clear();

        foreach (var role in roles)
        {
            if (UserRoles.IndexOf(role.Value) < 0)
            {
                AvailableRoles.Add(role.Value);
            }
        }
    }

    private async Task LoadJobs()
    {
        var jobs = await _jobEndpoint.GetAll();

        AvailableJobs.Clear();

        foreach (var job in jobs)
        {
            if (UserJobs.IndexOf(job.JobName) < 0)
            {
                AvailableJobs.Add(job.JobName);
            }
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
            SelectedUserName = $"{value.FirstName} {value.LastName}";
            UserRoles = new BindingList<string>(value.Roles.Select(x => x.Value).ToList());
            LoadRoles();
            UserJobs = new BindingList<string>(value.JobTitles.Select(x => x.JobName).ToList());
            LoadJobs();
            NotifyOfPropertyChange(() => SelectedUser);
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


    private BindingList<string> _userRoles;

    public BindingList<string> UserRoles
    {
        get { return _userRoles; }
        set 
        { 
            _userRoles = value; 
            NotifyOfPropertyChange(() => UserRoles);
        }
    }

    private string _selectedUserRole;

    public string SelectedUserRole
    {
        get { return _selectedUserRole; }
        set 
        { 
            _selectedUserRole = value; 
            NotifyOfPropertyChange(() => SelectedUserRole);
        }
    }

    private BindingList<string> _availableRoles = new();

    public BindingList<string> AvailableRoles
    {
        get { return _availableRoles; }
        set 
        { 
            _availableRoles = value; 
            NotifyOfPropertyChange(() => AvailableRoles);
        }
    }

    private string _selectedAvailableRole;

    public string SelectedAvailableRole
    {
        get { return _selectedAvailableRole; }
        set 
        { 
            _selectedAvailableRole = value; 
            NotifyOfPropertyChange(() => SelectedAvailableRole);
        }
    }

    private BindingList<string> _userJobs;

    public BindingList<string> UserJobs
    {
        get { return _userJobs; }
        set 
        { 
            _userJobs = value; 
            NotifyOfPropertyChange(() => UserJobs);
        }
    }

    private string _selectedUserJob;

    public string SelectedUserJob
    {
        get { return _selectedUserJob; }
        set 
        { 
            _selectedUserJob = value; 
            NotifyOfPropertyChange(() => SelectedUserJob);
        }
    }



    private BindingList<string> _availableJobs = new();

    public BindingList<string> AvailableJobs
    {
        get { return _availableJobs; }
        set 
        { 
            _availableJobs = value; 
            NotifyOfPropertyChange(() => AvailableJobs);
        }
    }

    private string _selectedAvailableJob;

    public string SelectedAvailableJob
    {
        get { return _selectedAvailableJob; }
        set 
        { 
            _selectedAvailableJob = value; 
            NotifyOfPropertyChange(() => SelectedAvailableJob);
        }
    }


    public async Task AddSelectedRole()
    {
        
    }

    public async Task RemoveSelectedRole()
    {

    }

    public async Task AddSelectedJob()
    {

    }

    public async Task RemoveSelectedJob()
    {

    }

}
