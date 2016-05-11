using Assets.src.model;
using ManusMachina;
using System.Collections;
using UnityEngine;

public class RightHandController : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Left or right hand.
    /// </summary>
    public GLOVE_HAND glove_hand;

    /// <summary>
    /// Manus Glove instance.
    /// </summary>
    public Glove glove;

    /// <summary>
    /// The root (wrist) of the hand.
    /// </summary>
    private GameObject root;

    /// <summary>
    /// Game object hand.
    /// </summary>
    private GameObject hand;

    /// <summary>
    /// Grab controller for the manus hand
    /// </summary>
    private ManusGrab manusGrab;

    #endregion Fields

    #region Methods

    // Use this for initialization
    private void Start()
    {
        Manus.ManusInit();
        glove = new Glove(glove_hand);

        this.hand = GameObject.Find("Manus_Handv2_Right");
        this.root = GameObject.Find("right_wrist");

        manusGrab = new ManusGrab(hand);

        Debug.Log(glove + "\t" + glove_hand);
    }

    // Update is called once per frame
    private void Update()
    {
        // doe hier checken op hand - item proximity.
        // grab dingen hier
        // detect vuist etc.

        // maar doen we 1 hand controller en dan veel if/else checken voor left/right
        // of 1 abstracte?
        // of 2 losse?

        updatePosition();
    }

    private void updatePosition()
    {
        Vector3 newpos = this.root.transform.position;
        this.hand.transform.position = newpos;
        this.hand.transform.rotation = this.root.transform.rotation;
        this.hand.transform.Rotate(Vector3.up, 90);
        this.hand.transform.Rotate(Vector3.forward, -90);
    }

    private void onApplicationQuit()
    {
        Manus.ManusExit();
    }

    #endregion Methods
}