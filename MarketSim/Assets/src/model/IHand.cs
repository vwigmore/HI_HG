using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.src.model
{
    /// <summary>
    /// Interface used for hands.
    /// </summary>
    internal interface IHand
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
        void UpdateHand();

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

        #endregion Methods
    }
}