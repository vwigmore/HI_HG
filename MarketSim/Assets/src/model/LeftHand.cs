﻿using Assets.src.model;
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

        newrot.y += 90;

        this.handModel.transform.rotation = Quaternion.Euler(newrot);
    }

    /// <summary>
    /// Updates the gestures.
    /// </summary>
    public override void UpdateGestures()
    {
        Gestures gesture = GetGesture();

        if (!manusGrab.IsGrabbing())
        {
            if (gesture == Gestures.Grab)
                manusGrab.GrabHighlightedObject();
        }
        else
        {
            if (gesture == Gestures.Open)
                manusGrab.DropObject();
        }

        if (glove_hand == GLOVE_HAND.GLOVE_LEFT && Manager.GestureMovementOn)
        {
            if (gesture == Gestures.Thumb)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().walkBackwards();
            if (gesture == Gestures.Point)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().walkForward();
            if (gesture == Gestures.Pinky)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().rotateRight();
        }

        this.manusGrab.UpdateGrabbedObject(gameTransforms[0][0].parent.gameObject.transform);
    }

    #endregion Methods
}