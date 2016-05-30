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
    public class ManusGrab : Grab
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
        /// Updates the grabbed object.
        /// </summary>
        public override void UpdateGrabbedObject()
        {
        }

        /// <summary>
        /// Updates the grabbed object.
        /// </summary>
        /// <param name="trans">The trans.</param>
        public void UpdateGrabbedObject(Transform trans)
        {
            if (IsGrabbing())
            {
                Vector3 newpos = grabber.transform.position;
                GrabbedObject.transform.position = newpos;
                GrabbedObject.transform.rotation = trans.rotation;
                GrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        /// <summary>
        /// Drop currently grabbed object.
        /// </summary>
        public override void DropObject()
        {
            GrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            GrabbedObject = null;
            highlighted = null;
        }

        #endregion Methods
    }
}