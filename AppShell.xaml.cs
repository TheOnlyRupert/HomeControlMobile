using HomeControlMobile.Source;
using HomeControlMobile.Source.Modules;

namespace HomeControlMobile;

public partial class AppShell : Shell {
    public AppShell() {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(BehaviorPage), typeof(BehaviorPage));
    }
}