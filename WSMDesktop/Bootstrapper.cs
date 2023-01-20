using Caliburn.Micro;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI.Library.API;
using UI.Library.Models;

namespace WSMDesktop;

public class Bootstrapper : BootstrapperBase
{
    private SimpleContainer _container = new();
	public Bootstrapper()
	{
		Initialize();
	}

    private IConfiguration AddConfiguration()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

#if DEBUG
        builder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
#endif
        return builder.Build();
    }

    protected override void Configure()
    {
        _container.Instance(_container)
            .PerRequest<ICompanyEndpoint, CompanyEndpoint>()
            .PerRequest<IDepartmentEndpoint, DepartmentEndpoint>()
            .PerRequest<IJobTitleEndpoint, JobTitleEndpoint>()
            .PerRequest<IUserEndpoint, UserEndpoint>()
            .PerRequest<ITaskEndpoint, TaskEndpoint>();

        _container
            .Singleton<IWindowManager, WindowManager>()
            .Singleton<IEventAggregator, EventAggregator>()
            .Singleton<ILoggedInUserModel, LoggedInUserModel>()
            .Singleton<IAPIHelper, APIHelper>();

        _container.RegisterInstance(typeof(IConfigurationBuilder), "IConfiguration", AddConfiguration());

        GetType().Assembly.GetTypes()
            .Where(type => type.IsClass)
            .Where(type => type.Name.EndsWith("ViewModel"))
            .ToList()
            .ForEach(viewModelType => _container.RegisterPerRequest(
                viewModelType, viewModelType.ToString(), viewModelType)); 
    }


    protected override void OnStartup(object sender, StartupEventArgs e)
    {
        base.OnStartup(sender, e);
    }

    protected override object GetInstance(Type service, string key)
    {
        return _container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
        return _container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
        _container.BuildUp(instance);
    }
}
