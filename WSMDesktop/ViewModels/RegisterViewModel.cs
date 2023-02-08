using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UI.Library.API;
using UI.Library.Models;
using WSMDesktop.EventModels;

namespace WSMDesktop.ViewModels;

public class RegisterViewModel : Screen
{
    private readonly IUserEndpoint _userEndpoint;
    private readonly IEventAggregator _events;

    public RegisterViewModel(IUserEndpoint userEndpoint,
							 IEventAggregator events)
	{
        _userEndpoint = userEndpoint;
        _events = events;
    }

	private string _firstName;

	public string FirstName
	{
		get { return _firstName; }
		set 
		{ 
			_firstName = value; 
			NotifyOfPropertyChange(() => FirstName);
			NotifyOfPropertyChange(() => CanRegister);
			NotifyOfPropertyChange(() => RegisterButtonColor);
		}
	}

	private string _lastName;

	public string LastName
	{
		get { return _lastName; }
		set 
		{ 
			_lastName = value;
			NotifyOfPropertyChange(() => LastName);
            NotifyOfPropertyChange(() => CanRegister);
            NotifyOfPropertyChange(() => RegisterButtonColor);
        }
	}

	private string _emailAddress;

	public string EmailAddress
	{
		get { return _emailAddress; }
		set { _emailAddress = value; }
	}

	private string _phoneNumber;

	public string PhoneNumber
	{
		get { return _phoneNumber; }
		set 
		{ 
			_phoneNumber = value; 
			NotifyOfPropertyChange(() => PhoneNumber);
            NotifyOfPropertyChange(() => CanRegister);
            NotifyOfPropertyChange(() => RegisterButtonColor);
        }
	}

	private DateTime _dateOfBirth = SqlDateTime.MinValue.Value;

	public DateTime DateOfBirth
	{
		get { return _dateOfBirth; }
		set 
		{
			_dateOfBirth = value; 
			NotifyOfPropertyChange(() => DateOfBirth);
            NotifyOfPropertyChange(() => CanRegister);
            NotifyOfPropertyChange(() => RegisterButtonColor);
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
            NotifyOfPropertyChange(() => CanRegister);
            NotifyOfPropertyChange(() => RegisterButtonColor);
        }
	}

	private string _confirmPassword;

	public string ConfirmPassword
	{
		get { return _confirmPassword; }
		set 
		{ 
			_confirmPassword = value;
			NotifyOfPropertyChange(() => ConfirmPassword);
            NotifyOfPropertyChange(() => CanRegister);
            NotifyOfPropertyChange(() => RegisterButtonColor);
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

	public bool CanRegister
	{
		get
		{
			if (string.IsNullOrWhiteSpace(FirstName) == false &&
				string.IsNullOrWhiteSpace(LastName) == false &&
				string.IsNullOrWhiteSpace(EmailAddress) == false &&
				string.IsNullOrWhiteSpace(PhoneNumber) == false &&
				string.IsNullOrWhiteSpace(Password) == false &&
				string.IsNullOrWhiteSpace(ConfirmPassword) == false &&
				DateOfBirth >= SqlDateTime.MinValue.Value &&
                Password == ConfirmPassword &&
				Password.Length >= 6 &&
				ConfirmPassword.Length >= 6)
			{
				return true;
			}

			return false;
		}
	}

	public string RegisterButtonColor
	{
		get
		{
			if (CanRegister is true)
			{
				return "#121212";
			}

			return "Red";
		}
	}

    public async Task Register()
	{
		try
		{
            CreateUserModel u = new()
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                PhoneNumber = PhoneNumber,
                DateOfBirth = DateOfBirth,
                Password = Password,
                ConfirmPassword = ConfirmPassword
            };

            await _userEndpoint.CreateUserAsync(u);

            await _events.PublishOnCurrentThreadAsync(new RegisteredEvent(), new CancellationToken());
        }
		catch (Exception ex)
		{
			ErrorMessage = ex.Message;
		}
	}
}
