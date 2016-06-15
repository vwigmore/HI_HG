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
    public LeftHand(Glove glove, Transform RootTransform, GameObject handModel, GameObject handRoot, GameObject hand, AnimationClip animation, Color hl)
        : base(glove, RootTransform, handModel, handRoot, hand, animation, hl)
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
        Vector3 newrot = this.root.transform.rotation.eulerAngles;

        newrot.y -= 90;

        this.handModel.transform.rotation = Quaternion.Euler(newrot);
    }

    /// <summary>
    /// Updates the gestures.
    /// </summary>
    public override void UpdateGestures()
    {
        base.UpdateGestures();

        Gestures gesture = GestureController.GetGesture(this.glove);

        if (glove_hand == GLOVE_HAND.GLOVE_LEFT && Manager.GestureMovementOn)
        {
            MoveWithGesture(gesture);
        }
        this.manusGrab.UpdateGrabbedObject(-0.1f, gameTransforms[0][0].parent.gameObject.transform);
        this.manusGrab.basket.UpdateList();
    }

    /// <summary>
    /// Moves according to the given gesture.
    /// </summary>
    /// <param name="g">The gesture.</param>
    public void MoveWithGesture(Gestures g)
    {
        if (g == Gestures.Thumb)
        {
            GestureClient gc = new GestureClient(new Backward());
            gc.movePlayer(GameObject.FindGameObjectWithTag("Player"));
        }
        if (g == Gestures.Point)
        {
            GestureClient gc = new GestureClient(new Forward());
            gc.movePlayer(GameObject.FindGameObjectWithTag("Player"));
        }

        if (g == Gestures.Pinky)
        {
            GestureClient gc = new GestureClient(new RotateRight());
            gc.movePlayer(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    #endregion Methods
}