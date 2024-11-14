using HomeControlMobile.Source.ViewModel;

namespace HomeControlMobile.Source.Modules;

public partial class SettingsPage : ContentPage {
    public SettingsPage() {
        InitializeComponent();
        BindingContext = new SettingsPageVm();
    }
}