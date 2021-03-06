﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.model
{
    /// <summary>
    /// Interface used for hands.
    /// </summary>
    public interface IHand
    {
        #region Methods

        /// <summary>
        /// Updates the position.
        /// </summary>
        void UpdatePosition();

        /// <summary>
        /// Calls all update methods.
        /// </summary>
        /// <param name="b">The bends value, determines if a finger can bend or not.</param>
        void Update(bool[] b);

        /// <summary>
        /// Updates the hand.
        /// </summary>
        void UpdateHand(bool[] o);

        /// <summary>
        /// Updates the gestures.
        /// </summary>
        void UpdateGestures();

        /// <summary>
        /// Returns the ManusGrabs.
        /// </summary>
        /// <returns>The ManusGrab of the hand</returns>
        ManusGrab GetManusGrab();

        /// <summary>
        /// Touches the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        void Touch(GameObject obj);

        /// <summary>
        /// Gets the colliders.
        /// </summary>
        /// <returns>The Colliders</returns>
        ArrayList GetColliders();

        /// <summary>
        /// Gets the root transform.
        /// </summary>
        /// <returns>The root</returns>
        Transform GetRootTransform();

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <returns>The position</returns>
        Vector3 GetPosition();

        #endregion Methods
    }
}