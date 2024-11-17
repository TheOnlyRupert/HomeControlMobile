namespace HomeControlMobile.Source.Modules;

public partial class CalendarPage : ContentPage {
    public CalendarPage() {
        InitializeComponent();
        BindingContext = new CalendarViewModel();
    }
}