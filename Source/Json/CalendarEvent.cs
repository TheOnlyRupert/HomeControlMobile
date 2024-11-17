using System.Collections.ObjectModel;

namespace HomeControlMobile.Source.Json;

public class CalendarEvent {
    public int Id { get; set; }
    public DateTime EventDate { get; set; }
    public string Title { get; set; }
    public Color ForegroundColor { get; set; }
}

public class VersionInfo {
    public int Id { get; set; }
    public int Version { get; set; }
}

public class CalendarDay {
    public DateTime Date { get; set; }
    public bool IsCurrentMonth { get; set; }
    public ObservableCollection<CalendarEvent> Events { get; set; } = new();
}