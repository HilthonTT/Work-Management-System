using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using UI.Library.API;
using UI.Library.Models;
using WSMDesktop.EventModels;
using WSMDesktop.Models;

namespace WSMDesktop.ViewModels;

public class TaskViewModel : Screen
{
    private readonly ILoggedInUserModel _user;
    private readonly IMapper _mapper;
    private readonly IWindowManager _window;
    private readonly IEventAggregator _events;
    private readonly StatusViewModel _status;
    private readonly ITaskEndpoint _taskEndpoint;
    private readonly ICompanyEndpoint _companyEndpoint;
    private readonly IDepartmentEndpoint _departmentEndpoint;
    private bool _IsFilteredByDone = false;

    public TaskViewModel(ILoggedInUserModel user, 
                         IMapper mapper,
                         IWindowManager window,
                         IEventAggregator events,
                         StatusViewModel status,
                         ITaskEndpoint taskEndpoint,
                         ICompanyEndpoint companyEndpoint,
                         IDepartmentEndpoint departmentEndpoint)
	{
        _user = user;
        _mapper = mapper;
        _window = window;
        _events = events;
        _status = status;
        _taskEndpoint = taskEndpoint;
        _companyEndpoint = companyEndpoint;
        _departmentEndpoint = departmentEndpoint;
    }

    protected override async void OnViewLoaded(object view)
    {
        base.OnViewLoaded(view);
        try
        {
            await LoadTasks();
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

    public async Task LoadTasks()
    {
        var taskList = await _taskEndpoint.GetByUserId();
        var tasks = _mapper.Map<List<TaskDisplayModel>>(taskList);
        Tasks = new BindingList<TaskDisplayModel>(tasks);
    }

    public async Task FilterByIsDone()
    {
        if (_IsFilteredByDone == false)
        {
            var taskList = await _taskEndpoint.GetByUserId();
            var tasks = _mapper.Map<List<TaskDisplayModel>>(taskList);
            var tasksIsDone = tasks.Where(x => x.IsDone).ToList();
            Tasks = new BindingList<TaskDisplayModel>(tasksIsDone);
            _IsFilteredByDone = true;
        }
        else
        {
            await LoadTasks();
            _IsFilteredByDone = false;
        }
    }

    private BindingList<TaskDisplayModel> _tasks;

    public BindingList<TaskDisplayModel> Tasks
    {
        get { return _tasks; }
        set 
        { 
            _tasks = value; 
            NotifyOfPropertyChange(() => Tasks);
        }
    }

    private TaskDisplayModel _selectedTask;

    public TaskDisplayModel SelectedTask
    {
        get { return _selectedTask; }
        set 
        { 
            _selectedTask = value;
            Title = value?.Title;
            Description = value?.Description;
            DateDue = value?.DateDue;
            DateCreated= value?.DateCreated;
            NotifyOfPropertyChange(() => SelectedTask);
            NotifyOfPropertyChange(() => Percentage);
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
        }
    }

    private string _description;

    public string Description
    {
        get { return _description; }
        set 
        { _description = value; 
           NotifyOfPropertyChange(() => Description);
        }
    }

    private DateTime? _dateDue;

    public DateTime? DateDue
    {
        get { return _dateDue; }
        set 
        {
            _dateDue = value; 
            NotifyOfPropertyChange(() => DateDue);
        }
    }

    public string Percentage
    {
        get
        {
            if (SelectedTask is not null)
            {
                return $"{SelectedTask.PercentageDone}%";
            }

            return "0%";
        }
    }

    private DateTime? _dateCreated;

    public DateTime? DateCreated
    {
        get { return _dateCreated; }
        set 
        { 
            _dateCreated = value; 
            NotifyOfPropertyChange(() => DateCreated);
        }
    }

    private int _percentageNumber;

    public int PercentageNumber
    {
        get { return _percentageNumber; }
        set 
        { 
            _percentageNumber = value; 
            NotifyOfPropertyChange(() => PercentageNumber);
            NotifyOfPropertyChange(() => CanSetPercentage);
            NotifyOfPropertyChange(() => SetPercentageButtonColor);
        }
    }


    public bool CanSetPercentage
    {
        get
        {
            if (SelectedTask is not null && PercentageNumber >= 0 && PercentageNumber <= 100)
            {
                return true;
            }

            return false;
        }
    }

    public string SetPercentageButtonColor
    {
        get
        {
            if (CanSetPercentage is true)
            {
                return "#121212";
            }

            return "Red";
        }
    }

    public async Task SetPercentage()
    {
        TaskModel t = new()
        {
            Id = SelectedTask.Id,
            UserId = SelectedTask.UserId,
            DepartmentId = SelectedTask.DepartmentId,
            Title = SelectedTask.Title,
            Description = SelectedTask.Description,
            DateDue = SelectedTask.DateDue,
            PercentageDone = PercentageNumber,
            IsDone = SelectedTask.IsDone,
            DateCreated = SelectedTask.DateCreated
        };


        await _events.PublishOnCurrentThreadAsync(new UpdatedTaskPercentage(), new CancellationToken());
        await _taskEndpoint.UpdatePercentage(t);
    }
}
