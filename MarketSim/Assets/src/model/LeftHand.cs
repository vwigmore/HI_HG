using Assets.src.model;
using ManusMachina;
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

        Gestures gesture = GetGesture();

        GestureMovement(gesture);

        this.manusGrab.UpdateGrabbedObject(-0.1f, gameTransforms[0][0].parent.gameObject.transform);
        this.manusGrab.basket.UpdateList();
    }

    /// <summary>
    /// Gestures the movement.
    /// </summary>
    public void GestureMovement(Gestures gesture)
    {
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
        if (g == Gestures.Thumb) WalkBackWards(g);
        if (g == Gestures.Point) WalkForward(g);
        if (g == Gestures.Pinky) RotateRight(g);
          
    }

    /// <summary>
    /// Walks the back wards.
    /// </summary>
    /// <param name="g">The g.</param>
    public void WalkBackWards(Gestures g)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().walkBackwards();
    }

    /// <summary>
    /// Walks the forward.
    /// </summary>
    /// <param name="g">The g.</param>
    public void WalkForward(Gestures g)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().walkForward();
    }

    /// <summary>
    /// Rotates the right.
    /// </summary>
    /// <param name="g">The g.</param>
    public void RotateRight(Gestures g)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().rotateRight();
    }
    #endregion Methods
}