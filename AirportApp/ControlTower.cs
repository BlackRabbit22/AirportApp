using System;
using System.Diagnostics;
using AirportApp.CommonFiles;
using System.Windows.Controls;

namespace AirportApp;

/// <summary>
/// ControlTower class that manages <see cref="Airplane"/> objects.
/// </summary>
public class ControlTower
{
    private ListManager<Airplane> flights;
    private ListView listView;

    /// <summary>
    /// Altitude delegate.
    /// </summary>
    public delegate void AltitudeEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Altitude event handler.
    /// </summary>
    public event AltitudeEventHandler AltitudeChanged;

    /// <summary>
    /// Constructor for the ControlTower. Initializes a <see cref="ListManager{Airplane}"/> object.
    /// </summary>
    /// <param name="listView"></param>
    public ControlTower(ListView listView)
    {
        flights = new();
        this.listView = listView;
    }

    /// <summary>
    /// Returns a <see cref="ListManager{Airplane}"/> object.
    /// </summary>
    public ListManager<Airplane> Flights
    {
        get { return flights; }
    }

    /// <summary>
    /// Adds a <see cref="Airplane"/> to the <see cref="ListManager{Airplane}"/> object.
    /// </summary>
    /// <param name="airplane"></param>
    public void AddPlane(Airplane airplane)
    {
        flights.Add(airplane);
        string message = $", {airplane.FlightID}, heading for {airplane.Destination} sent to runway!";
        OnDisplayInfo(this, new AirplaneEventArgs(airplane.Name, message));
        airplane.TakeOff += OnDisplayInfo;
        airplane.Landed += OnDisplayInfo;
    }

    /// <summary>
    /// Adds to the listView information about the current flight.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnDisplayInfo(object sender, AirplaneEventArgs e)
    {
        OnAltitudeChanged();
        listView.Items.Add(new { Name = e.Flight, Status = e.Message });
    }

    /// <summary>
    /// Invokes AltitudeEventHandler.
    /// </summary>
    public virtual void OnAltitudeChanged()
    {
        if (AltitudeChanged != null)
        {
            AltitudeChanged(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Changes the altitude of a <see cref="Airplane"/>.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    public void ChangeAltitude(int index, double altitude)
    {
        Airplane plane = flights.GetItem(index);
        plane.Altitude = altitude;
        OnAltitudeChanged();
    }

    /// <summary>
    /// Orders an <see cref="Airplane"/> to takeoff.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool OrderTakeOff(int index)
    {
        Airplane plane = flights.GetItem(index);
        if (!plane.CanLand)
        {
            plane.OnTakeOff();
            return !plane.CanLand;
        }
        else
        {
            return plane.CanLand;
        }
    }
}