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
        #region Fields

        /// <summary>
        /// The throw force
        /// </summary>
        private readonly float throwForce = 500;

        /// <summary>
        /// The last position
        /// </summary>
        private Vector3 lastPos;

        #endregion Fields

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
        /// <param name="trans">The trans.</param>
        public void UpdateGrabbedObject(float offset, Transform trans)
        {
            if (IsGrabbing())
            {
                SetPrevPosition(GrabbedObject.transform.position);
                Vector3 newpos = grabber.transform.position;
                if (GrabbedObject.tag.Equals("basket"))
                {
                    float y = this.GrabbedObject.GetComponent<BoxCollider>().bounds.size.y;
                    GrabbedObjectsPosition(newpos, trans);
                }
                newpos.z += offset;
                GrabbedObjectsPosition(newpos,trans);
            }
        }

        /// <summary>
        /// Grabbeds the objects.
        /// </summary>
        /// <param name="newpos">The newpos.</param>
        /// <param name="trans">The trans.</param>
        public void GrabbedObjectsPosition(Vector3 newpos, Transform trans)
        {
            GrabbedObject.transform.position = newpos;
            GrabbedObject.transform.rotation = trans.rotation;
            GrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        #endregion Methods
    }
}