using Assets.src.model;
using ManusMachina;
using UnityEngine;

public class HandController : MonoBehaviour
{
    #region Fields

    public Transform RootTransform;

    /// <summary>
    /// The glove_hand
    /// </summary>
    public GLOVE_HAND glove_hand;

    /// <summary>
    /// The hand
    /// </summary>
    private IHand hand;

    #endregion Fields

    #region Methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        hand = HandFactory.createHand(glove_hand, RootTransform);
    }

    /// <summary>
    ///  Update is called once per frame.
    /// </summary>
    private void Update()
    {
        if (!Manager.MKBOnly)
        {
            hand.UpdatePosition();
        }
        hand.UpdateHand();
        hand.UpdateGestures();
        hand.UpdateVibration();
        hand.UpdateTimer();
    }

    /// <summary>
    /// Called when [trigger enter].
    /// </summary>
    /// <param name="collision">The collision.</param>
    private void OnTriggerEnter(Collider collision)
    {
        GameObject collisionObj = collision.gameObject;
        hand.GetManusGrab().HighlightSelectedObject(collisionObj);

        hand.Touch(collisionObj);
    }

    /// <summary>
    /// Called when [trigger exit].
    /// </summary>
    /// <param name="collision">The collision.</param>
    private void OnTriggerExit(Collider collision)
    {
        hand.GetManusGrab().ClearHighlights();
    }

    #endregion Methods
}