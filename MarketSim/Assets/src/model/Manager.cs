using System;
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

    /// <summary>
    /// The mouse keyboard only configuration.
    /// </summary>
    private static bool mKbOnly;

    /// <summary>
    /// The throw force
    /// </summary>
    private static float throwForce;

    /// <summary>
    /// The proximity dist
    /// </summary>
    private static float proximityDist;

    /// <summary>
    /// The configuration location
    /// </summary>
    private readonly string configLocation = "Assets\\src\\config.gryffindor";

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

    /// <summary>
    /// Gets a value indicating whether [MKB only].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [MKB only]; otherwise, <c>false</c>.
    /// </value>
    public static bool MKBOnly
    {
        get
        {
            return Manager.mKbOnly;
        }
    }

    /// <summary>
    /// Gets the throw force.
    /// </summary>
    /// <value>
    /// The throw force.
    /// </value>
    public static float ThrowForce
    {
        get
        {
            return Manager.throwForce;
        }
    }

    /// <summary>
    /// Gets the proximity dist.
    /// </summary>
    /// <value>
    /// The proximity dist.
    /// </value>
    public static float ProximityDist
    {
        get
        {
            return Manager.proximityDist;
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
        Manager.mKbOnly = true;

        /// Sets default values to prevent null values.
        Manager.highlightOn = false;
        Manager.gestureMovementOn = false;
        Manager.mKbOnly = false;
        Manager.throwForce = 1500f;
        Manager.proximityDist = 2.0f;

        readConfig();
    }

    /// <summary>
    /// Reads the configuration.
    /// </summary>
    private void readConfig()
    {
        string[] lines = System.IO.File.ReadAllLines(@configLocation);

        foreach (string line in lines)
        {
            ConfigInitialize(line);
        }
        Debug.Log("Config Loaded.");
    }

    /// <summary>
    /// configuration of filtered, split and delim.
    /// </summary>
    /// <param name="line">The line.</param>
    public void ConfigInitialize(string line)
    {
        string filtered = line.ToUpper().Replace(" ", string.Empty);
        filtered = filtered.Replace("\t", string.Empty);
        StringComparison comparison = StringComparison.InvariantCulture;
        char[] delim = { '=' };
        string[] split = filtered.Split(delim);
        if (!filtered.StartsWith("#", comparison) && split.Length == 2)
        {
            ApplyConfigs(split);
        }
    }

    /// <summary>
    /// Applies the configs.
    /// </summary>
    /// <param name="split">The split.</param>
    public void ApplyConfigs(String[] split)
    {
        switch (split[0])
        {
            case "HIGHLIGHTON":
                Manager.highlightOn = (split[1] == "TRUE"); break;
            case "GESTUREMOVEMENTON":
                Manager.gestureMovementOn = (split[1] == "TRUE"); break;
            case "MKBONLY":
                Manager.mKbOnly = (split[1] == "TRUE"); break;
            case "PROXDIST":
                Manager.proximityDist = float.Parse(split[1]); break;
            case "THROWFORCE":
                Manager.throwForce = float.Parse(split[1]); break;
        }
    }

    #endregion Methods
}