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
            //a comment
        }

        //Assign to Team 2 Member
        private void ButtonCancelReservation(object sender, EventArgs e)
        {

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
