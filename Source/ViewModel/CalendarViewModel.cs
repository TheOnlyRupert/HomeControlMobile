using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using HomeControlMobile.Source.Json;
using HomeControlMobile.Source.ViewModel;
using MySql.Data.MySqlClient;

public class CalendarViewModel : BindableObject {
    private DateTime _currentMonth;
    private ObservableCollection<CalendarEvent> _events;

    public CalendarViewModel() {
        CurrentMonth = DateTime.Now;
        LoadCalendarDays();

        PreviousMonthCommand = new Command(GoToPreviousMonth);
        NextMonthCommand = new Command(GoToNextMonth);
    }

    public DateTime CurrentMonth {
        get => _currentMonth;
        set {
            if (_currentMonth != value) {
                _currentMonth = value;
                OnPropertyChanged();
                LoadCalendarDays(); // Reload the calendar days when the month changes
            }
        }
    }

    public ObservableCollection<CalendarDay> CalendarDays { get; set; } = [];

    public ObservableCollection<CalendarEvent> Events {
        get => _events;
        set {
            _events = value;
            OnPropertyChanged();
        }
    }

    public Command PreviousMonthCommand { get; }
    public Command NextMonthCommand { get; }

    private async void LoadCalendarDays() {
        // Create a temporary collection of calendar days
        ObservableCollection<CalendarDay> calendarDays = new();

        // Calculate the first day of the month and the number of days in the current month
        DateTime firstDayOfMonth = new(CurrentMonth.Year, CurrentMonth.Month, 1);
        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        // Get the day of the week for the first day of the month
        int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

        // Calculate the number of previous month's days to show
        int previousMonthDays = startDayOfWeek;

        // Calculate the number of cells needed to fill the calendar
        int totalDaysInMonth = lastDayOfMonth.Day;
        const int totalCells = 42;
        int daysToFill = totalCells - (previousMonthDays + totalDaysInMonth);

        // Add previous month's days (if any)
        DateTime previousMonth = firstDayOfMonth.AddMonths(-1);
        for (int i = previousMonthDays - 1; i >= 0; i--) {
            calendarDays.Add(new CalendarDay {
                Date = new DateTime(previousMonth.Year, previousMonth.Month, previousMonthDays - i),
                IsCurrentMonth = false
            });
        }

        // Add current month's days
        for (int i = 1; i <= totalDaysInMonth; i++) {
            calendarDays.Add(new CalendarDay {
                Date = new DateTime(CurrentMonth.Year, CurrentMonth.Month, i),
                IsCurrentMonth = true
            });
        }

        // Add next month's days (if any)
        DateTime nextMonth = firstDayOfMonth.AddMonths(1);
        for (int i = 1; i <= daysToFill; i++) {
            calendarDays.Add(new CalendarDay {
                Date = new DateTime(nextMonth.Year, nextMonth.Month, i),
                IsCurrentMonth = false
            });
        }

        // Update the CalendarDays ObservableCollection without resetting it entirely
        CalendarDays.Clear(); // Clear only the existing items
        foreach (CalendarDay day in calendarDays) {
            CalendarDays.Add(day);
        }

        // Fetch events for the calendar (same as original code)
        await FetchEvents(firstDayOfMonth, lastDayOfMonth, calendarDays);

        CalendarDays[0].Events.Add(new CalendarEvent {
            Id = 2,
            EventDate = DateTime.Today,
            Title = "Test",
            ForegroundColor = Color.FromRgb(255, 255, 255)
        });
    }

    private async Task FetchEvents(DateTime firstDayOfMonth, DateTime lastDayOfMonth, ObservableCollection<CalendarDay> calendarDays) {
        MySqlConnection connection = new(@"Server=" + Preferences.Get(nameof(SettingsPageVm.DatabaseHost), "null") + @";Database=HomeControl;Uid=" +
                                         Preferences.Get(nameof(SettingsPageVm.DatabaseUsername), "null") + @";Pwd=" + Preferences.Get(nameof(SettingsPageVm.DatabasePassword), "null"));

        try {
            await connection.OpenAsync();

            MySqlCommand command = new("SELECT * FROM events WHERE date BETWEEN @start AND @end", connection);
            command.Parameters.AddWithValue("@start", firstDayOfMonth);
            command.Parameters.AddWithValue("@end", lastDayOfMonth);
            await using DbDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync()) {
                DateTime eventDate = reader.GetDateTime("EventDate");
                CalendarEvent calendarEvent = new() {
                    Id = reader.GetInt32("Id"),
                    EventDate = eventDate,
                    Title = reader.GetString("Title"),
                    ForegroundColor = Color.FromArgb(reader.GetString("ForegroundColor"))
                };

                CalendarDay? matchingDay = calendarDays.FirstOrDefault(d => d.Date.Date == eventDate.Date);
                matchingDay?.Events.Add(calendarEvent);
            }
        } catch (Exception ex) {
            Console.WriteLine(ex);
        }
    }

    // Command handlers for navigating between months
    private void GoToPreviousMonth() {
        CurrentMonth = CurrentMonth.AddMonths(-1); // This triggers LoadCalendarDays
    }

    private void GoToNextMonth() {
        CurrentMonth = CurrentMonth.AddMonths(1); // This triggers LoadCalendarDays
    }
}