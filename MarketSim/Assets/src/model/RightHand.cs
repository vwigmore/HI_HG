﻿using Assets.src.model;
using ManusMachina;
using System.Collections;
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
    public RightHand(Glove glove, Transform RootTransform, GameObject handModel, GameObject handRoot, GameObject hand, AnimationClip animation, Color hl)
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
        newrot.y -= 180;

        this.handModel.transform.rotation = Quaternion.Euler(newrot);
    }

    #endregion Methods
}