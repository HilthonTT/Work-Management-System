using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UI.Library.API;
using UI.Library.Models;
using WSMDesktop.EventModels;

namespace WSMDesktop.ViewModels;

public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
{
    private readonly IEventAggregator _events;
    private readonly ILoggedInUserModel _user;
    private readonly IAPIHelper _apiHelper;

    public ShellViewModel(IEventAggregator events,
                          ILoggedInUserModel user,
                          IAPIHelper apiHelper)
	{
        _events = events;
        _user = user;
        _apiHelper = apiHelper;

        _events.SubscribeOnPublishedThread(this);
        ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
    }

    public bool IsLoggedIn
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_user.Token) == false)
            {
                return true;
            }

            return false;
        }
    }

    public bool IsLoggedOut
    {
        get
        {
            return !IsLoggedIn;
        }
    }

    public async Task LogIn()
    {
        await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
    }

    public async Task LogOut()
    {
        _apiHelper.LogOffUser();
        _user.ResetUserModel();
        await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
        NotifyOfPropertyChange(() => IsLoggedIn);
        NotifyOfPropertyChange(() => IsLoggedOut);
    }

    public async Task Exit()
    {
        await TryCloseAsync();
    }

    public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
    {
        await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
        NotifyOfPropertyChange(() => IsLoggedIn);
        NotifyOfPropertyChange(() => IsLoggedOut);
    }
}
