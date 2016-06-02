namespace Assets.src.model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Implements specific grab functions for the Mouse.
    /// </summary>
    /// <seealso cref="Assets.src.model.Grab" />
    public class MouseGrab : Grab
    {
        #region Fields

        /// <summary>
        /// The ray cast hit
        /// </summary>
        private RaycastHit hit;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseGrab"/> class.
        /// </summary>
        /// <param name="grabber">The grabber.</param>
        /// <param name="highlightColor">Color of the highlight.</param>
        public MouseGrab(GameObject grabber, Color highlightColor)
            : base(grabber, highlightColor)
        {
            this.Grabber = grabber;
            this.highlightColor = highlightColor;
           
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Sets the Ray cast hit.
        /// </summary>
        /// <param name="hit">The Ray cast hit.</param>
        public void SetHit(RaycastHit hit)
        {
            this.hit = hit;
        }

        #endregion Methods
    }
}