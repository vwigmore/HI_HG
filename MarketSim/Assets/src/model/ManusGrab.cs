namespace Assets.src.model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Implements specific grab functions for the Manus.
    /// </summary>
    /// <seealso cref="Assets.src.model.Grab" />
    internal class ManusGrab : Grab
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ManusGrab"/> class.
        /// </summary>
        /// <param name="grabber">The grabber.</param>
        /// <param name="highlightColor">Color of the highlight.</param>
        public ManusGrab(GameObject grabber, Color highlightColor)
            : base(grabber, highlightColor)
        {
            this.Grabber = grabber;
            this.highlightColor = highlightColor;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Drops the object.
        /// </summary>
        public override void DropObject()
        {
            Vector3 newpos = GrabbedObject.transform.position;
            newpos.y = 0;
            GrabbedObject.transform.position = newpos;
            GrabbedObject.GetComponent<Collider>().enabled = true;
            GrabbedObject.GetComponent<Rigidbody>().Sleep();
            this.GrabbedObject = null;
            this.highlighted = null;
        }

        #endregion Methods
    }
}