using System;
using System.Windows.Threading;

namespace AirportApp;

/// <summary>
/// Airplane class.
/// </summary>
public class Airplane
{
    private DispatcherTimer dispatcherTimer;
    private string originDestination;

    /// <summary>
    /// Landed event.
    /// </summary>
    public event EventHandler<AirplaneEventArgs> Landed;

    /// <summary>
    /// TakeOff event.
    /// </summary>
    public event EventHandler<AirplaneEventArgs> TakeOff;

    public string Name { get; set; }
    public string FlightID { get; set; }
    public string Destination { get; set; }
    public double FlightTime { get; set; }
    public bool CanLand { get; set; }

    /// <summary>
    /// Returns the altitude of the plane
    /// </summary>
    public double Altitude { get; set; }


    /// <summary>
    /// Performs actions on each DispatcherTimer_Tick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        StopTimer();
        CanLand = false;
        OnLanding();
    }

    /// <summary>
    /// Set the altitude  and fire landed event.
    /// </summary>
    public void OnLanding()
    {
        if (Landed != null)
        {
            Altitude = 0;
            Landed(this, new AirplaneEventArgs(Name, FlightInfo_Landing()));
            // Check if destination is home or not
            if (Destination == "Home")
            {
                Destination = originDestination;
            }
            else
            {
                originDestination = Destination;
                Destination = "Home";
            }
        }
    }

    /// <summary>
    /// Set altitude and fire takeoff event.
    /// </summary>
    public void OnTakeOff()
    {
        Altitude = 9000;
        if (TakeOff != null)
        {
            TakeOff(this, new AirplaneEventArgs(Name, FlightInfo_TakeOff()));
            SetupTimer();
            CanLand = true;
        }
    }

    /// <summary>
    /// Setup the timer.
    /// </summary>
    public void SetupTimer()
    {
        dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
        dispatcherTimer.Interval = TimeSpan.FromSeconds(Math.Round(FlightTime, 2));
        dispatcherTimer.Start();
    }

    /// <summary>
    /// Stop the timer.
    /// </summary>
    public void StopTimer()
    {
        dispatcherTimer.Stop();
    }

    /// <summary>
    /// Converts properties into a string.
    /// </summary>
    /// <returns>Properties into a string format.</returns>
    public override string ToString()
    {
        string strOut = Name + ", " + FlightID + ", heading for " + Destination + ", time: " +
                        FlightTime.ToString("F2");
        return strOut;
    }

    /// <summary>
    /// Returns the information of the plane on take off.
    /// </summary>
    /// <returns>Information of the plane on take off.</returns>
    public string FlightInfo_TakeOff()
    {
        string strOut = "is taking off, destination for " + Destination + ", " +
                        DateTime.Now.ToString("hh:mm:ss") + "!";
        return strOut;
    }

    /// <summary>
    /// Return the information of the plane on landing.
    /// </summary>
    /// <returns>Information of the plane on landing.</returns>
    public string FlightInfo_Landing()
    {
        string strOut = "has landed in " + Destination + ", " + DateTime.Now.ToString("hh:mm:ss") + "!";
        return strOut;
    }

    /// <summary>
    /// Change the altitude of a plane.
    /// </summary>
    /// <param name="altitude"></param>
    public void ChangeAltitude(double altitude)
    {
        Altitude = altitude;
    }
}