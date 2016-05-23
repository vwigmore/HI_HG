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

        public override void UpdateGrabbedObject()
        {
            if (IsGrabbing())
            {
                Vector3 newpos = grabber.transform.position;
                GrabbedObject.transform.position = newpos;
                GrabbedObject.GetComponent<Collider>().enabled = false;
            }
        }

        public override void DropObject()
        {
            //Vector3 newpos = grabbedObject.transform.position;
            //newpos.y = 0;
            //grabbedObject.transform.position = newpos;
            GrabbedObject.GetComponent<Collider>().enabled = true;

            //grabbedObject.GetComponent<Rigidbody>().Sleep();
            GrabbedObject = null;
            highlighted = null;
        }

        #endregion Methods
    }
}