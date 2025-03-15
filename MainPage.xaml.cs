using System.Collections.Specialized;
using System.Reflection.Metadata;

namespace ExampleGitHubSetup
{
    public class SeatingUnit
    {
        public string Name { get; set; }
        public bool Reserved { get; set; }

        public SeatingUnit(string name, bool reserved = false)
        {
            Name = name;
            Reserved = reserved;
        }

    }

    public partial class MainPage : ContentPage
    {
        SeatingUnit[,] seatingChart = new SeatingUnit[5, 10];

        public MainPage()
        {
            InitializeComponent();
            GenerateSeatingNames();
            RefreshSeating();
        }

        private async void ButtonReserveSeat(object sender, EventArgs e)
        {
            var seat = await DisplayPromptAsync("Enter Seat Number", "Enter seat number: ");

            if (seat != null)
            {
                for (int i = 0; i < seatingChart.GetLength(0); i++)
                {
                    for (int j = 0; j < seatingChart.GetLength(1); j++)
                    {
                        if (seatingChart[i, j].Name == seat)
                        {
                            seatingChart[i, j].Reserved = true;
                            await DisplayAlert("Successfully Reserverd", "Your seat was reserverd successfully!", "Ok");
                            RefreshSeating();
                            return;
                        }
                    }
                }

                await DisplayAlert("Error", "Seat was not found.", "Ok");
            }
        }

        private void GenerateSeatingNames()
        {
            List<string> letters = new List<string>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                letters.Add(c.ToString());
            }

            int letterIndex = 0;

            for (int row = 0; row < seatingChart.GetLength(0); row++)
            {
                for (int column = 0; column < seatingChart.GetLength(1); column++)
                {
                    seatingChart[row, column] = new SeatingUnit(letters[letterIndex] + (column + 1).ToString());
                }

                letterIndex++;
            }
        }

        private void RefreshSeating()
        {
            grdSeatingView.RowDefinitions.Clear();
            grdSeatingView.ColumnDefinitions.Clear();
            grdSeatingView.Children.Clear();

            for (int row = 0; row < seatingChart.GetLength(0); row++)
            {
                var grdRow = new RowDefinition();
                grdRow.Height = 50;

                grdSeatingView.RowDefinitions.Add(grdRow);

                for (int column = 0; column < seatingChart.GetLength(1); column++)
                {
                    var grdColumn = new ColumnDefinition();
                    grdColumn.Width = 50;

                    grdSeatingView.ColumnDefinitions.Add(grdColumn);

                    var text = seatingChart[row, column].Name;

                    var seatLabel = new Label();
                    seatLabel.Text = text;
                    seatLabel.HorizontalOptions = LayoutOptions.Center;
                    seatLabel.VerticalOptions = LayoutOptions.Center;
                    seatLabel.BackgroundColor = Color.Parse("#333388");
                    seatLabel.Padding = 10;

                    if (seatingChart[row, column].Reserved == true)
                    {
                        //Change the color of this seat to represent its reserved.
                        seatLabel.BackgroundColor = Color.Parse("#883333");

                    }

                    Grid.SetRow(seatLabel, row);
                    Grid.SetColumn(seatLabel, column);
                    grdSeatingView.Children.Add(seatLabel);

                }
            }
        }

        //Assign to Team 1 Member
        private void ButtonReserveRange(object sender, EventArgs e)
        {
            
    var input = await DisplayPromptAsync("Reserve Seat Range", "Enter seat range (e.g., A1:A4):");
    if (string.IsNullOrWhiteSpace(input))
        return;

    string[] parts = input.Split(':');
    if (parts.Length != 2)
    {
        await DisplayAlert("Error", "Invalid format! Use A1:A4.", "OK");
        return;
    }

    string startSeat = parts[0];
    string endSeat = parts[1];

    // Extract row and columns with error handling
    if (!char.IsLetter(startSeat[0]) || !char.IsLetter(endSeat[0]) || 
        startSeat.Length < 2 || endSeat.Length < 2 || 
        !int.TryParse(startSeat.Substring(1), out int startCol) || 
        !int.TryParse(endSeat.Substring(1), out int endCol))
    {
        await DisplayAlert("Error", "Invalid seat format! Use A1:A4.", "OK");
        return;
    }

    char startRow = char.ToUpper(startSeat[0]);
    char endRow = char.ToUpper(endSeat[0]);

    // Validation
    if (startRow != endRow || startCol < 1 || endCol < 1 || 
        startCol > 10 || endCol > 10 || startCol > endCol)
    {
        await DisplayAlert("Error", "Invalid seat range! Must be same row, between 1-10, and start <= end.", "OK");
        return;
    }

    // Get row index (A=0, B=1, etc.)
    int rowIndex = startRow - 'A';
    if (rowIndex < 0 || rowIndex >= seatingChart.GetLength(0))
    {
        await DisplayAlert("Error", "Invalid row! Must be A-E.", "OK");
        return;
        }

        //Assign to Team 2 Member (Ryan Mitchell)
        private void ButtonCancelReservation(object sender, EventArgs e)
        {
            var seat = await DisplayPromptAsync("Cancel Reservation","Enter a seat number ");
            if (string.IsNullOrEmpty(seat)) return;
            {
                await DisplayAlert("Error", "Invalid format!", "OK");
                return;
            if (!seatingChart[row,col].Reserved)
            {
                await DisplayAlert("Error", "This seat is not reserved","OK");
                return;
            }
            seatingChart[row,col].Reserved = false;
            await DisplayAlert ("Success",$"Reservation for {seat} has been canceled","OK");
            RefreshSeating();

            }
        }

        //Assign to Team 3 Member
        private void ButtonCancelReservationRange(object sender, EventArgs e)
        {

        }

        //Assign to Team 4 Member (Kristian Day)
        private async void ButtonResetSeatingChart(object sender, EventArgs e)
        {
            // Ask user for input
            var input = await DisplayPromptAsync("Reset Seating Chart", "Type YES to confirm reset: ");

            // Check if user entered "YES"
            if (input != null && input.ToUpper() == "YES")
            {
                // Loop all seats and set them to available
                for (int row = 0; row < seatingChart.GetLength(0); row++)
                {
                    for (int col = 0; col < seatingChart.GetLength(1); col++)
                    {
                        // Update Seating Chart
                        seatingChart[row, col].Reserved = false;
                    }
                }

                // Show a success message
                await DisplayAlert("Success", "All seats have been reset.", "Ok");

                // Update the seating chart
                RefreshingSeating();
            }
            else
            {
                // Show Error Message if input is not "YES"
                await DisplayAlert("Error", "Reset Failed. Type 'YES' to confirm.", "Ok");
            
            }
        }
    }

}
