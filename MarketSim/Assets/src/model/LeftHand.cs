using Assets.src.model;
using ManusMachina;
using System.Collections;
using UnityEngine;

/// <summary>
/// Left Hand extends Hand.
/// </summary>
public class LeftHand : Hand
{
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="LeftHand"/> class.
    /// </summary>
    /// <param name="handModel">The hand model.</param>
    /// <param name="handRoot">The hand root.</param>
    /// <param name="hand">The hand.</param>
    /// <param name="animation">The animation.</param>
    /// <param name="hl">The highlight color.</param>
    public LeftHand(Glove glove, Transform RootTransform, GameObject handModel, GameObject handRoot, GameObject hand, AnimationClip animation, Color hl, VibrateHand vh)
        : base(glove, RootTransform, handModel, handRoot, hand, animation, hl, vh)
    {
    }

    #endregion Constructors

    #region Methods

    /// <summary>
    /// Update the position of the hand according to the arm.
    /// </summary>
    public override void UpdatePosition()
    {
        Vector3 newpos = this.root.transform.position;
        this.handModel.transform.position = newpos;

        GameObject wrist = GameObject.Find("12_Wrist_Left");
        GameObject elbow = GameObject.Find("11_Elbow_Left");
        Vector3 dir = wrist.transform.position - elbow.transform.position;
        this.handModel.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);

        Vector3 newrot = this.root.transform.rotation.eulerAngles;

        newrot.y += 90;

        this.handModel.transform.rotation = Quaternion.Euler(newrot);
    }

    /// <summary>
    /// Updates the gestures.
    /// </summary>
    public override void UpdateGestures()
    {
        base.UpdateGestures();

        Gestures gesture = GetGesture();

        if (glove_hand == GLOVE_HAND.GLOVE_LEFT && Manager.GestureMovementOn)
        {
            MoveWithGesture(gesture);
        }
    }

    /// <summary>
    /// Moves according to the given gesture.
    /// </summary>
    /// <param name="g">The gesture.</param>
    public void MoveWithGesture(Gestures g)
    {
        if (g == Gestures.Thumb)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().walkBackwards();
        if (g == Gestures.Point)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().walkForward();
        if (g == Gestures.Pinky)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().rotateRight();
    }

    #endregion Methods
}