using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeControlMobile.Source.ViewModel;

public class SettingsPageVm : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #region Fields
    
    public bool IsLocallyHosted
    {
        get => Preferences.Get(nameof(IsLocallyHosted), true);
        set { Preferences.Set(nameof(IsLocallyHosted), value); OnPropertyChanged(); }
    }
    
    public bool IsDatabaseHosted
    {
        get => Preferences.Get(nameof(IsDatabaseHosted), false);
        set { Preferences.Set(nameof(IsDatabaseHosted), value); OnPropertyChanged(); }
    }
    
    public string DatabaseHost
    {
        get => Preferences.Get(nameof(DatabaseHost), "");
        set { Preferences.Set(nameof(DatabaseHost), value); OnPropertyChanged(); }
    }
    
    public string DatabaseUsername
    {
        get => Preferences.Get(nameof(DatabaseUsername), "");
        set { Preferences.Set(nameof(DatabaseUsername), value); OnPropertyChanged(); }
    }

    public string DatabasePassword
    {
        get => Preferences.Get(nameof(DatabasePassword), "");
        set { Preferences.Set(nameof(DatabasePassword), value); OnPropertyChanged(); }
    }
    
    public string User1Name
    {
        get => Preferences.Get(nameof(User1Name), "");
        set { Preferences.Set(nameof(User1Name), value); OnPropertyChanged(); }
    }
    
    public string User2Name
    {
        get => Preferences.Get(nameof(User2Name), "");
        set { Preferences.Set(nameof(User2Name), value); OnPropertyChanged(); }
    }
    
    public string User3Name
    {
        get => Preferences.Get(nameof(User3Name), "");
        set { Preferences.Set(nameof(User3Name), value); OnPropertyChanged(); }
    }
    
    public string User4Name
    {
        get => Preferences.Get(nameof(User4Name), "");
        set { Preferences.Set(nameof(User4Name), value); OnPropertyChanged(); }
    }
    
    public string User5Name
    {
        get => Preferences.Get(nameof(User5Name), "");
        set { Preferences.Set(nameof(User5Name), value); OnPropertyChanged(); }
    }

    #endregion
}