using System;
using System.ComponentModel;

namespace WSMDesktop.Models;

public class TaskDisplayModel : INotifyPropertyChanged
{
    public int Id { get; set; }
    private string? _userId;

    public string? UserId
    {
        get { return _userId; }
        set 
        { 
            _userId = value; 
            CallPropertyChanged(nameof(UserId));
        }
    }

    private int? _departmentId;

    public int? DepartmentId
    {
        get { return _departmentId; }
        set 
        { 
            _departmentId = value; 
            CallPropertyChanged(nameof(DepartmentId));
        }
    }


    private string _title;

    public string Title
    {
        get { return _title; }
        set 
        { 
            _title = value; 
            CallPropertyChanged(nameof(Title));
            CallPropertyChanged(nameof(DisplayText));
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

    private DateTime _dateDue;

    public DateTime DateDue
    {
        get { return _dateDue; }
        set 
        { 
            _dateDue = value; 
            CallPropertyChanged(nameof(DateDue));
            CallPropertyChanged(nameof(DisplayText));
        }
    }


    private int _percentageDone;

    public int PercentageDone
    {
        get { return _percentageDone; }
        set 
        { 
            _percentageDone = value;
            CallPropertyChanged(nameof(PercentageDone));
            CallPropertyChanged(nameof(DisplayText));
        }
    }

    private bool _isDone;

    public bool IsDone
    {
        get { return _isDone; }
        set 
        { 
            _isDone = value; 
            CallPropertyChanged(nameof(IsDone));
        }
    }

    private DateTime _dateCreated;

    public DateTime DateCreated
    {
        get { return _dateCreated; }
        set 
        { 
            _dateCreated = value; 
            CallPropertyChanged(nameof(DateCreated));
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


    public string FormattedDateDue()
    {
        return DateDue.ToString("dd/MM/yyyy");
    }

    public string DisplayText
    {
        get
        {
            return $"{Title} dued {FormattedDateDue()} -- {PercentageDone}%";
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void CallPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
