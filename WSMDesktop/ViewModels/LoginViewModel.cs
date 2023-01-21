﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UI.Library.API;
using WSMDesktop.EventModels;

namespace WSMDesktop.ViewModels;

public class LoginViewModel : Screen
{
    private readonly IAPIHelper _apiHelper;
    private readonly IEventAggregator _events;

    public LoginViewModel(IAPIHelper apiHelper,
					   IEventAggregator events)
	{
        _apiHelper = apiHelper;
        _events = events;
    }

	private string _email;

	public string Email
	{
		get { return _email; }
		set 
		{ 
			_email = value; 
			NotifyOfPropertyChange(() => Email);
            NotifyOfPropertyChange(() => CanLogIn);
            NotifyOfPropertyChange(() => LoginButtonColor);
        }
	}

	private string _password;

	public string Password
	{
		get { return _password; }
		set 
		{ 
			_password = value; 
			NotifyOfPropertyChange(() => Password);
			NotifyOfPropertyChange(() => CanLogIn);
			NotifyOfPropertyChange(() => LoginButtonColor);
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
			NotifyOfPropertyChange(() => IsErrorVisible);
		}
	}

	public bool CanLogIn
	{
		get
		{
			if (Email?.Length > 0 && Password?.Length > 0)
			{
				return true;
			}

			return false;
		}
	}

	public bool IsErrorVisible
	{
		get
		{
			if (ErrorMessage?.Length > 0)
			{
				return true;
			}

			return false;
		}
	}

	public string LoginButtonColor
	{
		get
		{
			if (CanLogIn is true)
			{
				return "#121212";
			}

			return "Red";
		}
	}

	public async Task LogIn()
	{
		try
		{
			ErrorMessage = "";

			var result = await _apiHelper.Authenticate(Email, Password);

			// Capture info about the user
			await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

			await _events.PublishOnCurrentThreadAsync(new LogOnEvent(), new CancellationToken());
		}
		catch (Exception ex)
		{
			ErrorMessage = ex.Message;
		}
	}
}