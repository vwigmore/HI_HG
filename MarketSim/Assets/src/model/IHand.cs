
﻿using UnityEngine;
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.src.model
{
    /// <summary>
    /// Interface used for hands.
    /// </summary>
    public interface IHand
    {
        #region Methods

        /// <summary>
        /// Initializes the transforms.
        /// </summary>
        void InitTransforms();

        /// <summary>
        /// Creates the colliders.
        /// </summary>
        void CreateColliders();

        /// <summary>
        /// Updates the position.
        /// </summary>
        void UpdatePosition();

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
        /// <returns></returns>
        ManusGrab GetManusGrab();

        /// <summary>
        /// Gets the gesture.
        /// </summary>
        /// <returns></returns>
        Gestures GetGesture();

  

        /// <summary>
        /// Touches the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        void Touch(GameObject obj);

        /// <summary>
        /// Gets the colliders.
        /// </summary>
        /// <returns></returns>
        ArrayList GetColliders();

        /// <summary>
        /// Gets the root transform.
        /// </summary>
        /// <returns></returns>
        Transform GetRootTransform();

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <returns></returns>
        Vector3 GetPosition();

        #endregion Methods
    }
}