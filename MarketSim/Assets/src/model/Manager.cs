using System.Collections;
using UnityEngine;

/// <summary>
/// The manager keeps track of global variables, settings.
/// Should be singleton as there is only 1 manager for the simulation.
/// </summary>
public class Manager : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Highlight configuration.
    /// </summary>
    private static bool highlightOn;

    /// <summary>
    /// Moving with specific gestures configuration.
    /// </summary>
    private static bool gestureMovementOn;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Gets the Manager instance.
    /// </summary>
    /// <value>
    /// The Manager instance.
    /// </value>
    public static Manager Instance { get; private set; }

    /// <summary>
    /// Gets a value indicating whether to highlight or not.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [highlighting enabled]; otherwise, <c>false</c>.
    /// </value>
    public static bool HighlightOn
    {
        get
        {
            return Manager.highlightOn;
        }
    }

    /// <summary>
    /// Gets a value indicating whether gesture movement is enabled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [gesture movement enabled]; otherwise, <c>false</c>.
    /// </value>
    public static bool GestureMovementOn
    {
        get
        {
            return Manager.gestureMovementOn;
        }
    }

    #endregion Properties

    #region Methods

    /// <summary>
    ///  Initialize Manager instance and configuration.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        Manager.highlightOn = true;
        Manager.gestureMovementOn = true;

        Debug.Log("I'm alive");
    }

    #endregion Methods
}