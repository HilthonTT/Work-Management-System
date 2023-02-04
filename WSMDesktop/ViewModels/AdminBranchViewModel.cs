using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI.Library.API;
using WSMDesktop.Models;

namespace WSMDesktop.ViewModels;

public class AdminBranchViewModel : Screen
{
    private readonly ICompanyEndpoint _companyEndpoint;
    private readonly StatusViewModel _status;
    private readonly IMapper _mapper;
    private readonly IWindowManager _window;

    public AdminBranchViewModel(ICompanyEndpoint companyEndpoint,
                                 StatusViewModel status,
                                 IMapper mapper,
                                 IWindowManager window)
	{
        _companyEndpoint = companyEndpoint;
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

    public async Task LoadAllCompanies()
    {
        var companyList = await _companyEndpoint.GetAll();
        var companies = _mapper.Map<List<CompanyDisplayModel>>(companyList);
    }

    public async Task LoadAllDepartments()
    {
        var departmentList =await _companyEndpoint.GetAll();
        var companies = _mapper.Map<List<DepartmentDisplayModel>>(departmentList);
    }
}
