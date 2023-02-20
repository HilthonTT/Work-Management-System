using System;
using System.ComponentModel;

namespace WSMDesktop.Models;

public class CompanyDisplayModel : INotifyPropertyChanged
{
    public int Id { get; set; }
    private string _companyName;

    public string CompanyName
    {
        get { return _companyName; }
        set 
        {
            _companyName = value;
            CallPropertyChanged(nameof(CompanyName));
            CallPropertyChanged(nameof(DisplayText));
        }
    }

    private string _address;

    public string Address
    {
        get { return _address; }
        set 
        { 
            _address = value; 
            CallPropertyChanged(nameof(Address));
        }
    }

    private string _phoneNumber;

    public string PhoneNumber
    {
        get { return _phoneNumber; }
        set 
        { 
            _phoneNumber = value; 
            CallPropertyChanged(nameof(PhoneNumber));
            CallPropertyChanged(nameof(DisplayText));
        }
    }

    private string? _chairPersonId;

    public string? ChairPersonId
    {
        get { return _chairPersonId; }
        set 
        { 
            _chairPersonId = value; 
            CallPropertyChanged(nameof(ChairPersonId));
        }
    }

    private string _description;

    public string Description
    {
        get { return _description; }
        set 
        { 
            _description = value; 
            CallPropertyChanged(nameof(Description));
        }
    }

    private DateTime _dateFounded;

    public DateTime DateFounded
    {
        get { return _dateFounded; }
        set 
        { 
            _dateFounded = value; 
            CallPropertyChanged(nameof(DateFounded));
        }
    }

    private bool _archived;

    public bool Archived
    {
        get { return _archived; }
        set 
        { 
            _archived = value; 
            CallPropertyChanged(nameof(Archived));
        }
    }


    public string DisplayText
    {
        get
        {
            return $"{CompanyName} - ☎️{PhoneNumber}";
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void CallPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
