using System.Collections;
using UnityEngine;

/**
 * The manager keeps track of global variables, settings.
 * Should be singleton as there is only 1 manager for the simulation.
 **/

public class Manager : MonoBehaviour
{
    #region Fields

    public bool NO_VR = true;

    #endregion Fields

    #region Properties

    public static Manager Instance { get; private set; }

    #endregion Properties

    #region Methods

    /// <summary>
    ///  Initialize instance
    /// </summary>
    private void Awake()
    {
        Instance = this;
        Debug.Log("I'm alive");
    }

    /// <summary>
    ///  Use this for initialization
    /// </summary>
    private void Start()
    {
    }

    /// <summary>
    ///  Update is called once per frame
    /// </summary>
    private void Update()
    {
    }

    #endregion Methods
}