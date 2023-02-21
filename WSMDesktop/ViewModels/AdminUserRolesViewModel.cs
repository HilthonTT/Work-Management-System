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

namespace WSMDesktop.ViewModels;

public class AdminUserRolesViewModel : Screen
{
    private readonly IUserEndpoint _userEndpoint;
    private readonly IJobTitleEndpoint _jobEndpoint;
    private readonly IMapper _mapper;
    private readonly IWindowManager _window;
    private readonly StatusViewModel _status;

    public AdminUserRolesViewModel(IUserEndpoint userEndpoint,
						   IJobTitleEndpoint jobEndpoint,
                           IMapper mapper,
						   IWindowManager window,
						   StatusViewModel status)
	{
        _userEndpoint = userEndpoint;
        _jobEndpoint = jobEndpoint;
        _mapper = mapper;
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

    private string _searchUserText;

    public string SearchUserText
    {
        get { return _searchUserText; }
        set 
        {
            _searchUserText = value;
            NotifyOfPropertyChange(() => SearchUserText);
            SearchUser();
        }
    }

    public static string SearchUserButtonColor
    {
        get
        {
            return "#121212";
        }
    }

    public async Task SearchUser()
    {
        var userList = await _userEndpoint.GetAllAsync();
        var output = _mapper.Map<List<UserModel>>(userList);

        if (string.IsNullOrWhiteSpace(SearchUserText) == false)
        {
            output = output.Where(x => x.FirstName.Contains(SearchUserText, StringComparison.InvariantCultureIgnoreCase) ||
                    x.LastName.Contains(SearchUserText, StringComparison.InvariantCultureIgnoreCase) ||
                    x.EmailAddress.Contains(SearchUserText, StringComparison.InvariantCultureIgnoreCase) ||
                    x.PhoneNumber.Contains(SearchUserText, StringComparison.InvariantCultureIgnoreCase)).ToList();

            Users = new BindingList<UserModel>(output);
        }
        else
        {
            Users = new BindingList<UserModel>(output);
        }
    }

    private async Task LoadUsers()
    {
        var userList = await _userEndpoint.GetAllAsync();
        Users = new BindingList<UserModel>(userList);
    }

    private async Task LoadRoles()
    {
        var roles = await _userEndpoint.GetAllRolesAsync();

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
        var jobs = await _jobEndpoint.GetAllAsync();

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
            SelectedUserName = $"{value?.FirstName} {value?.LastName}";
            if (SelectedUser is not null)
            {
                UserRoles = new BindingList<string>(value.Roles.Select(x => x.Value).ToList());
                LoadRoles();
                UserJobs = new BindingList<string>(value.JobTitles.Select(x => x.JobName).ToList());
                LoadJobs();
            }
            else if (SelectedUser is null)
            {
                LoadRoles();
                LoadJobs();
            }
            NotifyOfPropertyChange(() => SelectedUser);
            NotifyOfPropertyChange(() => SelectedUserButtonColor);
        }
    }

    private string? _selectedUserName;

