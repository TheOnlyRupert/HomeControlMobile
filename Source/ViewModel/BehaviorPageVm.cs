using System.ComponentModel;
using System.Windows.Input;
using MySql.Data.MySqlClient;

namespace HomeControlMobile.Source.ViewModel;

public class BehaviorPageVm : INotifyPropertyChanged {
    public BehaviorPageVm() {
        MySqlConnection connection = new(@"Server=" + Preferences.Get(nameof(SettingsPageVm.DatabaseHost), "null") + @";Database=HomeControl;Uid=" +
                                         Preferences.Get(nameof(SettingsPageVm.DatabaseUsername), "null") + @";Pwd=" + Preferences.Get(nameof(SettingsPageVm.DatabasePassword), "null"));

        try {
            connection.Open();

            const string query = "SELECT id, stars, strikes FROM behavior";
            using MySqlCommand command = new(query, connection);
            using MySqlDataReader? reader = command.ExecuteReader();

            while (reader.Read()) {
                int userId = reader.GetInt32("id");
                int stars = reader.GetInt32("stars");
                int strikes = reader.GetInt32("strikes");

                // Assign values based on UserId
                switch (userId) {
                case 1:
                    User1Stars = stars;
                    User1Strikes = strikes;
                    break;
                case 2:
                    User2Stars = stars;
                    User2Strikes = strikes;
                    break;
                case 3:
                    User3Stars = stars;
                    User3Strikes = strikes;
                    break;
                case 4:
                    User4Stars = stars;
                    User4Strikes = strikes;
                    break;
                case 5:
                    User5Stars = stars;
                    User5Strikes = strikes;
                    break;
                }
            }
        } catch (Exception ex) {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        connection.Close();

        OnPropertyChanged(nameof(User1Stars));
        OnPropertyChanged(nameof(User1Strikes));
        OnPropertyChanged(nameof(User2Stars));
        OnPropertyChanged(nameof(User2Strikes));
        OnPropertyChanged(nameof(User3Stars));
        OnPropertyChanged(nameof(User3Strikes));
        OnPropertyChanged(nameof(User4Stars));
        OnPropertyChanged(nameof(User4Strikes));
        OnPropertyChanged(nameof(User5Stars));
        OnPropertyChanged(nameof(User5Strikes));

        IncreaseStarsCommand = new Command<string>(IncreaseStars);
        DecreaseStarsCommand = new Command<string>(DecreaseStars);
        IncreaseStrikesCommand = new Command<string>(IncreaseStrikes);
        DecreaseStrikesCommand = new Command<string>(DecreaseStrikes);
        SaveCommand = new Command(SaveData);
    }

    public int User1Stars { get; set; }
    public int User1Strikes { get; set; }
    public int User2Stars { get; set; }
    public int User2Strikes { get; set; }
    public int User3Stars { get; set; }
    public int User3Strikes { get; set; }
    public int User4Stars { get; set; }
    public int User4Strikes { get; set; }
    public int User5Stars { get; set; }
    public int User5Strikes { get; set; }
    public ICommand IncreaseStarsCommand { get; }
    public ICommand DecreaseStarsCommand { get; }
    public ICommand IncreaseStrikesCommand { get; }
    public ICommand DecreaseStrikesCommand { get; }
    public ICommand SaveCommand { get; }
    public event PropertyChangedEventHandler? PropertyChanged;

    public void SaveData() {
        MySqlConnection connection = new(@"Server=" + Preferences.Get(nameof(SettingsPageVm.DatabaseHost), "null") + @";Database=HomeControl;Uid=" +
                                         Preferences.Get(nameof(SettingsPageVm.DatabaseUsername), "null") + @";Pwd=" + Preferences.Get(nameof(SettingsPageVm.DatabasePassword), "null"));

        try {
            connection.Open();

            using (MySqlTransaction? transaction = connection.BeginTransaction()) {
                const string updateUserQuery = "UPDATE behavior SET stars = @stars, strikes = @strikes WHERE id = @userId";

                using (MySqlCommand command = new(updateUserQuery, connection, transaction)) {
                    command.Parameters.Add("@stars", MySqlDbType.Int32);
                    command.Parameters.Add("@strikes", MySqlDbType.Int32);
                    command.Parameters.Add("@userId", MySqlDbType.Int32);

                    command.Parameters["@stars"].Value = User1Stars;
                    command.Parameters["@strikes"].Value = User1Strikes;
                    command.Parameters["@userId"].Value = 1;
                    command.ExecuteNonQuery();

                    command.Parameters["@stars"].Value = User2Stars;
                    command.Parameters["@strikes"].Value = User2Strikes;
                    command.Parameters["@userId"].Value = 2;
                    command.ExecuteNonQuery();

                    command.Parameters["@stars"].Value = User3Stars;
                    command.Parameters["@strikes"].Value = User3Strikes;
                    command.Parameters["@userId"].Value = 3;
                    command.ExecuteNonQuery();

                    command.Parameters["@stars"].Value = User4Stars;
                    command.Parameters["@strikes"].Value = User4Strikes;
                    command.Parameters["@userId"].Value = 4;
                    command.ExecuteNonQuery();

                    command.Parameters["@stars"].Value = User5Stars;
                    command.Parameters["@strikes"].Value = User5Strikes;
                    command.Parameters["@userId"].Value = 5;
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        } catch (Exception ex) {
            Console.WriteLine($"An error occurred while saving data: {ex.Message}");
        }
    }

    private void IncreaseStars(string user) {
        switch (user) {
        case "User1" when User1Stars < 5:
            User1Stars++;
            break;
        case "User2" when User2Stars < 5:
            User2Stars++;
            break;
        case "User3" when User3Stars < 5:
            User3Stars++;
            break;
        case "User4" when User4Stars < 5:
            User4Stars++;
            break;
        case "User5" when User5Stars < 5:
            User5Stars++;
            break;
        }

        OnPropertyChanged(nameof(User1Stars));
        OnPropertyChanged(nameof(User2Stars));
        OnPropertyChanged(nameof(User3Stars));
        OnPropertyChanged(nameof(User4Stars));
        OnPropertyChanged(nameof(User5Stars));
    }

    private void DecreaseStars(string user) {
        switch (user) {
        case "User1" when User1Stars > 0:
            User1Stars--;
            break;
        case "User2" when User2Stars > 0:
            User2Stars--;
            break;
        case "User3" when User3Stars > 0:
            User3Stars--;
            break;
        case "User4" when User4Stars > 0:
            User4Stars--;
            break;
        case "User5" when User5Stars > 0:
            User5Stars--;
            break;
        }

        OnPropertyChanged(nameof(User1Stars));
        OnPropertyChanged(nameof(User2Stars));
        OnPropertyChanged(nameof(User3Stars));
        OnPropertyChanged(nameof(User4Stars));
        OnPropertyChanged(nameof(User5Stars));
    }

    private void IncreaseStrikes(string user) {
        switch (user) {
        case "User1" when User1Strikes < 3:
            User1Strikes++;
            break;
        case "User2" when User2Strikes < 3:
            User2Strikes++;
            break;
        case "User3" when User3Strikes < 3:
            User3Strikes++;
            break;
        case "User4" when User4Strikes < 3:
            User4Strikes++;
            break;
        case "User5" when User5Strikes < 3:
            User5Strikes++;
            break;
        }

        OnPropertyChanged(nameof(User1Strikes));
        OnPropertyChanged(nameof(User2Strikes));
        OnPropertyChanged(nameof(User3Strikes));
        OnPropertyChanged(nameof(User4Strikes));
        OnPropertyChanged(nameof(User5Strikes));
    }

    private void DecreaseStrikes(string user) {
        switch (user) {
        case "User1" when User1Strikes > 0:
            User1Strikes--;
            break;
        case "User2" when User2Strikes > 0:
            User2Strikes--;
            break;
        case "User3" when User3Strikes > 0:
            User3Strikes--;
            break;
        case "User4" when User4Strikes > 0:
            User4Strikes--;
            break;
        case "User5" when User5Strikes > 0:
            User5Strikes--;
            break;
        }

        OnPropertyChanged(nameof(User1Strikes));
        OnPropertyChanged(nameof(User2Strikes));
        OnPropertyChanged(nameof(User3Strikes));
        OnPropertyChanged(nameof(User4Strikes));
        OnPropertyChanged(nameof(User5Strikes));
    }

    protected virtual void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}