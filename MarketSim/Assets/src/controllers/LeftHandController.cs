using ManusMachina;
using System.Collections;
using UnityEngine;

public class LeftHandController : MonoBehaviour
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

    #endregion Fields

    #region Methods

    // Use this for initialization
    private void Start()
    {
        Manus.ManusInit();
        glove = new Glove(glove_hand);
        this.hand = GameObject.Find("Manus_Handv2_Left");
        this.root = GameObject.Find("left_wrist");

        Debug.Log(glove + "\t" + glove_hand);
    }

    // Update is called once per frame
    private void Update()
    {
        updatePosition();
    }

    private void updatePosition()
    {
        Vector3 newpos = this.root.transform.position;
        this.hand.transform.position = newpos;
        this.hand.transform.rotation = this.root.transform.rotation;
        this.hand.transform.Rotate(Vector3.up, -90);
        this.hand.transform.Rotate(Vector3.forward, -90);
    }

    private void onApplicationQuit()
    {
        Manus.ManusExit();
    }

    #endregion Methods
}