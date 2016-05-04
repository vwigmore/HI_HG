using UnityEngine;
using System.Collections;

/**
 * The manager keeps track of global variables, settings.
 * Should be singleton as there is only 1 manager for the simulation.
 **/
public class Manager : MonoBehaviour {

    public bool NO_VR = true;

    public static Manager Instance { get; private set; }

    // Initialize instance
    void Awake()
    {
        Instance = this;
        Debug.Log("I'm alive");

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
