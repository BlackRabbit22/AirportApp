using System;
using System.Windows;
using System.Windows.Controls;

namespace AirportApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private ControlTower tower;

    public MainWindow()
    {
        InitializeComponent();
        tower = new(flights_ListView);
        tower.AltitudeChanged += OnAltitudeChange;
    }


    /// <summary>
    /// Updates GUI fields.
    /// </summary>
    public void UpdateGUI()
    {
        int? i = null;
        name_TextBox.Text = "";
        flightID_TextBox.Text = "";
        description_TextBox.Text = "";
        flightTime_TextBox.Text = "";
        planes_ListBox.ItemsSource = tower.Flights.GetItemList();
        if (planes_ListBox.SelectedIndex != -1)
            altitudeDisplay_Label.Content = tower.Flights.GetItem(planes_ListBox.SelectedIndex).Altitude;
    }

    /// <summary>
    /// Orders selected plane to take off.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void takeOff_Button_Click(object sender, RoutedEventArgs e)
    {
        int index = planes_ListBox.SelectedIndex;
        if (tower.OrderTakeOff(index))
        {
            MessageBox.Show("Plane already in flight!");
        }

        UpdateGUI();
    }

    /// <summary>
    /// Adds a plane to the listBox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void addPlane_Button_Click(object sender, RoutedEventArgs e)
    {
        if (name_TextBox.Text == "" || flightTime_TextBox.Text == "" || description_TextBox.Text == "")
        {
            MessageBox.Show("Input fields cannot be empty.");
            return;
        }

        Airplane airplane = new Airplane();
        airplane.Name = name_TextBox.Text;
        airplane.FlightID = flightID_TextBox.Text;
        airplane.Destination = description_TextBox.Text;
        if (double.TryParse(flightTime_TextBox.Text, out double flightTime))
        {
            airplane.FlightTime = flightTime;
            tower.AddPlane(airplane);
            UpdateGUI();
        }
        else
        {
            MessageBox.Show("Not a valid time");
        }
    }

    /// <summary>
    /// Updates GUI when event fired.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnAltitudeChange(object sender, EventArgs e)
    {
        if (planes_ListBox.SelectedIndex != -1)
        {
            Airplane plane = tower.Flights.GetItem(planes_ListBox.SelectedIndex);
            flights_ListView.Items.Add(new
            {
                Name = plane.Name,
                Status = $"altitude changed to: {plane.Altitude}"
            });
        }

        UpdateGUI();
    }

    /// <summary>
    /// Change altitude for selected plane.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void changeAltitude_Button_Click(object sender, RoutedEventArgs e)
    {
        int index = planes_ListBox.SelectedIndex;
        if (double.TryParse(altitude_TextBox.Text, out double altitude))
        {
            tower.ChangeAltitude(index, altitude);
        }
    }

    /// <summary>
    /// Update altitude information when selecting a plane.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void planes_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateGUI();
    }
}