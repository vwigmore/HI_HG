using ManusMachina;
using UnityEngine;

/// <summary>
/// Left Hand extends Hand.
/// </summary>
public class RightHand : Hand
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
    public RightHand(Glove glove, Transform RootTransform, GameObject handModel, GameObject handRoot, GameObject hand, AnimationClip animation, Color hl, VibrateHand vh)
        : base(glove, RootTransform, handModel, handRoot, hand, animation, hl, vh)
    {
    }

    #endregion Constructors

    #region Methods

    /// <summary>
    /// Updates the gestures.
    /// </summary>
    public override void UpdateGestures()
    {
        base.UpdateGestures();
    }

    /// <summary>
    /// Update the position of the hand according to the arm.
    /// </summary>
    public override void UpdatePosition()
    {
        Vector3 newpos = this.root.transform.position;
        this.handModel.transform.position = newpos;

        GameObject wrist = GameObject.Find("22_Wrist_Right");
        GameObject elbow = GameObject.Find("21_Elbow_Right");
        Vector3 dir = wrist.transform.position - elbow.transform.position;
        this.handModel.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);

        Vector3 newrot = this.root.transform.rotation.eulerAngles;
        newrot.y -= 180;
        this.handModel.transform.rotation = Quaternion.Euler(newrot);
    }

    #endregion Methods
}