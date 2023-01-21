using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Library.API;

namespace WSMDesktop.ViewModels;

public class MaintenanceViewModel : Screen
{
    private readonly IEventAggregator _events;
    private readonly IWindowManager _window;
    private readonly ICompanyEndpoint _companyEndpoint;
    private readonly IDepartmentEndpoint _departmentEndpoint;
    private readonly IJobTitleEndpoint _jobEndpoint;
    private readonly IUserEndpoint _userEndpoint;
    private readonly ITaskEndpoint _taskEndpoint;

    public MaintenanceViewModel(IEventAggregator events,       
                                IWindowManager window,
                                ICompanyEndpoint companyEndpoint,
                                IDepartmentEndpoint departmentEndpoint,
                                IJobTitleEndpoint jobEndpoint,
                                IUserEndpoint userEndpoint,
                                ITaskEndpoint taskEndpoint)
	{
        _events = events;
        _window = window;
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
    }

    public async Task LoadDepartments()
    {
        var departmentList = await _departmentEndpoint.GetAll();
    }

    public async Task LoadUsers()
    {
        var userList = await _userEndpoint.GetAll();
    }

    public async Task LoadJobs()
    {
        var jobList = await _jobEndpoint.GetAll();
    }

}