    public string? SelectedUserName
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
            NotifyOfPropertyChange(() => CanAddSelectedRole);
            NotifyOfPropertyChange(() => CanRemoveSelectedRole);
            NotifyOfPropertyChange(() => RemoveSelectedRoleButtonColor);
            NotifyOfPropertyChange(() => AddSelectedRoleButtonColor);
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
            NotifyOfPropertyChange(() => CanAddSelectedRole);
            NotifyOfPropertyChange(() => CanRemoveSelectedRole);
            NotifyOfPropertyChange(() => RemoveSelectedRoleButtonColor);
            NotifyOfPropertyChange(() => AddSelectedRoleButtonColor);
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
            NotifyOfPropertyChange(() => CanAddSelectedJob);
            NotifyOfPropertyChange(() => CanRemoveSelectedJob);
            NotifyOfPropertyChange(() => RemoveSelectedJobButtonColor);
            NotifyOfPropertyChange(() => AddSelectedJobButtonColor);
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
            NotifyOfPropertyChange(() => CanAddSelectedJob);
            NotifyOfPropertyChange(() => CanRemoveSelectedJob);
            NotifyOfPropertyChange(() => RemoveSelectedJobButtonColor);
            NotifyOfPropertyChange(() => AddSelectedJobButtonColor);
        }
    }

    public bool CanAddSelectedRole
    {
        get
        {
            if (SelectedUser is null || SelectedAvailableRole is null)
                return false;

            return true;
        }
    }

    public string AddSelectedRoleButtonColor
    {
        get
        {
            if (CanAddSelectedRole is true)
                return "#121212";

            return "Red";
        }
    }


    public async Task AddSelectedRole()
    {
        await _userEndpoint.AddUserToRoleAsync(SelectedUser.Id, SelectedAvailableRole);

        UserRoles.Add(SelectedAvailableRole);
        AvailableRoles.Remove(SelectedAvailableRole);

        NotifyOfPropertyChange(() => Users);
        NotifyOfPropertyChange(() => AvailableRoles);
        NotifyOfPropertyChange(() => UserRoles);
    }

    public bool CanRemoveSelectedRole
    {
        get
        {
            if (SelectedUser is null || SelectedUserRole is null)
                return false;

            return true;
        }
    }

    public string RemoveSelectedRoleButtonColor
    {
        get
        {
            if (CanRemoveSelectedRole is true)
                return "#121212";

            return "Red";
        }
    }

    public async Task RemoveSelectedRole()
    {
        await _userEndpoint.RemoveUserFromRoleAsync(SelectedUser.Id, SelectedUserRole);

        AvailableRoles.Add(SelectedAvailableRole);
        UserRoles.Remove(SelectedAvailableRole);

        NotifyOfPropertyChange(() => Users);
        NotifyOfPropertyChange(() => AvailableRoles);
        NotifyOfPropertyChange(() => UserRoles);
    }

    public bool CanAddSelectedJob
    {
        get
        {
            if (SelectedUser is null || SelectedAvailableJob is null)
                return false;

            return true;
        }
    }

    public string AddSelectedJobButtonColor
    {
        get
        {
            if (CanAddSelectedJob is true)
                return "#121212";

            return "Red";
        }
    }


    public async Task AddSelectedJob()
    {
        var updatedUser = SelectedUser;
        var jobs = await _jobEndpoint.GetAllAsync();
        var chosenJob = jobs.Where(x => x.JobName == SelectedAvailableJob).FirstOrDefault();
        updatedUser.JobTitleId = chosenJob.Id;

        await _userEndpoint.UpdateUserJobTitleIdAsync(updatedUser);

        UserJobs.Add(SelectedAvailableJob);
        AvailableJobs.Remove(SelectedAvailableJob);

        NotifyOfPropertyChange(() => Users);
        NotifyOfPropertyChange(() => AvailableJobs);
        NotifyOfPropertyChange(() => UserJobs);
    }

    public bool CanRemoveSelectedJob
    {
        get
        {
            if (SelectedUser is null || SelectedUserJob is null)
                return false;

            return true;
        }
    }

    public string RemoveSelectedJobButtonColor
    {
        get
        {
            if (CanRemoveSelectedJob is true)
                return "#121212";

            return "Red";
        }
    }

    public async Task RemoveSelectedJob()
    {
        var updatedUser = SelectedUser;
        updatedUser.JobTitleId = null;

        await _userEndpoint.UpdateUserJobTitleIdAsync(updatedUser);

        UserJobs.Remove(SelectedAvailableJob);
        AvailableJobs.Add(SelectedAvailableJob);

        NotifyOfPropertyChange(() => Users);
        NotifyOfPropertyChange(() => AvailableJobs);
        NotifyOfPropertyChange(() => UserJobs);
    }
}
