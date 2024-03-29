﻿using System;
using System.ComponentModel;

namespace WSMDesktop.Models;

public class ItemDisplayModel : INotifyPropertyChanged
{
    public int Id { get; set; }
    private string _modelName;

    public string ModelName
    {
        get { return _modelName; }
        set 
        { 
            _modelName = value; 
            CallPropertyChanged(nameof(ModelName));
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
            CallPropertyChanged(nameof(DisplayText));
        }
    }

    private int _quantity;

    public int Quantity
    {
        get { return _quantity; }
        set 
        { 
            _quantity = value; 
            CallPropertyChanged(nameof(Quantity));
        }
    }

    private decimal _price;

    public decimal Price
    {
        get { return _price; }
        set 
        { 
            _price = value; 
            CallPropertyChanged(nameof(Price));
            CallPropertyChanged(nameof(DisplayText));
        }
    }

    private string _location;

    public string Location
    {
        get { return _location; }
        set 
        { 
            _location = value; 
            CallPropertyChanged(nameof(Location));
        }
    }

    private string? _internalSupplierPersonId;

    public string? InternalSupplierPersonId
    {
        get { return _internalSupplierPersonId; }
        set 
        { 
            _internalSupplierPersonId = value; 
            CallPropertyChanged(nameof(InternalSupplierPersonId));
        }
    }

    private int? _internalSupplierCompanyId;

    public int? InternalSupplierCompanyId
    {
        get { return _internalSupplierCompanyId; }
        set 
        { 
            _internalSupplierCompanyId = value; 
            CallPropertyChanged(nameof(InternalSupplierCompanyId));
        }
    }


    private decimal? _ean;

    public decimal? EAN
    {
        get { return _ean; }
        set 
        { 
            _ean = value;
            CallPropertyChanged(nameof(EAN));
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
            CallPropertyChanged(nameof(DisplayText));
        }
    }


    public string DisplayText
    {
        get
        {
            return $"{ModelName} - {Price}€ - Archived: {Archived}";
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void CallPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
    }
}
