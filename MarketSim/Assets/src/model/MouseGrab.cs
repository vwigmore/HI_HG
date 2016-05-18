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
    internal class MouseGrab : Grab
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
        /// Drop currently grabbed object.
        /// </summary>
        public override void DropObject()
        {
            if (!this.InProximity(this.hit.point))
            {
                return;
            }

            if ((1.0 - (this.hit.normal.y * this.hit.normal.y)) < 0.4 &&
                !this.hit.transform.gameObject.tag.Equals("Player"))
            {
                Vector3 newpos = this.hit.point;
                GrabbedObject.transform.position = newpos;
                GrabbedObject.GetComponent<Collider>().enabled = true;
                GrabbedObject.GetComponent<Rigidbody>().Sleep();
                this.GrabbedObject = null;
                this.highlighted = null;
            }
        }

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