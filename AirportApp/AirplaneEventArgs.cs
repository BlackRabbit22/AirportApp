using System;

namespace AirportApp;

/// <summary>
/// AirplaneEventArgs class.
/// </summary>
public class AirplaneEventArgs : EventArgs
{
    private string message;
    private string name;

    /// <summary>
    /// Constructor for AirplaneEventArgs that is used for <see cref="Airplane"/> events.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="message"></param>
    public AirplaneEventArgs(string name, string message)
    {
        this.name = name;
        this.message = message;
    }

    /// <summary>
    /// Gets/sets Flight name.
    /// </summary>
    public string Flight
    {
        get { return name; }
        set { name = value; }
    }

    /// <summary>
    /// Gets/sets Flight information.
    /// </summary>
    public string Message
    {
        get { return message; }
        set { message = value; }
    }
}