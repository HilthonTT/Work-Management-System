using System;
using System.ComponentModel;

namespace WSMDesktop.Models;

public class DepartmentDisplayModel : INotifyPropertyChanged
{
    public int Id { get; set; }
    
    private int _companyId;

    public int CompanyId
    {
        get { return _companyId; }
        set 
        { 
            _companyId = value; 
            CallPropertyChanged(nameof(CompanyId));
        }
    }

    private string _departmentName;

    public string DepartmentName
    {
        get { return _departmentName; }
        set 
        { 
            _departmentName = value; 
            CallPropertyChanged(nameof(DepartmentName));
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

    private string _phoneNumber;

    public string PhoneNumber
    {
        get { return _phoneNumber; }
        set 
        { 
            _phoneNumber = value; 
            CallPropertyChanged(nameof(PhoneNumber));
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

    private DateTime _createdDate;

    public DateTime CreatedDate
    {
        get { return _createdDate; }
        set 
        { 
            _createdDate = value;
            CallPropertyChanged(nameof(CreatedDate));
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
            return $"{DepartmentName}";
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void CallPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
