using HomeControlMobile.Source.ViewModel;

namespace HomeControlMobile.Source.Modules;

public partial class BehaviorPage : ContentPage {
    public BehaviorPage() {
        InitializeComponent();
        BindingContext = new BehaviorPageVm();
    }
}