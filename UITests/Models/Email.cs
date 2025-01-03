﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace UITests.Models;

public partial class Email : ObservableObject
{
    public string Id { get; set; }

    [ObservableProperty]
    private string value;

    [ObservableProperty]
    private bool isActive;

    public Email(string id, string value, bool isActive)
    {
        Id = id;
        Value = value;
        IsActive = isActive;
    }
}
