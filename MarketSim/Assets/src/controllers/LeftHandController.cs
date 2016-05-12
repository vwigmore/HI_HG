using ManusMachina;
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

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        Manus.ManusInit();
        this.glove = new Glove(this.glove_hand);
        this.hand = GameObject.Find("Manus_Handv2_Left");
        this.root = GameObject.Find("left_wrist");

        Debug.Log(this.glove + "\t" + this.glove_hand);
    }

    /// <summary>
    ///  Update is called once per frame.
    /// </summary>
    private void Update()
    {
        this.UpdatePosition();
    }

    /// <summary>
    /// Checks whether the hand is a fist.
    /// </summary>
    /// <returns>Boolean value</returns>
    private bool IsFist()
    {
        for (int i = 0; i < 5; i++)
        {
            if (this.glove.Fingers[i] <= 0.4f)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Update the position of the hand according to the arm.
    /// </summary>
    private void UpdatePosition()
    {
        Vector3 newpos = this.root.transform.position;
        this.hand.transform.position = newpos;
        this.hand.transform.rotation = this.root.transform.rotation;
        this.hand.transform.Rotate(Vector3.up, -90);
        this.hand.transform.Rotate(Vector3.forward, -90);
    }

    /// <summary>
    /// When application is terminated, cancel Manus communication.
    /// </summary>
    private void OnApplicationQuit()
    {
        Manus.ManusExit();
    }

    #endregion Methods
}